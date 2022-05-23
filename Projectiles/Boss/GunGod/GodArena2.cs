using System;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class GodArena2 : ModProjectile
	{
		public override string Texture => "Terraria/Item_98";

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 255;
		}

		public override void AI()
		{
			var val = EnemyHelper.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
			if (val != null) {
				projectile.alpha -= 2;
				if (projectile.alpha < 0) projectile.alpha = 0;
				projectile.Center = val.Center;
			}
			else {
				projectile.velocity = Vector2.Zero;
				var projectile2 = projectile;
				projectile2.alpha += 2;
				if (projectile.alpha > 255) {
					projectile.Kill();
					return;
				}
			}

			projectile.timeLeft = 2;
			projectile.scale = 1f - projectile.alpha / 255f;
			projectile.ai[0] -= (float) Math.PI / 57f;
			if (projectile.ai[0] < -(float) Math.PI) {
				projectile.ai[0] += (float) Math.PI * 2f;
				projectile.netUpdate = true;
			}

			projectile.rotation += 0.3f;
		}

		public override bool CanDamage() => false;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y = num * projectile.frame;
			var rectangle = new Rectangle(0, y, texture2D.Width, num);
			var origin = rectangle.Size() / 2f;
			var alpha = projectile.GetAlpha(lightColor);
			for (int i = 0; i < 9; i++) {
				var vector = new Vector2(150f * projectile.scale / 2f, 0f).RotatedBy(projectile.ai[0]);
				float num2 = (float) Math.PI * 2f / 9f * i;
				vector = vector.RotatedBy(num2);
				Main.spriteBatch.Draw(texture2D,
					projectile.Center + vector - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle,
					alpha, num2 + projectile.ai[0] + (float) Math.PI / 2f, origin, projectile.scale,
					SpriteEffects.None, 0f);
			}

			return false;
		}

		public override Color? GetAlpha(Color lightColor) => Color.White * projectile.Opacity;
	}
}