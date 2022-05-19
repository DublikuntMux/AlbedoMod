using System;
using Albedo.Global;
using Albedo.Helper;
using Albedo.Projectiles.Combined;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Accessories
{
	public class Imitator : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 20;
			projectile.height = 54;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			var val = Main.player[projectile.owner];
			var modPlayer = val.GetModPlayer<AlbedoPlayer>();
			if (val.whoAmI == Main.myPlayer && (val.dead || !modPlayer.GodImitator)) {
				modPlayer.GodImitator = false;
				projectile.Kill();
				projectile.netUpdate = true;
				return;
			}

			projectile.netUpdate = true;
			const float num = 50f;
			float num2 = Main.mouseTextColor / 200f - 0.35f;
			num2 *= 0.2f;
			projectile.scale = num2 + 0.95f;
			const float x = 75f;
			Lighting.AddLight(projectile.Center, 0.1f, 0.4f, 0.2f);
			projectile.position = val.Center + new Vector2(x, 0f).RotatedBy(projectile.ai[1]);
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			const float num3 = 0.03f;
			projectile.ai[1] -= num3;
			if (projectile.ai[1] > (float) Math.PI) {
				projectile.ai[1] -= (float) Math.PI * 2f;
				projectile.netUpdate = true;
			}

			projectile.rotation = projectile.ai[1] + (float) Math.PI / 2f;
			if (projectile.ai[0] != 0f) {
				projectile.ai[0] -= 1f;
				return;
			}

			float x2 = projectile.position.X;
			float y = projectile.position.Y;
			bool flag = false;
			var val2 = BossHelper.NpcExists(
				GameHelper.FindClosestHostileNpcPrioritizingMinionFocus(projectile, 700f, true));
			if (val2 != null) {
				x2 = val2.Center.X;
				y = val2.Center.Y;
				projectile.Distance(val2.Center);
				flag = true;
			}

			if (flag) {
				var vector = new Vector2(projectile.position.X + projectile.width * 0.5f,
					projectile.position.Y + projectile.height * 0.5f);
				float num4 = x2 - vector.X;
				float num5 = y - vector.Y;
				float num6 = (float) Math.Sqrt(num4 * num4 + num5 * num5);
				num6 = 10f / num6;
				num4 *= num6;
				num5 *= num6;
				if (projectile.owner == Main.myPlayer)
					Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num4, num5,
						ModContent.ProjectileType<PostLunarBulletProjectile>(),
						projectile.damage, projectile.knockBack, projectile.owner);
				projectile.ai[0] = num;
			}

			if (Main.netMode == NetmodeID.Server) projectile.netUpdate = true;
		}
	}
}