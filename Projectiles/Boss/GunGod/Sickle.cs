using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Sickle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.alpha = 100;
            projectile.hostile = true;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (this.projectile.localAI[0] == 0f)
            {
                this.projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item8, this.projectile.Center);
            }

            var projectile = this.projectile;
            projectile.rotation += 0.8f;
            if ((this.projectile.localAI[1] += 1f) < 90f)
            {
                var projectile2 = this.projectile;
                projectile2.velocity *= 1.045f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var y = num * projectile.frame;
            var rectangle = new Rectangle(0, y, texture2D.Width, num);
            var origin = rectangle.Size() / 2f;
            var color = lightColor;
            color = projectile.GetAlpha(color);
            for (var i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                var color2 = color;
                color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) /
                          (float) ProjectileID.Sets.TrailCacheLength[projectile.type];
                var vector = projectile.oldPos[i];
                var rotation = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D,
                    vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                    rectangle, color2, rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(30, 600);
        }
    }
}