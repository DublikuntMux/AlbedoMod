using System;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Blast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = Main.projFrames[645];
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.alpha = 0;
            projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
        }

        public override void AI()
        {
            if (projectile.position.HasNaNs())
            {
                projectile.Kill();
                return;
            }

            var projectile1 = projectile;
            if (++projectile1.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                var projectile2 = projectile;
                if (++projectile2.frame >= Main.projFrames[projectile.type])
                {
                    var projectile3 = projectile;
                    projectile3.frame--;
                    projectile.Kill();
                }
            }

            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item88, projectile.Center);
                projectile.scale = Main.rand.NextFloat(1f, 3f);
                projectile.rotation = Main.rand.NextFloat((float) Math.PI * 2f);
            }
        }

        public override bool CanDamage()
        {
            return projectile.frame < 4;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(153, 300);
            target.immune[projectile.owner] = 1;
        }


        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 127) * projectile.Opacity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var y = num * projectile.frame;
            var rectangle = new Rectangle(0, y, texture2D.Width, num);
            var origin = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}