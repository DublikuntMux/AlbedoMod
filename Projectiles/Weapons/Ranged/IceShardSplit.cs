using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Albedo.Helper.GameHelper;

namespace Albedo.Projectiles.Weapons.Ranged
{
    public class IceShardSplit : ModProjectile
    {
        public override string Texture => "Albedo/Projectiles/Empty";

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.scale = 0.9f;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
            projectile.ranged = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return projectile.timeLeft < 150 && target.CanBeChasedBy(projectile);
        }

        public override void AI()
        {
            var projectile1 = projectile;
            projectile1.rotation += 0.15f;
            Lighting.AddLight(projectile1.Center, new Vector3(44f, 191f, 232f) * 0.005098039f);
            for (var i = 0; i < 2; i++)
            {
                var val = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                    DustID.BlueCrystalShard,
                    0f, 0f, 100);
                val.noGravity = true;
            }

            if (projectile1.timeLeft < 150) HomeInOnNPC(projectile1, !projectile1.tileCollide, 450f, 12f, 25f);
        }
    }
}