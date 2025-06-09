using Terraria.ModLoader;

namespace EternityMod.Content.Reworks.Bosses.LunaticCultist;

public class LunaticCultistOverride : GlobalNPC
{
    #region Enumerations

    public enum CultistState
    {
        // Summon animation.
        SummonAnimation,

        // Phase one attacks.
        SolarBarrage,
        LightningOrbAndVortexPortals,
        IceBallAndFrostWaves,
        Ritual,
        
        // Phase two attacks.
        NebulaOrbsAndPrismBeam,
        HomingStardustAndAlienBolts,
        AncientDooms,

        // Desperation phase attacks.
        StarBulletHell,
        LunarDeathraySpin,

        // Death animation.
        DeathAnimation
    }

    #endregion Enumerations
}
