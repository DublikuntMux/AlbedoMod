using System;
using System.Linq;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class GunGodDeathray : BaseDeathray
	{
		private Vector2 _spawnPos;

		public GunGodDeathray()
			: base(120f, "GunGodDeathray")
		{
		}

		public override void AI()
		{
			if (!Main.dedServ && Main.LocalPlayer.active) Main.LocalPlayer.GetModPlayer<AlbedoPlayer>().ScreenShake = 2;

			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
				projectile.velocity = -Vector2.UnitY;
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
				projectile.velocity = -Vector2.UnitY;
			if (projectile.localAI[0] == 0f)
				_spawnPos = projectile.Center;
			else
				projectile.Center = _spawnPos + Main.rand.NextVector2Circular(5f, 5f);
			float num = 5f;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= maxTime) {
				projectile.Kill();
				return;
			}

			projectile.scale = (float) Math.Sin(projectile.localAI[0] * (float) Math.PI / maxTime) * num * 6f;
			if (projectile.scale > num) projectile.scale = num;
			if (projectile.localAI[0] > maxTime / 2f && projectile.scale < num && projectile.ai[0] > 0f) {
				if (Main.netMode != NetmodeID.MultiplayerClient)
					for (int i = Main.rand.Next(150); i < 3000; i += 300)
						Projectile.NewProjectile(projectile.Center + projectile.velocity * i, Vector2.Zero,
							ModContent.ProjectileType<ScytheSplit>(), projectile.damage, projectile.knockBack,
							projectile.owner, projectile.ai[0], -1f);
				projectile.ai[0] = 0f;
			}

			float num2 = 3f;
			_ = projectile.width;
			float[] array = new float[(int) num2];
			for (int j = 0; j < array.Length; j++) array[j] = 3000f;
			float num3 = array.Sum();
			num3 /= num2;
			float amount = 0.5f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num3, amount);
			var vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
			for (int l = 0; l < 2; l++) {
				float num4 = projectile.velocity.ToRotation() +
				             (Main.rand.NextBool(2) ? -1f : 1f) * ((float) Math.PI / 2f);
				float num5 = (float) Main.rand.NextDouble() * 2f + 2f;
				var vector3 = new Vector2((float) Math.Cos(num4) * num5, (float) Math.Sin(num4) * num5);
				int num6 = Dust.NewDust(vector2, 0, 0, DustID.CopperCoin, vector3.X, vector3.Y);
				Main.dust[num6].noGravity = true;
				Main.dust[num6].scale = 1.7f;
			}

			if (Main.rand.NextBool(5)) {
				var vector4 = projectile.velocity.RotatedBy(1.5707963705062866) *
				              ((float) Main.rand.NextDouble() - 0.5f) * projectile.width;
				int num7 = Dust.NewDust(vector2 + vector4 - Vector2.One * 4f, 8, 8, DustID.CopperCoin, 0f, 0f, 100, default, 1.5f);
				var obj = Main.dust[num7];
				obj.velocity *= 0.5f;
				Main.dust[num7].velocity.Y = 0f - Math.Abs(Main.dust[num7].velocity.Y);
			}

			var projectile1 = projectile;
			projectile1.position -= projectile.velocity;
			projectile.rotation = projectile.velocity.ToRotation() - (float) Math.PI / 2f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(195, 600);
			target.AddBuff(196, 600);
		}
	}
}