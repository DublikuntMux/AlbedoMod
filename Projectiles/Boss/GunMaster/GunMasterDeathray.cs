using System;
using System.Linq;
using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunMaster
{
	public class GunMasterDeathray : BaseDeathray
	{
		private Vector2 _spawnPos;

		public GunMasterDeathray()
			: base(120f, "GunMasterDeathray")
		{
		}

		public override void AI()
		{
			if (!Main.dedServ && Main.LocalPlayer.active)
			{
				Main.LocalPlayer.GetModPlayer<AlbedoPlayer>().Screenshake = 2;
			}

			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.localAI[0] == 0f)
			{
				_spawnPos = projectile.Center;
			}
			else
			{
				projectile.Center = _spawnPos + Main.rand.NextVector2Circular(5f, 5f);
			}
			float num = 5f;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= maxTime)
			{
				projectile.Kill();
				return;
			}
			projectile.scale = (float)Math.Sin(projectile.localAI[0] * (float)Math.PI / maxTime) * num * 6f;
			if (projectile.scale > num)
			{
				projectile.scale = num;
			}
			if (projectile.localAI[0] > maxTime / 2f && projectile.scale < num && projectile.ai[0] > 0f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					for (int i = Main.rand.Next(150); i < 3000; i += 300)
					{
						Projectile.NewProjectile(projectile.Center + projectile.velocity * i, Vector2.Zero, ModContent.ProjectileType<ScytheSplit>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], -1f);
					}
				}
				projectile.ai[0] = 0f;
			}
			float num2 = 3f;
			_ = projectile.width;
			float[] array = new float[(int)num2];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = 3000f;
			}
			float num3 = array.Sum();
			num3 /= num2;
			float amount = 0.5f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num3, amount);
			Vector2 vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
			for (int l = 0; l < 2; l++)
			{
				float num4 = projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)) ? (-1f) : 1f) * ((float)Math.PI / 2f);
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

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(195, 600);
			target.AddBuff(196, 600);
		}
	}
}
