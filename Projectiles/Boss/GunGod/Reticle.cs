using System;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Reticle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 110;
            projectile.height = 110;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.alpha = byte.MaxValue;
            projectile.timeLeft = 70;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (AlbedoUtils.BossIsAlive(ref AlbedoGlobalNpc.HellGuard, ModContent.NPCType<NPCs.Boss.GunGod.GunGod>()) &&
                !Main.npc[AlbedoGlobalNpc.HellGuard].dontTakeDamage)
            {
                if (projectile.localAI[0] == 0.0)
                {
                    projectile.localAI[0] = Main.rand.NextBool() ? -1f : 1f;
                    projectile.rotation = Main.rand.NextFloat(6.283185f);
                }

                projectile.scale = (float) (1.5 - 0.00833333376795053 * Math.Min(60, 70 - projectile.timeLeft));
                projectile.velocity = Vector2.Zero;
                if (++projectile.localAI[1] < 15.0)
                    projectile.rotation += MathHelper.ToRadians(12f) * projectile.localAI[0];
            }

            if (projectile.timeLeft < 10)
                projectile.alpha += 26;
            else
                projectile.alpha -= 26;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            else
            {
                if (projectile.alpha <= byte.MaxValue)
                    return;
                projectile.alpha = byte.MaxValue;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, 128) *
                   (float) (1.0 - projectile.alpha / (double) byte.MaxValue);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var num2 = num1 * projectile.frame;
            var rectangle = new Rectangle(0, num2, texture2D.Width, num1);
            var vector2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, vector2, projectile.scale, 0, 0.0f);
            return false;
        }
    }
}