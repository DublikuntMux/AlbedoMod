using System;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class GlowRing : ModProjectile
    {
        public Color color = new Color(255, 255, 255, 0);

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.alpha = 0;
            projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
        }

        public override void AI()
        {
            var val = AlbedoUtils.NpcExists(projectile.ai[0]);
            if (val != null) projectile.Center = val.Center;
            var num = 12f;
            var num2 = 30;
            var flag = false;
            switch ((int) projectile.ai[1])
            {
                case -23:
                {
                    flag = true;
                    num2 = 90;
                    var num5 = projectile.localAI[0] / num2;
                    color = new Color(51, 255, 191) * num5;
                    projectile.alpha = (int) (255f * (1f - num5));
                    projectile.scale = 27f * (1f - num5);
                    break;
                }
                case -21:
                    num = 4f;
                    num2 = 60;
                    break;
                case -20:
                {
                    flag = true;
                    num2 = 200;
                    var num3 = projectile.localAI[0] / num2;
                    color = new Color(51, 255, 191) * num3;
                    projectile.alpha = (int) (255f * (1f - num3));
                    projectile.scale = 18f * (1f - num3);
                    break;
                }
                case -19:
                    color = Color.Yellow;
                    color.A = 0;
                    num = 18f;
                    break;
                case -18:
                    num = 36f;
                    num2 = 120;
                    break;
                case -17:
                    num = 6f;
                    goto case -16;
                case -16:
                    color = new Color(255, 51, 153, 0);
                    break;
                case -15:
                    num = 18f;
                    goto case -16;
                case -14:
                    num = 24f;
                    goto case -16;
                case -13:
                    color = new Color(93, 255, 241, 0);
                    num = 6f;
                    num2 = 15;
                    break;
                case -12:
                    color = new Color(0, 0, 255, 0);
                    num2 = 45;
                    break;
                case -11:
                    color = new Color(0, 255, 0, 0);
                    num2 = 45;
                    break;
                case -10:
                    color = new Color(0, 255, 255, 0);
                    num2 = 45;
                    break;
                case -9:
                    color = new Color(255, 255, 0, 0);
                    num2 = 45;
                    break;
                case -8:
                    color = new Color(255, 127, 40, 0);
                    num2 = 45;
                    break;
                case -7:
                    color = new Color(255, 0, 0, 0);
                    num2 = 45;
                    break;
                case -6:
                    color = new Color(255, 255, 0, 0);
                    num = 18f;
                    break;
                case -5:
                    color = new Color(200, 0, 255, 0);
                    num = 18f;
                    break;
                case -4:
                    color = new Color(255, 255, 0, 0);
                    num = 18f;
                    num2 = 60;
                    break;
                case -3:
                    color = new Color(255, 100, 0, 0);
                    num = 18f;
                    num2 = 60;
                    break;
                case -2:
                    color = new Color(51, 255, 191, 0);
                    num = 18f;
                    break;
                case -1:
                    color = new Color(200, 0, 200, 0);
                    num2 = 60;
                    break;
                case 4:
                    color = new Color(51, 255, 191, 0);
                    num2 = 45;
                    break;
                case 222:
                    color = new Color(255, 255, 100, 0);
                    num2 = 45;
                    break;
                case 114:
                    color = new Color(93, 255, 241, 0);
                    num = 12f;
                    num2 = 30;
                    break;
                case 125:
                    color = new Color(255, 0, 0, 0);
                    num = 24f;
                    num2 = 60;
                    break;
                case 129:
                case 130:
                    color = new Color(255, 0, 0, 0);
                    num = 12f;
                    num2 = 30;
                    break;
                case 439:
                    color = new Color(255, 127, 40, 0);
                    break;
                case 396:
                case 397:
                case 398:
                    color = new Color(51, 255, 191, 0);
                    num = 12f;
                    num2 = 60;
                    break;
            }

            if ((projectile.localAI[0] += 1f) > num2)
            {
                projectile.Kill();
                return;
            }

            if (!flag)
            {
                projectile.scale = num * (float) Math.Sin(Math.PI / 2.0 * projectile.localAI[0] / num2);
                projectile.alpha = (int) (255f * projectile.localAI[0] / num2);
            }

            if (projectile.alpha < 0) projectile.alpha = 0;
            if (projectile.alpha > 255) projectile.alpha = 255;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return color * projectile.Opacity;
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