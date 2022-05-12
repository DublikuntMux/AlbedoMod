using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Weapons.Ranged
{
	internal class SwitcherRocket : ModProjectile
	{
		private float Lifetime
		{
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
			if (Lifetime < 2f)
			{
				projectile.velocity *= 1.05f;
			}
			projectile.rotation = projectile.velocity.ToRotation();
			Dust.NewDustPerfect(projectile.Center, 31, (Vector2?)Vector2.Zero, 100, default(Color), 2f).noGravity = true;
			if (Lifetime > 0.5f)
			{
				for (int i = -1; i < 2; i += 2)
				{
					Vector2 vector = projectile.velocity * i * 8f * 0.25f;
					Vector2 vector2 = projectile.velocity * 0.25f;
					for (int j = 1; j < 5; j++)
					{
						Dust.NewDustPerfect(projectile.Center + vector2 * j + vector * j * (float)Math.Sin(Main.time * 10.0 + j), 182, (Vector2?)(-projectile.velocity * 3f), 100, default(Color), 0.7f).noGravity = true;
					}
				}
			}
			Lifetime += 0.1f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(23f, 5f), 1f, SpriteEffects.None, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Projectile explode = Projectile.NewProjectileDirect(projectile.position, Vector2.Zero, 695, (int)(projectile.damage * 2.5f), 4f, projectile.owner);
			explode.hide = true;
			explode.netUpdate = true;
			Main.PlaySound(SoundID.DD2_KoboldExplosion, projectile.position);
			AlbedoUtils.NewDust(explode, Vector2.Zero, 31, 24, 90, 1f, default(Color), 0, noGrav: true, noLight: false, delegate(Dust o)
			{
				o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
				o.fadeIn = Main.rand.NextFloat(2f, 4f);
			});
			AlbedoUtils.NewDust(explode, Vector2.Zero, 6, 28, 90, 3f, default(Color), 0, noGrav: true, noLight: false, delegate(Dust o)
			{
				o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
			});
			AlbedoUtils.NewDust(explode, Vector2.Zero, 182, 12, 90, 1.4f, default(Color), 0, noGrav: true, noLight: false, delegate(Dust o)
			{
				o.velocity = -VelocityToPoint(o.position, explode.Center, Main.rand.NextFloat(4f));
			});
		}

		private static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float speed)
		{
			Vector2 vector = B - A;
			vector *= speed / vector.Length();
			if (!Utils.HasNaNs(vector))
			{
				return vector;
			}
			return Vector2.Zero;
		}
	}
}
