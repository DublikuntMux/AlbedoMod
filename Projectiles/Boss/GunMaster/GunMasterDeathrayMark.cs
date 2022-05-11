using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunMaster
{
	public class GunMasterDeathrayMark : BaseDeathray
	{
		public GunMasterDeathrayMark()
			: base(30f, "GunMasterDeathray")
		{
		}

		public override bool CanDamage()
		{
			return false;
		}

		public override void AI()
		{
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			float num = 0.3f;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= maxTime)
			{
				projectile.Kill();
				return;
			}
			projectile.scale = (float)Math.Sin(projectile.localAI[0] * (float)Math.PI / maxTime) * 0.6f * num;
			if (projectile.scale > num)
			{
				projectile.scale = num;
			}
			float num2 = 3f;
			_ = projectile.width;
			float[] array = new float[(int)num2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 3000f;
			}
			float num3 = array.Sum();
			num3 /= num2;
			float amount = 0.5f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num3, amount);
			Vector2 vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
			for (int k = 0; k < 2; k++)
			{
				float num4 = projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? -1f : 1f) * ((float)Math.PI / 2f);
				float num5 = (float)Main.rand.NextDouble() * 2f + 2f;
				Vector2 vector3 = new Vector2((float)Math.Cos(num4) * num5, (float)Math.Sin(num4) * num5);
				int num6 = Dust.NewDust(vector2, 0, 0, 244, vector3.X, vector3.Y, 0, default(Color), 1f);
				Main.dust[num6].noGravity = true;
				Main.dust[num6].scale = 1.7f;
			}
			if (Main.rand.NextBool(5))
			{
				Vector2 vector4 = Utils.RotatedBy(projectile.velocity, 1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				int num7 = Dust.NewDust(vector2 + vector4 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default(Color), 1.5f);
				Dust obj = Main.dust[num7];
				obj.velocity *= 0.5f;
				Main.dust[num7].velocity.Y = 0f - Math.Abs(Main.dust[num7].velocity.Y);
			}
			Projectile projectile1 = projectile;
			projectile1.position -= projectile.velocity;
			projectile.rotation = projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Projectile.NewProjectile(projectile.Center, projectile.velocity, ModContent.ProjectileType<GunMasterDeathray>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
			}
		}
	}
}
