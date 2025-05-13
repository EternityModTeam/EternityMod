using System.Collections.Generic;
using SubworldLibrary;
using Terraria.ModLoader;

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
        
    }
}
