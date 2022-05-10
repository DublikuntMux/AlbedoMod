using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Albedo.Global.AlbedoUtils;

namespace Albedo.Projectiles.GunProjectiles
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
			Projectile projectile1 = projectile;
			projectile1.rotation += 0.15f;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 3f)
			{
				Lighting.AddLight(projectile.Center, new Vector3(44f, 191f, 232f) * 0.005098039f);
				for (int i = 0; i < 2; i++)
				{
					const int num = 14;
					int num2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y) , projectile.width - num * 2, projectile.height - num * 2, 68, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num2].noGravity = true;
					Dust obj = Main.dust[num2];
					obj.velocity *= 0.1f;
					Dust obj2 = Main.dust[num2];
					obj2.velocity += projectile.velocity * 0.5f;
				}
			}
			HomeInOnNPC(projectile, !projectile.tileCollide, 150f, 12f, 25f);
		}

		public override void Kill(int timeLeft)
		{
			const int num = 2;
			if (projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = RandomVelocity(100f, 70f, 100f);
					Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<IceShardSplit>(), Convert.ToInt32(projectile.damage * 0.45), 0f, projectile.owner, 0f, 0f);
				}
			}
			Main.PlaySound(SoundID.Item118, projectile.Center);
		}
	}
}
