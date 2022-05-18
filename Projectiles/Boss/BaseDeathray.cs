using Albedo.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss
{
	public abstract class BaseDeathray : ModProjectile
	{
		protected readonly float hitboxModifier;

		protected readonly string texture;

		protected readonly float transparency;
		protected float maxTime;

		protected BaseDeathray(float maxTime, string texture, float transparency = 0f, float hitboxModifier = 1f)
		{
			this.maxTime = maxTime;
			this.texture = texture;
			this.transparency = transparency;
			this.hitboxModifier = hitboxModifier;
		}

		public override void SetDefaults()
		{
			projectile.width = 48;
			projectile.height = 48;
			projectile.hostile = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 600;
			cooldownSlot = 1;
			projectile.hide = true;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 1;
		}

		public override void PostAI()
		{
			if (projectile.hide) {
				projectile.hide = false;
				if (projectile.friendly)
					projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.velocity == Vector2.Zero) return false;
			var texture2D = Main.projectileTexture[projectile.type];
			var texture2D2 = mod.GetTexture("Projectiles/Boss/Deathrays/" + texture + "2");
			var texture2D3 = mod.GetTexture("Projectiles/Boss/Deathrays/" + texture + "3");
			float num = projectile.localAI[1];
			var value = new Color(255, 255, 255, 0) * 0.95f;
			value = Color.Lerp(value, Color.Transparent, transparency);
			var spriteBatch2 = Main.spriteBatch;
			var texture2D4 = texture2D;
			var position = projectile.Center - Main.screenPosition;
			spriteBatch2.Draw(texture2D4, position, null, value, projectile.rotation, texture2D.Size() / 2f,
				projectile.scale, SpriteEffects.None, 0f);
			num -= (texture2D.Height / 2 + texture2D3.Height) * projectile.scale;
			var center = projectile.Center;
			center += projectile.velocity * projectile.scale * texture2D.Height / 2f;
			if (num > 0f) {
				float num2 = 0f;
				var value2 = new Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), texture2D2.Width, 16);
				while (num2 + 1f < num) {
					if (num - num2 < value2.Height) value2.Height = (int) (num - num2);
					Main.spriteBatch.Draw(texture2D2, center - Main.screenPosition, value2, value, projectile.rotation,
						new Vector2(value2.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
					num2 += value2.Height * projectile.scale;
					center += projectile.velocity * value2.Height * projectile.scale;
					value2.Y += 16;
					if (value2.Y + value2.Height > texture2D2.Height) value2.Y = 0;
				}
			}

			var spriteBatch3 = Main.spriteBatch;
			var texture2D5 = texture2D3;
			var position2 = center - Main.screenPosition;
			spriteBatch3.Draw(texture2D5, position2, null, value, projectile.rotation, texture2D3.Frame().Top(),
				projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = (TileCuttingContext) 2;
			var velocity = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + velocity * projectile.localAI[1],
				projectile.width * projectile.scale, DelegateMethods.CutTiles);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (projHitbox.Intersects(targetHitbox)) return true;
			float num = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
				projectile.Center + projectile.velocity * projectile.localAI[1],
				22f * projectile.scale * hitboxModifier, ref num);
		}
	}
}