using Terraria;
using Terraria.ModLoader;
using static Albedo.AlbedoUtils;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Weapons.Ranged
{
	public class IceShardSplit : ModProjectile
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
			Projectile projectile1 = projectile;
			projectile1.rotation += 0.15f;
			Lighting.AddLight(projectile1.Center, new Vector3(44f, 191f, 232f) * 0.005098039f);
			AlbedoUtils.NewDust(projectile1, Vector2.Zero, 68, 2, 100, 1, default(Color), 100, true);
			if (projectile1.timeLeft < 150)
			{
				HomeInOnNPC(projectile1, !projectile1.tileCollide, 450f, 12f, 25f);
			}
		}
	}
}
