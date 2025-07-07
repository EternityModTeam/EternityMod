global using static System.MathF;
global using static Microsoft.Xna.Framework.MathHelper;
global using static Luminance.Common.Utilities.Utilities;
global using static EternityMod.Core.Utilities.EternityUtilities;

using System.IO;
using Terraria.ModLoader;
using Luminance.Core.ModCalls;
using EternityMod.Core.Networking;

namespace EternityMod;

public class EternityMod : Mod
{
    // Defer packet reading to a separate class.
    public override void HandlePacket(BinaryReader reader, int whoAmI) => PacketManager.ReceivePacket(reader);

    // Use the Luminance library's mod call system for cross-compatibility.
    public override object Call(params object[] args) => ModCallManager.ProcessAllModCalls(this, args);
}
