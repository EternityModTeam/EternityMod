using Terraria.Localization;

namespace EternityMod.Core.Utilities;

public static partial class EternityUtilities
{
    /// <param name="key">The language key. This will have "Mods.EternityMod." appended behind it.</param>
    /// <returns>
    /// A <see cref="LocalizedText"/> instance found using the provided key with "Mods.EternityMod." appended behind it. 
    /// <para>NOTE: Modded translations are not loaded until after PostSetupContent.</para>Caching the result is suggested.
    /// </returns>
    public static LocalizedText GetText(string key) =>
        Language.GetOrRegister("Mods.EternityMod." + key);

    /// <param name="key">The language key. This will have "Mods.EternityMod." appended behind it.</param>
    /// <returns>
    /// A <see cref="string"/> instance found using the provided key with "Mods.EternityMod." appended behind it.
    /// <para>NOTE: Modded translations are not loaded until after PostSetupContent.</para>Caching the result is suggested.
    /// </returns>
    public static string GetTextValue(string key) =>
        Language.GetTextValue("Mods.EternityMod." + key);
}
