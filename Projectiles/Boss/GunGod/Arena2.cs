using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Arena2 : ModProjectile
    {
        public override string Texture => "Terraria/Item_98";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GunGod Seal");
        }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            var val = AlbedoUtils.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
            if (val != null)
            {
                var projectile1 = projectile;
                projectile1.alpha -= 2;
                if (projectile.alpha < 0) projectile.alpha = 0;

                projectile1.Center = val.Center;
            }
            else
            {
                projectile.velocity = Vector2.Zero;
                var projectile2 = projectile;
                projectile2.alpha += 2;
                if (projectile.alpha > 255)
                {
                    projectile.Kill();
                    return;
                }
            }

            projectile.timeLeft = 2;
            projectile.scale = 1f - projectile.alpha / 255f;
            projectile.ai[0] += (float) Math.PI / 57f;
            if (projectile.ai[0] > (float) Math.PI)
            {
                projectile.ai[0] -= (float) Math.PI * 2f;
                projectile.netUpdate = true;
            }

            var projectile3 = projectile;
            projectile3.rotation += 0.3f;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var y = num * projectile.frame;
            var rectangle = new Rectangle(0, y, texture2D.Width, num);
            var origin = rectangle.Size() / 2f;
            var alpha = projectile.GetAlpha(lightColor);
            for (var i = 0; i < 6; i++)
            {
                var vector = new Vector2(150f * projectile.scale / 2f, 0f).RotatedBy(projectile.ai[0]);
                vector = vector.RotatedBy((float) Math.PI / 3f * i);
                Main.spriteBatch.Draw(texture2D,
                    projectile.Center + vector - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
                    alpha, i % 2 == 0 ? projectile.rotation : 0f - projectile.rotation, origin, projectile.scale,
                    i % 2 != 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }

            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }
    }
}