using System;
using System.Collections.Generic;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class GlowRingHollow : ModProjectile
    {
        public Color color = Color.White;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glow Ring");
        }

        public override void SetDefaults()
        {
            projectile.width = 1000;
            projectile.height = 1000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles,
            List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles,
            List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindProjectiles.Add(index);
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            projectile.timeLeft = 2;
            var num = 500f;
            var num2 = 60;
            var num3 = 3;
            switch ((int) projectile.ai[0])
            {
                case 1:
                    color = Color.Red;
                    num = 525f;
                    num2 = 180;
                    break;
                case 2:
                    color = Color.Green;
                    num = 350f;
                    num2 = 180;
                    break;
                case 3:
                {
                    color = Color.Yellow;
                    num2 = 180;
                    num3 = 10;
                    var val5 = AlbedoUtils.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
                    if (val5 != null)
                    {
                        projectile.Center = val5.Center;
                        num = 1400f * (num2 - projectile.localAI[0]) / num2;
                        break;
                    }

                    projectile.Kill();
                    return;
                }
                case 4:
                    color = Color.Cyan;
                    num = 1200f;
                    num2 = 360;
                    break;
                case 5:
                    color = new Color(51, 255, 191);
                    num2 = 120;
                    num = 1200f * (float) Math.Cos(Math.PI / 2.0 * projectile.localAI[0] / num2);
                    num3 = -1;
                    projectile.alpha = 0;
                    break;
                case 8:
                    color = Color.Red;
                    num2 = 60;
                    num3 = 3;
                    num = projectile.ai[1] * (float) Math.Sqrt(Math.Sin(Math.PI / 2.0 * projectile.localAI[0] / num2));
                    break;
            }

            if ((projectile.localAI[0] += 1f) > num2)
            {
                projectile.Kill();
                return;
            }

            if (num3 >= 0)
            {
                projectile.alpha = 255 - (int) (255.0 * Math.Sin(Math.PI / num2 * projectile.localAI[0])) * num3;
                if (projectile.alpha < 0) projectile.alpha = 0;
            }

            color.A = 0;
            projectile.scale = num * 2f / 1000f;
            projectile.position = projectile.Center;
            projectile.width = projectile.height = (int) (1000f * projectile.scale);
            projectile.Center = projectile.position;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return color * projectile.Opacity * (Main.mouseTextColor / 255f) * 0.9f;
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
                projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None,
                0f);
            return false;
        }
    }
}