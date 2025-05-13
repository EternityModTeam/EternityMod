using Luminance.Core.ModCalls;
using Terraria.ModLoader;

namespace EternityMod;

public class EternityMod : Mod
{
    // Use Luminance's mod call manager for cross-compatibility.
    public override object Call(params object[] args) => ModCallManager.ProcessAllModCalls(this, args);
}
