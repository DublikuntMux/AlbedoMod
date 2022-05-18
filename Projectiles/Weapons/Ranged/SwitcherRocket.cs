using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Weapons.Ranged
{
	internal class SwitcherRocket : ModProjectile
	{
		private float Lifetime {
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.aiStyle = 0;
			projectile.tileCollide = true;
			projectile.penetrate = 1;
		}

		public override void AI()
		{
			if (Lifetime < 2f) projectile.velocity *= 1.05f;
			projectile.rotation = projectile.velocity.ToRotation();
			Dust.NewDustPerfect(projectile.Center, 31, Vector2.Zero, 100, default, 2f).noGravity = true;
			if (Lifetime > 0.5f)
				for (int i = -1; i < 2; i += 2) {
					var vector = projectile.velocity * i * 8f * 0.25f;
					var vector2 = projectile.velocity * 0.25f;
					for (int j = 1; j < 5; j++)
						Dust.NewDustPerfect(
							projectile.Center + vector2 * j + vector * j * (float) Math.Sin(Main.time * 10.0 + j), 182,
							-projectile.velocity * 3f, 100, default, 0.7f).noGravity = true;
				}

			Lifetime += 0.1f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null,
				Color.White, projectile.rotation, new Vector2(23f, 5f), 1f, SpriteEffects.None, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			var explode = Projectile.NewProjectileDirect(projectile.position, Vector2.Zero,
				ProjectileID.DD2ExplosiveTrapT2Explosion,
				(int) (projectile.damage * 2.5f), 4f, projectile.owner);
			explode.hide = true;
			explode.netUpdate = true;
			Main.PlaySound(SoundID.DD2_KoboldExplosion, projectile.position);
			NewDust(explode, Vector2.Zero, 31, 24, 90, 1f, default, 0, true, false, delegate(Dust o) {
				o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
				o.fadeIn = Main.rand.NextFloat(2f, 4f);
			});
			NewDust(explode, Vector2.Zero, 6, 28, 90, 3f, default, 0, true, false,
				delegate(Dust o) {
					o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
				});
			NewDust(explode, Vector2.Zero, 182, 12, 90, 1.4f, default, 0, true, false,
				delegate(Dust o) {
					o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
				});
		}

		private static void NewDust(Projectile projectile, Vector2 speed, int type, int count = 1, int chance = 100,
			float size = 1f, Color color = default, int alpha = 0, bool noGrav = true, bool noLight = false,
			Action<Dust> callback = null)
		{
			for (int i = 0; i < count; i++)
				if (Main.rand.Next(100) <= chance) {
					var val = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, type,
						speed.X, speed.Y, alpha, color, size);
					val.noGravity = noGrav;
					val.noLight = noLight;
					callback?.Invoke(val);
				}
		}

		private static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float speed)
		{
			var vector = B - A;
			vector *= speed / vector.Length();
			if (!vector.HasNaNs()) return vector;
			return Vector2.Zero;
		}
	}
}