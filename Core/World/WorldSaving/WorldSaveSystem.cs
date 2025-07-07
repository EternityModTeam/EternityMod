using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using SubworldLibrary;

namespace EternityMod.Core.World.WorldSaving;

public class WorldSaveSystem : ModSystem
{
    public static bool DeathModeEnabled
    {
        get;
        set;
    }

    public static bool NightmareModeEnabled
    {
        get;
        set;
    }

    public override void OnWorldLoad()
    {
        if (SubworldSystem.AnyActive())
            return;
    }

    public override void OnWorldUnload()
    {
        if (SubworldSystem.AnyActive())
            return;

        DeathModeEnabled = false;
        NightmareModeEnabled = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        if (DeathModeEnabled)
            tag["DeathModeEnabled"] = true;
        if (NightmareModeEnabled)
            tag["NightmareModeEnabled"] = true;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        DeathModeEnabled = tag.ContainsKey("DeathModeEnabled");
        NightmareModeEnabled = tag.ContainsKey("NightmareModeEnabled");
    }

    public override void NetSend(BinaryWriter writer)
    {
        BitsByte bb = new();
        bb[0] = DeathModeEnabled;
        bb[1] = NightmareModeEnabled;

        writer.Write(bb);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte bb = reader.ReadByte();
        DeathModeEnabled = bb[0];
        NightmareModeEnabled = bb[1];
    }
}
