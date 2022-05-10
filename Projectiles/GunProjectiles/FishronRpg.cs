using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Albedo.Global.AlbedoUtils;

namespace Albedo.Projectiles.GunProjectiles
{
	public class FishronRpg : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishron RPG");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 95;
			projectile.ranged = true;
		}

		public override void AI()
		{
			if (projectile.ai[1] == 0f)
			{
				for (int i = 0; i < 5; i++)
				{
					int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
					Dust obj = Main.dust[num];
					obj.velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num].scale = 0.5f;
						Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				projectile.ai[1] = 1f;
				Main.PlaySound(SoundID.Item92, projectile.position);
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				projectile.ai[1] = 0f;
				projectile.alpha = 255;
				projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 200;
				projectile.height = 200;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
				projectile.knockBack = 10f;
			}
			else if (Math.Abs(projectile.velocity.X) >= 8f || Math.Abs(projectile.velocity.Y) >= 8f)
			{
				for (int j = 0; j < 2; j++)
				{
					float num2 = 0f;
					float num3 = 0f;
					if (j == 1)
					{
						num2 = projectile.velocity.X * 0.5f;
						num3 = projectile.velocity.Y * 0.5f;
					}
					int num4 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num2, projectile.position.Y + 3f + num3) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 33, 0f, 0f, 100, default(Color), 1f);
					Dust obj2 = Main.dust[num4];
					obj2.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
					Dust obj3 = Main.dust[num4];
					obj3.velocity *= 0.2f;
					Main.dust[num4].noGravity = true;
					num4 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num2, projectile.position.Y + 3f + num3) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 33, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num4].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
					Dust obj4 = Main.dust[num4];
					obj4.velocity *= 0.05f;
				}
			}
			HomeInOnNPC(projectile, !projectile.tileCollide, 200f, 12f, 20f);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			return true;
		}
		
		public override void Kill(int timeLeft)
		{
			projectile.position = projectile.Center;
			projectile.width = (projectile.height = 48);
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			projectile.maxPenetrate = -1;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.Damage();
			Main.PlaySound(SoundID.Item92, projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[num];
				obj.velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num].scale = 0.5f;
					Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 20; j++)
			{
				int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num2].noGravity = true;
				Dust obj2 = Main.dust[num2];
				obj2.velocity *= 5f;
				num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[num2];
				obj3.velocity *= 2f;
			}
		}
	}
}
