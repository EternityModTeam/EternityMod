using Terraria.ModLoader;

namespace EternityMod.Core.CrossCompatibility.Inbound.Fargos;

public class FargosCompatibility : ModSystem
{
    /// <summary>
    /// The Fargo's Mutant mod.
    /// </summary>
    public static Mod? FargosMutant
    {
        get;
        private set;
    }

    /// <summary>
    /// The Fargo's Souls mod.
    /// </summary>
    public static Mod? FargosSouls
    {
        get;
        private set;
    }

    /// <summary>
    /// The Fargo's Souls DLC mod.
    /// </summary>
    public static Mod? FargosDLC
    {
        get;
        private set;
    }

    /// <summary>
    /// Checks whether Eternity Mode is active or not.
    /// </summary>
    public static bool EternityModeIsActive => (bool)(FargosSouls?.Call("Emode") ?? false);

    public override void PostSetupContent()
    {
        if (ModLoader.TryGetMod("Fargowiltas", out Mod mutant))
            FargosMutant = mutant;
        if (ModLoader.TryGetMod("FargowiltasSouls", out Mod souls))
            FargosSouls = souls;
        if (ModLoader.TryGetMod("FargowiltasCrossmod", out Mod dlc))
            FargosDLC = dlc;
    }
}
