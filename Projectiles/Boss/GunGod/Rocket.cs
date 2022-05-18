using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Rocket : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_448";

		public override void SetStaticDefaults() => Main.projFrames[projectile.type] = 3;

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.alpha = 0;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;
			projectile.scale = 2.5f;
			cooldownSlot = 1;
		}

		public override void AI()
		{
			if (projectile.ai[1] > 0f) {
				var projectile1 = projectile;
				projectile1.timeLeft++;
				if ((projectile.ai[1] -= 1f) == 0f) {
					projectile.velocity = Vector2.Normalize(projectile.velocity) * (projectile.velocity.Length() + 6f);
					projectile.netUpdate = true;
					for (int i = 0; i < 36; i++) {
						var vector =
							(Vector2.UnitX * -8f + -Vector2.UnitY.RotatedBy(i * 3.14159274101257 / 36.0 * 2.0) *
								new Vector2(2f, 8f)).RotatedBy(projectile.rotation - 1.57079637050629);
						int num = Dust.NewDust(projectile.Center, 0, 0, 228);
						Main.dust[num].scale = 1f;
						Main.dust[num].noGravity = true;
						Main.dust[num].position = projectile.Center + vector * 6f;
						Main.dust[num].velocity = projectile.velocity * 0f;
					}
				}
			}
			else {
				if (projectile.ai[0] >= 0f && projectile.ai[0] < 255f) {
					if ((projectile.ai[1] -= 1f) > -45f) {
						double num2 = (Main.player[(int) projectile.ai[0]].Center - projectile.Center).ToRotation() -
						              (double) projectile.velocity.ToRotation();
						if (num2 > Math.PI) num2 -= Math.PI * 2.0;
						if (num2 < -Math.PI) num2 += Math.PI * 2.0;
						projectile.velocity = projectile.velocity.RotatedBy(num2 * 0.05000000074505806);
					}
				}
				else {
					projectile.ai[0] = Player.FindClosest(projectile.Center, 0, 0);
				}

				projectile.tileCollide = true;
				if ((projectile.localAI[0] += 1f) > 10f) {
					projectile.localAI[0] = 0f;
					for (int j = 0; j < 36; j++) {
						var vector2 =
							(Vector2.UnitX * -8f + -Vector2.UnitY.RotatedBy(j * 3.14159274101257 / 36.0 * 2.0) *
								new Vector2(2f, 4f)).RotatedBy(projectile.rotation - 1.57079637050629);
						int num3 = Dust.NewDust(projectile.Center, 0, 0, 228);
						Main.dust[num3].scale = 1f;
						Main.dust[num3].noGravity = true;
						Main.dust[num3].position = projectile.Center + vector2 * 6f;
						Main.dust[num3].velocity = projectile.velocity * 0f;
					}
				}
			}

			projectile.rotation = projectile.velocity.ToRotation() + (float) Math.E * 449f / 777f;
			var vector3 = Vector2.UnitY.RotatedBy(projectile.rotation) * 8f * 2f;
			int num4 = Dust.NewDust(projectile.Center, 0, 0, 228);
			Main.dust[num4].position = projectile.Center + vector3;
			Main.dust[num4].scale = 1f;
			Main.dust[num4].noGravity = true;
			var projectile2 = projectile;
			if (++projectile2.frameCounter >= 3) {
				projectile.frameCounter = 0;
				var projectile3 = projectile;
				if (++projectile3.frame >= 3) projectile.frame = 0;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(36, 300);
			projectile.timeLeft = 0;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);
			projectile.position = projectile.Center;
			projectile.width = projectile.height = 112;
			projectile.position.X -= projectile.width / 2;
			projectile.position.Y -= projectile.height / 2;
			for (int i = 0; i < 4; i++)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default, 1.5f);
			for (int j = 0; j < 40; j++) {
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 228, 0f, 0f, 0,
					default, 2.5f);
				Main.dust[num].noGravity = true;
				var obj = Main.dust[num];
				obj.velocity *= 3f;
				int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 228, 0f, 0f, 100,
					default, 1.5f);
				var obj2 = Main.dust[num2];
				obj2.velocity *= 2f;
				Main.dust[num2].noGravity = true;
			}

			for (int k = 0; k < 1; k++) {
				int num3 = Gore.NewGore(
					projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f,
						projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default,
					Main.rand.Next(61, 64));
				var obj3 = Main.gore[num3];
				obj3.velocity *= 0.3f;
				Main.gore[num3].velocity.X += Main.rand.Next(-10, 11) * 0.05f;
				Main.gore[num3].velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
			}
		}

		public override Color? GetAlpha(Color lightColor) => Color.White * projectile.Opacity;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y = num * projectile.frame;
			var rectangle = new Rectangle(0, y, texture2D.Width, num);
			var origin = rectangle.Size() / 2f;
			Main.spriteBatch.Draw(texture2D,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
				projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}