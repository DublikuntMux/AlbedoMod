using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class ScytheSpin : ModProjectile
    {
        public override string Texture => "Terraria/Item_98";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 420;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item71, projectile.Center);
            }

            if (projectile.timeLeft == 390)
            {
                projectile.velocity = Vector2.Zero;
                projectile.netUpdate = true;
            }
            else if (projectile.timeLeft == 360)
            {
                Main.PlaySound(SoundID.Item84, projectile.Center);
            }
            else if (projectile.timeLeft < 360)
            {
                var val = AlbedoUtils.NpcExists(projectile.ai[0], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
                if (val == null)
                {
                    projectile.Kill();
                    return;
                }

                var center = val.Center;
                projectile.velocity = (center - projectile.Center).RotatedBy(Math.PI / 2.0 * projectile.ai[1]);
                var projectile1 = projectile;
                projectile1.velocity *= (float) Math.PI / 180f;
            }

            projectile.spriteDirection = (int) projectile.ai[1];
            var projectile2 = projectile;
            projectile2.rotation += projectile.spriteDirection * 0.5f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item71, projectile.Center);
            for (var i = 0; i < 20; i++)
            {
                var num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27);
                Main.dust[num].noGravity = true;
                Main.dust[num].noLight = true;
                var obj = Main.dust[num];
                obj.scale += 1f;
                var obj2 = Main.dust[num];
                obj2.velocity *= 4f;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num2 = Player.FindClosest(projectile.Center, 0, 0);
                if (num2 != -1)
                {
                    var vector = 15f * projectile.DirectionTo(Main.player[num2].Center);
                    Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<Sickle3>(),
                        projectile.damage, projectile.knockBack, projectile.owner, num2);
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(30, 600);
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
            var effects = projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (var i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                var color2 = color;
                color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) /
                          (float) ProjectileID.Sets.TrailCacheLength[projectile.type];
                var vector = projectile.oldPos[i];
                var rotation = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D,
                    vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                    rectangle, color2, rotation, origin, projectile.scale, effects, 0f);
            }

            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, effects, 0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}