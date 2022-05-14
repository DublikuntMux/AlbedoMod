using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Albedo.Projectiles.Boss.HellGuard
{
    public class EarthChainBlast2 : MoonLordSunBlast
    {
        public override string Texture => "Terraria/Projectile_687";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chain Blast");
            Main.projFrames[projectile.type] = Main.projFrames[645];
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(24, 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var num2 = num1 * projectile.frame;
            var rectangle = new Rectangle(0, num2, texture2D.Width, num1);
            var vector2 = rectangle.Size() * 2f;
            var color = projectile.ai[1] <= 3.0
                ? Color.Lerp(new Color(byte.MaxValue, 95, 46, 50), new Color(150, 35, 0, 100),
                    (float) ((3.0 - projectile.ai[1]) / 3.0))
                : Color.Lerp(new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0),
                    new Color(byte.MaxValue, 95, 46, 50), Math.Min(2f, 7f - projectile.ai[1]) / 4f);
            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), rectangle, color,
                projectile.rotation, vector2, projectile.scale, 0, 0.0f);
            return false;
        }
    }
}