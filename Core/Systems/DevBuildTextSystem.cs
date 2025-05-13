using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace EternityMod.Core.Systems;

public class DevBuildTextSystem : ModSystem
{
    public override void PostDrawInterface(SpriteBatch spriteBatch)
    {
        if (Main.gameMenu)
            return;

        DrawText(spriteBatch, Color.Lavender, "- Eternity Demonstration -" +
            "\nAll current content portrayed in Eternity is subject to change or be removed.", new(Main.screenWidth / 2, Main.screenHeight / 24f), 0.3f);
    }

    public static void DrawText(SpriteBatch spriteBatch, Color color, string text, Vector2 position, float scale = 1f)
    {
        var font = FontAssets.DeathText.Value;

        var textSize = font.MeasureString(text) * scale;
        var textPosition = position - textSize / 2f;

        var shadowColor = Color.Black;
        shadowColor.A = color.A;

        ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, textPosition, shadowColor, 0f, default, Vector2.One * scale);
        ChatManager.DrawColorCodedString(spriteBatch, font, text, textPosition, color, 0f, default, Vector2.One * scale);
    }
}
