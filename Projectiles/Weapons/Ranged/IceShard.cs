using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Albedo.AlbedoUtils;

namespace Albedo.Projectiles.Weapons.Ranged
{
    public class IceShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Shard");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
        }

        public override void AI()
        {
            var projectile1 = projectile;
            projectile1.rotation += 0.15f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 3f)
            {
                Lighting.AddLight(projectile.Center, new Vector3(44f, 191f, 232f) * 0.005098039f);
                NewDust(projectile, Vector2.Zero, 68, 3, 90, 1, default, 100);
            }

            HomeInOnNPC(projectile, !projectile.tileCollide, 150f, 12f, 25f);
        }

        public override void Kill(int timeLeft)
        {
            const int num = 2;
            if (projectile.owner == Main.myPlayer)
                for (var i = 0; i < num; i++)
                {
                    var vector = RandomVelocity(100f, 70f, 100f);
                    Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<IceShardSplit>(),
                        Convert.ToInt32(projectile.damage * 0.45), 0f, projectile.owner);
                }

            Main.PlaySound(SoundID.Item118, projectile.Center);
        }
    }
}