using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Laser : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_466";

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 5;
			projectile.height = 5;
			projectile.aiStyle = 1;
			aiType = 449;
			projectile.hostile = true;
			projectile.alpha = byte.MaxValue;
			projectile.extraUpdates = 2;
			projectile.timeLeft = 360;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.scale = 0.3f;
			cooldownSlot = 1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 4; i++) {
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 222);
				var obj = Main.dust[num];
				obj.velocity *= 2f;
			}
		}

		public override Color? GetAlpha(Color lightColor) => Color.White;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture2D = Main.projectileTexture[projectile.type];
			var bounds = texture2D.Bounds;
			var origin = bounds.Size() / 2f;
			var alpha = projectile.GetAlpha(lightColor);
			for (int i = 1; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
				if (!(projectile.oldPos[i] == Vector2.Zero) && !(projectile.oldPos[i - 1] == projectile.oldPos[i])) {
					var vector = projectile.oldPos[i - 1] - projectile.oldPos[i];
					int num = (int) vector.Length();
					vector.Normalize();
					var color = alpha;
					color *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) /
					         (float) ProjectileID.Sets.TrailCacheLength[projectile.type];
					for (int j = 0; j < num; j += 3) {
						var vector2 = projectile.oldPos[i] + vector * j;
						Main.spriteBatch.Draw(texture2D,
							vector2 + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
							bounds, color, projectile.rotation, origin, projectile.scale,
							SpriteEffects.FlipHorizontally, 0f);
					}
				}

			return false;
		}
	}
}