using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Weapons.Ranged
{
    internal class Holowave : ModProjectile
    {
        protected const float FrameSpeed = 1f;
        protected float FrameCounter;

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 34;
            Main.projFrames[projectile.type] = 10;
            projectile.friendly = true;
            projectile.aiStyle = 0;
            projectile.timeLeft = 70;
            projectile.tileCollide = true;
            projectile.ranged = true;
        }


        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * ((255 - projectile.alpha) / 255f);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var origin = new Vector2(10f, 17f);
            var num = (float) Math.Abs(Math.Cos(Main.time * 8.0) * 0.5);
            var vector = projectile.velocity.SafeNormalize(Vector2.Zero) * 8f;
            for (var num2 = -1f - num; num2 <= 1f + num; num2 += 0.2f)
            {
                var position = projectile.Center + vector * num2 * projectile.scale - Main.screenPosition;
                spriteBatch.Draw(color: new Color(1f, 1f, 1f, (1f - Math.Min(1f, Math.Abs(num2))) * 0.2f),
                    texture: Main.projectileTexture[projectile.type], position: position,
                    sourceRectangle: new Rectangle(0, 34 * projectile.frame, 20, 34), rotation: projectile.rotation,
                    origin: origin, scale: projectile.scale, effects: SpriteEffects.None, layerDepth: 0f);
            }
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            FrameCounter += projectile.velocity.Length() * 0.1f * FrameSpeed;
            if (FrameCounter >= 2f)
            {
                var projectile1 = projectile;
                projectile1.frame++;
                FrameCounter = 0f;
            }

            if (projectile.frame >= Main.projFrames[projectile.type]) projectile.frame = 0;
            if (projectile.timeLeft < 20) projectile.alpha = 255 - (int) (255f * (projectile.timeLeft / 20f));
            if (projectile.timeLeft > 50) projectile.alpha = (int) (255f * ((projectile.timeLeft - 50) / 20f));
        }

        public override void Kill(int timeLeft)
        {
            if (timeLeft != 0) AlbedoUtils.NewDust(projectile, projectile.velocity * 0.2f, 88, 6, 50);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = height = 20;
            return true;
        }
    }
}