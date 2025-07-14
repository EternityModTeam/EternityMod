using Luminance.Common.Easings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace EternityMod.Content.Rarities;

public class EternityRarity : SpeciallyRenderedRarity
{
    public static Color ColorA => Color.Black;

    public static Color ColorB => Color.DarkViolet;

    public static float ColorInterpolant
    {
        get
        {
            float baseInterpolant = Cos01(Main.GlobalTimeWrappedHourly * 2.1f);
            float colorInterpolant = EasingCurves.Cubic.Evaluate(EasingType.InOut, baseInterpolant);
            return colorInterpolant;
        }
    }

    public static Color InvertedRarityColor => Color.Lerp(ColorA, ColorB, 1f - ColorInterpolant);

    public override Color RarityColor => Color.Lerp(ColorA, ColorB, ColorInterpolant);

    protected override void RenderRarityText(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float maxWidth, float spread, bool ui)
    {
        int splitLength = text.Length / 2;

        // Prioritize trying to split along natural spaces.
        int? spaceIndex = null;
        float minDistance = 9999f;
        for (int i = 0; i < text.Length; i++)
        {
            float distanceFromSplit = Distance(i, splitLength);
            if (distanceFromSplit < minDistance && text[i] == ' ')
            {
                spaceIndex = i;
                minDistance = distanceFromSplit;
            }
        }

        if (spaceIndex is not null)
            splitLength = spaceIndex.Value;

        string partA = new(text.AsSpan(0, splitLength));
        string partB = new(text.AsSpan(splitLength, text.Length - splitLength));

        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, partA, position, RarityColor, rotation, origin, scale, maxWidth, spread);

        position.X += font.MeasureString(partA).X * scale.X;
        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, partB, position, InvertedRarityColor, rotation, origin, scale, maxWidth, spread);
    }
}
