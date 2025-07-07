using System.Collections.Generic;
using System.IO;
using EternityMod.Core.Globals.NPCs;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EternityMod.Core.World.WorldSaving;

public class BossDownedSaveSystem : ModSystem
{
    internal static List<string> downedRegistry = [];

    public override void OnWorldLoad()
    {
        if (!SubworldSystem.AnyActive())
            downedRegistry?.Clear();
    }

    public override void OnWorldUnload()
    {
        if (!SubworldSystem.AnyActive())
            downedRegistry?.Clear();
    }

    public override void OnModLoad()
    {
        EternityGlobalNPC.OnKillEvent += RecordBossDefeats;
    }

    public override void SaveWorldData(TagCompound tag) => tag[nameof(downedRegistry)] = downedRegistry;

    public override void LoadWorldData(TagCompound tag)
    {
        downedRegistry.Clear();
        downedRegistry.AddRange((List<string>)tag.GetList<string>(nameof(downedRegistry)));
    }

    public override void NetSend(BinaryWriter writer)
    {
        writer.Write(downedRegistry.Count);
        for (int i = 0; i < downedRegistry.Count; i++)
            writer.Write(downedRegistry[i]);
    }

    public override void NetReceive(BinaryReader reader)
    {
        downedRegistry.Clear();

        int downedBossesCount = reader.ReadInt32();
        for (int i = 0; i < downedBossesCount; i++)
            downedRegistry.Add(reader.ReadString());
    }

    private void RecordBossDefeats(NPC npc)
    {
        // Check if the specified mod NPC has the defeat recording interface.
        // If it does, execute the OnDefeat method and make a note of them in the interface.
        if (npc.ModNPC is not null and IBossDowned downed && !downedRegistry.Contains(npc.ModNPC.Name))
        {
            downed.OnDefeat();
            downedRegistry.Add(npc.ModNPC.Name);

            if (downed.AutomaticallyRegisterDeathGlobally)
                GlobalBossDownedSaveSystem.MarkDefeated(npc);
        }
    }

    /// <summary>
    ///     Sets the defeat state for a given mod NPC.
    /// </summary>
    /// <typeparam name="BossType">The ModNPC type to modify the defeat status of.</typeparam>
    /// <param name="isDefeated">Whether the boss should be marked as defeated or not.</param>
    public static void SetDefeatState<BossType>(bool isDefeated) where BossType : ModNPC
    {
        string bossName = ModContent.GetModNPC(ModContent.NPCType<BossType>()).Name;
        if (isDefeated && !downedRegistry.Contains(bossName))
            downedRegistry.Add(bossName);
        if (!isDefeated)
            downedRegistry.Remove(bossName);

        // Fire a world sync.
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.WorldData);
    }

    /// <summary>
    ///     Determines whether a given mod NPC has been defeated in the world yet or not.
    /// </summary>
    /// <typeparam name="BossType">The ModNPC type to check the defeat status of.</typeparam>
    public static bool HasDefeated<BossType>() where BossType : ModNPC =>
        downedRegistry.Contains(ModContent.GetModNPC(ModContent.NPCType<BossType>()).Name);
}
