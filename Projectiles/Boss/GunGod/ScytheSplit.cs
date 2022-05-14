using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class ScytheSplit : ModProjectile
    {
        public override string Texture => "Terraria/Item_98";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            cooldownSlot = 1;
            projectile.scale = 2f;
        }

        public override void AI()
        {
            var projectile1 = projectile;
            projectile1.rotation += 1f;
            if ((projectile.ai[0] -= 1f) <= 0f) projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            var num = projectile.ai[1] >= 0f ? 50 : 25;
            float num2 = projectile.ai[1] >= 0f ? 15 : 6;
            for (var i = 0; i < num; i++)
            {
                var num3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 70, 0f, 0f, 0,
                    default, 3.5f);
                var obj = Main.dust[num3];
                obj.velocity *= num2;
                Main.dust[num3].noGravity = true;
            }

            if (!(projectile.ai[1] >= 0f) || Main.netMode == NetmodeID.MultiplayerClient) return;
            int num4 = Player.FindClosest(projectile.Center, 0, 0);
            if (num4 != -1)
            {
                var vector = projectile.ai[1] == 0f
                    ? Vector2.Normalize(projectile.velocity)
                    : projectile.DirectionTo(Main.player[num4].Center);
                vector *= 30f;
                var num5 = projectile.ai[1] == 0f ? 6 : 10;
                for (var j = 0; j < num5; j++)
                    Projectile.NewProjectile(projectile.Center, vector.RotatedBy((float) Math.PI * 2f / num5 * j),
                        ModContent.ProjectileType<Sickle2>(), projectile.damage, projectile.knockBack, projectile.owner,
                        num4);
            }
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

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0) * projectile.Opacity;
        }
    }
}