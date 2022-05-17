using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
    public class LavaPalladOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Pallad Orb");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.scale = 2f;
            projectile.extraUpdates = 3;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (projectile.localAI[1] == 0.0)
            {
                projectile.localAI[1] = 1f;
                Main.PlaySound(SoundID.Item, projectile.Center, 14);
            }

            if (projectile.timeLeft % (projectile.extraUpdates + 1) == 0 && ++projectile.localAI[1] > 30.0)
            {
                if (projectile.localAI[1] < 90.0)
                {
                    var projectile1 = projectile;
                    projectile1.velocity *= 1.035f;
                }

                if (projectile.localAI[1] > 60.0 && projectile.localAI[1] < 150.0 &&
                    BossHelper.BossIsAlive(ref AlbedoGlobalNpc.HellGuard,
                        ModContent.NPCType<NPCs.Boss.HellGuard.HellGuard>()) &&
                    Main.npc[AlbedoGlobalNpc.HellGuard].HasValidTarget)
                {
                    var rotation1 = projectile.velocity.ToRotation();
                    var rotation2 =
                        (Main.player[Main.npc[AlbedoGlobalNpc.HellGuard].target].Center +
                            Main.player[Main.npc[AlbedoGlobalNpc.HellGuard].target].velocity * 10f - projectile.Center)
                        .ToRotation();
                    projectile.velocity =
                        new Vector2(projectile.velocity.Length(), 0f).RotatedBy(rotation1.AngleLerp(rotation2, 0.03f));
                }
            }

            var index1 = Dust.NewDust(projectile.position, projectile.width, projectile.height,
                Main.rand.NextBool() ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 2f);
            Main.dust[index1].noGravity = true;
            var dust1 = Main.dust[index1];
            dust1.velocity *= 3f;
            var index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height,
                Main.rand.NextBool() ? 174 : 259, 0.0f, 0.0f, 100);
            var dust2 = Main.dust[index2];
            dust2.velocity *= 2f;
            Main.dust[index2].noGravity = true;
            projectile.rotation += 0.4f;
            if (++projectile.frameCounter <= 3)
                return;
            projectile.frameCounter = 0;
            if (++projectile.frame < Main.projFrames[projectile.type])
                return;
            projectile.frame = 0;
        }

        public override void Kill(int timeLeft)
        {
            if (timeLeft > 0)
            {
                projectile.timeLeft = 0;
                projectile.position = projectile.Center;
                projectile.width = 500;
                projectile.height = 500;
                projectile.Center = projectile.position;
                projectile.penetrate = -1;
                projectile.Damage();
            }

            Main.PlaySound(SoundID.Item, projectile.Center, 14);
            for (var index1 = 0; index1 < 20; ++index1)
            {
                var index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100,
                    new Color(), 3.5f);
                Main.dust[index2].noGravity = true;
                var dust1 = Main.dust[index2];
                dust1.velocity *= 7f;
                var index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100,
                    new Color(), 1.5f);
                var dust2 = Main.dust[index3];
                dust2.velocity *= 3f;
            }

            for (var index4 = 0; index4 < 20; ++index4)
            {
                var index5 = Dust.NewDust(projectile.position, projectile.width, projectile.height,
                    Main.rand.NextBool() ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 4f);
                Main.dust[index5].noGravity = true;
                var dust3 = Main.dust[index5];
                dust3.velocity = dust3.velocity * 21f * projectile.scale;
                var index6 = Dust.NewDust(projectile.position, projectile.width, projectile.height,
                    Main.rand.NextBool() ? 174 : 259, 0.0f, 0.0f, 100, new Color(), 2.5f);
                var dust4 = Main.dust[index6];
                dust4.velocity *= 12f;
                Main.dust[index6].noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft <= 0)
                return;
            projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var num2 = num1 * projectile.frame;
            var rectangle = new Rectangle(0, num2, texture2D.Width, num1);
            var vector2 = rectangle.Size() / 2f;
            var spriteEffects = projectile.spriteDirection < 0 ? (SpriteEffects) 1 : 0;
            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }
    }
}