using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
	public class EarthChainBlast : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_687";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chain Blast");
			Main.projFrames[projectile.type] = Main.projFrames[645];
		}

		public override void SetDefaults()
		{
			projectile.width = 100;
			projectile.height = 100;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 1;
		}

		public override bool CanDamage() => projectile.frame == 3 || projectile.frame == 4;

		public override void AI()
		{
			if (projectile.position.HasNaNs()) {
				projectile.Kill();
			}
			else {
				if (++projectile.frameCounter >= 2) {
					projectile.frameCounter = 0;
					if (++projectile.frame >= Main.projFrames[projectile.type]) {
						--projectile.frame;
						projectile.Kill();
						return;
					}
				}

				if (++projectile.localAI[1] == 8.0 && projectile.ai[1] > 0.0 &&
				    Main.netMode != NetmodeID.MultiplayerClient) {
					--projectile.ai[1];
					var rotationVector2 = projectile.ai[0].ToRotationVector2();
					float radians = MathHelper.ToRadians(15f);
					if (projectile.ai[1] > 2.0) {
						for (int index = -1; index <= 1; ++index)
							if (index != 0)
								Projectile.NewProjectile(
									projectile.Center + projectile.width * 1.25f *
									rotationVector2.RotatedBy((double) MathHelper.ToRadians(60f) * index +
									                          Main.rand.NextFloat(-radians, radians)), Vector2.Zero,
									projectile.type, projectile.damage, 0.0f, projectile.owner, projectile.ai[0],
									projectile.ai[1]);
					}
					else {
						Projectile.NewProjectile(
							projectile.Center + projectile.width * 2.25f *
							rotationVector2.RotatedBy(Main.rand.NextFloat(-radians, radians)), Vector2.Zero,
							projectile.type, projectile.damage, 0.0f, projectile.owner, projectile.ai[0],
							projectile.ai[1]);
					}
				}

				if (projectile.localAI[0] != 0.0)
					return;
				projectile.localAI[0] = 1f;
				Main.PlaySound(SoundID.Item88, projectile.Center);
				projectile.position = projectile.Center;
				projectile.scale = Main.rand.NextFloat(1f, 3f);
				projectile.width = (int) (projectile.width * (double) projectile.scale);
				projectile.height = (int) (projectile.height * (double) projectile.scale);
				projectile.Center = projectile.position;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit) => target.AddBuff(24, 300);

		public override void Kill(int timeLeft)
		{
			for (int index = 0; index < 4; ++index)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(),
					1.5f);
			if (!Main.rand.NextBool(4))
				return;
			int index1 =
				Gore.NewGore(
					projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f,
						projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, new Vector2(),
					Main.rand.Next(61, 64));
			var gore = Main.gore[index1];
			gore.velocity *= 0.3f;
			Main.gore[index1].velocity.X += Main.rand.Next(-10, 11) * 0.05f;
			Main.gore[index1].velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture2D = Main.projectileTexture[projectile.type];
			int num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int num2 = num1 * projectile.frame;
			var rectangle = new Rectangle(0, num2, texture2D.Width, num1);
			var vector2 = rectangle.Size() / 2f;
			var color = projectile.ai[1] <= 3.0
				? Color.Lerp(new Color(byte.MaxValue, 95, 46, 50), new Color(150, 35, 0, 100),
					(float) ((3.0 - projectile.ai[1]) / 3.0))
				: Color.Lerp(new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0),
					new Color(byte.MaxValue, 95, 46, 50), (float) ((7.0 - projectile.ai[1]) / 4.0));
			Main.spriteBatch.Draw(texture2D,
				projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), rectangle, color,
				projectile.rotation, vector2, projectile.scale, 0, 0.0f);
			return false;
		}
	}
}