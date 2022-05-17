using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Weapons.Ranged
{
    public class EaterofDreamsProjectile : ModProjectile
    {
        public override string Texture => "Albedo/Projectiles/Empty";

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.timeLeft = 90;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.25f / 255f,
                (255 - projectile.alpha) * 0.05f / 255f, (255 - projectile.alpha) * 0.05f / 255f);
            if (projectile.timeLeft > 90) projectile.timeLeft = 90;
            if (projectile.ai[0] > 7f)
            {
                var num296 = 1f;
                switch (projectile.ai[0])
                {
                    case 8f:
                        num296 = 0.25f;
                        break;
                    case 9f:
                        num296 = 0.5f;
                        break;
                    case 10f:
                        num296 = 0.75f;
                        break;
                }

                projectile.ai[0] += 1f;
                var num297 = 27;
                if (Main.rand.NextBool(2))
                    for (var num298 = 0; num298 < 1; num298++)
                    {
                        var num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y),
                            projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f,
                            projectile.velocity.Y * 0.2f, 100);
                        if (num297 == 235 && Main.rand.NextBool(3))
                        {
                            Main.dust[num299].noGravity = true;
                            Main.dust[num299].scale *= 3f;
                            var expr_DBEF_cp_0 = Main.dust[num299];
                            expr_DBEF_cp_0.velocity.X *= 2f;
                            var expr_DC0F_cp_0 = Main.dust[num299];
                            expr_DC0F_cp_0.velocity.Y *= 2f;
                        }
                        else
                        {
                            Main.dust[num299].scale *= 1.5f;
                        }

                        var expr_DC74_cp_0 = Main.dust[num299];
                        expr_DC74_cp_0.velocity.X *= 1.2f;
                        var expr_DC94_cp_0 = Main.dust[num299];
                        expr_DC94_cp_0.velocity.Y *= 1.2f;
                        Main.dust[num299].scale *= num296;
                        if (num297 == 75)
                        {
                            Main.dust[num299].velocity += projectile.velocity;
                            if (!Main.dust[num299].noGravity) Main.dust[num299].velocity *= 0.5f;
                        }
                    }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation += 0.3f * projectile.direction;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
                if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y;
                projectile.velocity *= 0.75f;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(153, 500);
        }
    }
}