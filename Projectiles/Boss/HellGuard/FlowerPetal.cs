using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
    public class FlowerPetal : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_221";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flower Petal");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.hide = true;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0.0 && projectile.timeLeft > 105)
                projectile.timeLeft = 105;
            if (projectile.localAI[0] == 0.0)
            {
                projectile.localAI[0] = 1f;
                projectile.scale = Main.rand.NextFloat(1.5f, 2f);
                projectile.frame = Main.rand.Next(3);
                projectile.hide = false;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }

            if (++projectile.localAI[1] > 30.0 && projectile.localAI[1] < 100.0)
            {
                var projectile1 = projectile;
                projectile1.velocity *= 1.06f;
            }

            projectile.rotation += projectile.velocity.X * 0.01f;
            var index = Dust.NewDust(projectile.Center, 0, 0, 86);
            Main.dust[index].noGravity = true;
            Main.dust[index].scale *= 2f;
            var dust = Main.dust[index];
            dust.velocity *= 0.1f;
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 86, projectile.velocity.X,
                projectile.velocity.Y);
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] != 0.0 || Main.netMode == NetmodeID.MultiplayerClient)
                return;
            for (var index = -1; index <= 1; ++index)
                Projectile.NewProjectile(projectile.Center,
                    projectile.velocity.RotatedBy((double) MathHelper.ToRadians(5f) * index) / 2f, projectile.type,
                    projectile.damage, 0.0f, Main.myPlayer, 1f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture2D = Main.projectileTexture[projectile.type];
            var num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            var num2 = num1 * projectile.frame;
            var rectangle = new Rectangle(0, num2, texture2D.Width, num1);
            var vector2 = rectangle.Size() / 2f;
            projectile.GetAlpha(lightColor);
            var spriteEffects = projectile.spriteDirection < 0 ? 0 : (SpriteEffects) 1;
            for (var index = 0; index < ProjectileID.Sets.TrailCacheLength[projectile.type]; ++index)
            {
                var color = Color.White * projectile.Opacity * 0.75f * 0.5f;
                var oldPo = projectile.oldPos[index];
                var num3 = projectile.oldRot[index];
                Main.spriteBatch.Draw(texture2D,
                    oldPo - projectile.Size / 2f - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY),
                    rectangle, color, num3, vector2, projectile.scale, spriteEffects, 0.0f);
            }

            Main.spriteBatch.Draw(texture2D,
                projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), rectangle,
                projectile.GetAlpha(lightColor), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
            return false;
        }
    }
}