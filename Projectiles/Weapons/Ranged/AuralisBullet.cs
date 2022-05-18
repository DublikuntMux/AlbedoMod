using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Weapons.Ranged
{
	public class AuralisBullet : ModProjectile
	{
		private static readonly Color BlueColor = new Color(0, 77, 255);

		private static readonly Color GreenColor = new Color(0, 255, 77);
		public override string Texture => "Albedo/Projectiles/Empty";

		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 5;
			projectile.alpha = 255;
			projectile.timeLeft = 200;
			projectile.extraUpdates = 10;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		private static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
		{
			float amount = (float) ((Math.Sin((float) Math.PI * 2f / seconds * (double) Main.GlobalTime) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, amount);
		}

		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 6f)
				for (int i = 0; i < 5; i++) {
					var obj = Main.dust[
						Dust.NewDust(projectile.position, projectile.width, projectile.height, 229,
							projectile.velocity.X, projectile.velocity.Y, 100, ColorSwap(BlueColor, GreenColor, 1f))];
					obj.velocity = Vector2.Zero;
					obj.position -= projectile.velocity / 5f * i;
					obj.noGravity = true;
					obj.scale = 0.65f;
					obj.noLight = true;
					obj.color = ColorSwap(BlueColor, GreenColor, 1f);
				}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			const int num = 420;
			target.AddBuff(69, num);
			target.AddBuff(39, num);
		}
	}
}