using System;
using System.Collections.Generic;
using EternityMod.Core.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EternityMod.Core.Globals.NPCs;

public partial class EternityGlobalNPC : GlobalNPC
{
    internal ReferencedValueRegistry valueRegistry = new ReferencedValueRegistry();

    public delegate void EditSpawnRateDelegate(Player player, ref int spawnRate, ref int maxSpawns);

    public static event EditSpawnRateDelegate? EditSpawnRateEvent;

    public delegate void EditSpawnPoolDelegate(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo);

    public static event EditSpawnPoolDelegate? EditSpawnPoolEvent;

    public delegate void NPCActionDelegate(NPC npc);

    public static event NPCActionDelegate? OnKillEvent;

    public delegate bool NPCBoolDelegate(NPC npc);

    public static event NPCBoolDelegate? PreAIEvent;

    public static event NPCBoolDelegate? PreDrawEvent;

    public static event NPCBoolDelegate? CheckDeadEvent;

    public delegate void NPCSpawnDelegate(NPC npc, IEntitySource source);

    public static event NPCSpawnDelegate? OnSpawnEvent;

    public delegate void ModifyNPCLootDelegate(NPC npc, NPCLoot npcLoot);

    public static event ModifyNPCLootDelegate? ModifyNPCLootEvent;

    public delegate void ModifyGlobalLootDelegate(GlobalLoot globalLoot);

    public static event ModifyGlobalLootDelegate? ModifyGlobalLootEvent;

    public delegate void ModifyShopDelegate(NPCShop shop);

    public static event ModifyShopDelegate? ModifyShopEvent;

    public override bool InstancePerEntity => true;

    public override void Unload()
    {
        // Reset all events on mod unload.
        OnKillEvent = null;
        EditSpawnRateEvent = null;
        EditSpawnPoolEvent = null;
        OnSpawnEvent = null;
        PreAIEvent = null;
        PreDrawEvent = null;
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        OnSpawnEvent?.Invoke(npc, source);
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        ModifyNPCLootEvent?.Invoke(npc, npcLoot);
    }

    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        ModifyGlobalLootEvent?.Invoke(globalLoot);
    }

    public override bool PreAI(NPC npc)
    {
        // Use default behavior if the event has no subscribers.
        if (PreAIEvent is null)
            return true;

        // Apply AI alterations in accordance with the event.
        bool result = true;
        foreach (Delegate d in PreAIEvent.GetInvocationList())
            result &= ((NPCBoolDelegate)d).Invoke(npc);

        return result;
    }


    public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        // Use default behavior if the event has no subscribers.
        if (PreDrawEvent is null)
            return true;

        // Apply AI alterations in accordance with the event.
        bool result = true;
        foreach (Delegate d in PreDrawEvent.GetInvocationList())
            result &= ((NPCBoolDelegate)d).Invoke(npc);

        return result;
    }

    public override void OnKill(NPC npc)
    {
        OnKillEvent?.Invoke(npc);
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        // Apply spawn rate alterations in accordance with the event.
        EditSpawnRateEvent?.Invoke(player, ref spawnRate, ref maxSpawns);
    }

    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        // Apply spawn pool alterations in accordance with the event.
        EditSpawnPoolEvent?.Invoke(pool, spawnInfo);
    }

    public override bool CheckDead(NPC npc)
    {
        // Use default behavior if the event has no subscribers.
        if (CheckDeadEvent is null)
            return true;

        // Apply death effect modifications in accordance with the event.
        bool result = true;
        foreach (Delegate d in CheckDeadEvent.GetInvocationList())
            result &= ((NPCBoolDelegate)d).Invoke(npc);

        return result;
    }

    public static Referenced<T> GetValueRef<T>(NPC npc, string key) where T : struct =>
        npc.GetGlobalNPC<EternityGlobalNPC>().valueRegistry.GetValueRef<T>(key);
}
