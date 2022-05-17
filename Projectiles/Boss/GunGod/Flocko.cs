using System;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Flocko : ModProjectile
    {
        public override string Texture => "Terraria/Cloud_14";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloud");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.timeLeft = 420;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            var val = BossHelper.NpcExists(projectile.ai[0], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
            if (val == null)
            {
                projectile.Kill();
                return;
            }

            var center = val.Center;
            center.X += (val.localAI[3] > 1f ? 1200 : 2000) * (float) Math.Sin(Math.PI / 360.0 * projectile.ai[1]++);
            center.Y -= 1100f;
            var vector = center - projectile.Center;
            if (vector.Length() > 100f)
            {
                vector /= 8f;
                projectile.velocity = (projectile.velocity * 23f + vector) / 24f;
            }
            else if (projectile.velocity.Length() < 12f)
            {
                var projectile1 = projectile;
                projectile1.velocity *= 1.05f;
            }

            if ((projectile.localAI[0] += 1f) > 90f && (projectile.localAI[1] += 1f) > (val.localAI[3] > 1f ? 4 : 2))
            {
                Main.PlaySound(SoundID.Item27, projectile.position);
                projectile.localAI[1] = 0f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    if (Math.Abs(val.Center.X - projectile.Center.X) > 400f)
                        for (var i = 0; i < 2; i++)
                        {
                            var vector2 = new Vector2(Main.rand.Next(-1000, 1001), Main.rand.Next(-1000, 1001));
                            vector2.Normalize();
                            vector2 *= 8f;
                            Projectile.NewProjectile(projectile.Center + vector2 * 4f, vector2,
                                ModContent.ProjectileType<FrostShard>(), projectile.damage, projectile.knockBack,
                                projectile.owner);
                        }
            }
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (var i = 0; i < 20; i++)
            {
                var num = Dust.NewDust(projectile.position, projectile.width, projectile.height,
                    Main.rand.NextBool() ? 80 : 76);
                Main.dust[num].noGravity = true;
                Main.dust[num].noLight = true;
                var obj = Main.dust[num];
                obj.scale += 1f;
                var obj2 = Main.dust[num];
                obj2.velocity *= 4f;
            }
        }
    }
}