using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Parry : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.width = 78;
            projectile.height = 78;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 2f;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                projectile.rotation = Main.rand.NextFloat((float) Math.PI * 2f);
                Main.PlaySound(SoundID.NPCHit4, projectile.Center);
                for (var i = 0; i < 20; i++)
                {
                    var num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 87, 0f, 0f, 0,
                        new Color(255, 255, 255, 150), 1.5f);
                    Main.dust[num].noGravity = true;
                    var obj = Main.dust[num];
                    obj.velocity *= 3f;
                }
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
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var y = num * projectile.frame;
            var rectangle = new Rectangle(0, y, texture2D.Width, num);
            var origin = rectangle.Size() / 2f;
            var effects = projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, effects, 0f);
            return false;
        }
    }
}