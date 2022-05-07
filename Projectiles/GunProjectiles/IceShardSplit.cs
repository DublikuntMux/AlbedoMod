using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Albedo.AlbedoUtils;

namespace Albedo.Projectiles.GunProjectiles
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
			return projectile.timeLeft < 150 && target.CanBeChasedBy((object)projectile, false);
		}

		public override void AI()
		{
			Projectile projectile1 = this.projectile;
			projectile1.rotation += 0.15f;
			Lighting.AddLight(projectile1.Center, new Vector3(44f, 191f, 232f) * 0.005098039f);
			for (int i = 0; i < 2; i++)
			{
				const int num = 14;
				int num2 = Dust.NewDust(new Vector2(projectile1.Center.X, projectile1.Center.Y), projectile1.width - num * 2, projectile1.height - num * 2, 68, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num2].noGravity = true;
				Dust obj = Main.dust[num2];
				obj.velocity *= 0.1f;
				Dust obj2 = Main.dust[num2];
				obj2.velocity += ((Entity)projectile1).velocity * 0.5f;
			}
			if (projectile1.timeLeft < 150)
			{
				HomeInOnNPC(projectile1, !projectile1.tileCollide, 450f, 12f, 25f);
			}
		}
	}
}
