﻿using Luminance.Assets;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EternityMod.Core.BaseEntities;

public abstract class BasePrimitiveLaserbeam : ModProjectile
{
    // This determines whether standard, automatic drawing via PreDraw should be performed. When enabled, PreDraw will handle draw effects on their own.
    // When disabled, it is the derived class' responsibility to draw things when necessary.
    // This exists primarily for the purpose of allowing drawing in special interfaces, such as IPixelatedPrimitiveRenderer.
    public virtual bool UseStandardDrawing => true;

    public abstract int LaserPointCount
    {
        get;
    }

    public abstract float MaxLaserLength
    {
        get;
    }

    public abstract float LaserExtendSpeedInterpolant
    {
        get;
    }

    public abstract ManagedShader LaserShader
    {
        get;
    }

    public ref float LaserShootTime => ref Projectile.ai[1];

    public ref float Time => ref Projectile.localAI[0];

    public ref float LaserLengthFactor => ref Projectile.localAI[1];

    public override string Texture => MiscTexturesRegistry.InvisiblePixelPath;

    public abstract void PrepareLaserShader(ManagedShader laserShader);

    public abstract float LaserWidthFunction(float completionRatio);
    
    public abstract Color LaserColorFunction(float completionRatio);

    public override void SendExtraAI(BinaryWriter writer) =>
        writer.Write(LaserLengthFactor);

    public override void ReceiveExtraAI(BinaryReader reader) =>
        LaserLengthFactor = reader.ReadSingle();

    public override void AI()
    {
        // Make the laser extend outward.
        LaserLengthFactor = Lerp(LaserLengthFactor, 1f, LaserExtendSpeedInterpolant);

        // Decide the rotation of the laser.
        Projectile.rotation = Projectile.velocity.ToRotation();

        // Die once the laser is done existing.
        if (Time >= LaserShootTime)
            Projectile.Kill();

        Time++;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (Time >= LaserShootTime - 7f)
            return false;

        float _ = 0f;
        Vector2 start = Projectile.Center;
        Vector2 end = start + Projectile.velocity * LaserLengthFactor * MaxLaserLength;
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, Projectile.scale * Projectile.width * 0.9f, ref _);
    }

    public void DrawLaser(bool pixelate)
    {
        // Calculate laser control points.
        Vector2 laserDirection = Projectile.velocity.SafeNormalize(Vector2.UnitY);
        List<Vector2> laserControlPoints = Projectile.GetLaserControlPoints(10, LaserLengthFactor * MaxLaserLength, laserDirection);

        // Draw the laser.
        PrepareLaserShader(LaserShader);
        PrimitiveSettings settings = new PrimitiveSettings(LaserWidthFunction, LaserColorFunction, Pixelate: pixelate, Shader: LaserShader);
        PrimitiveRenderer.RenderTrail(laserControlPoints, settings, LaserPointCount);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        // Do nothing if standard drawing is disabled.
        if (!UseStandardDrawing)
            return false;

        // Draw the laser manually if standard drawing is enabled.
        DrawLaser(false);
        return false;
    }

    // Disallow natural movement from the laser.
    public override bool ShouldUpdatePosition() => false;
}
