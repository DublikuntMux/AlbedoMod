using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Sickle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.alpha = 100;
			projectile.hostile = true;
			projectile.timeLeft = 300;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			cooldownSlot = 1;
		}

		public override void AI()
		{
			if (this.projectile.localAI[0] == 0f)
			{
				this.projectile.localAI[0] = 1f;
				Main.PlaySound(SoundID.Item8, this.projectile.Center);
			}
			Projectile projectile = this.projectile;
			projectile.rotation += 0.8f;
			if ((this.projectile.localAI[1] += 1f) < 90f)
			{
				Projectile projectile2 = this.projectile;
				projectile2.velocity *= 1.045f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y = num * projectile.frame;
			Rectangle rectangle = new Rectangle(0, y, texture2D.Width, num);
			Vector2 origin = Utils.Size(rectangle) / 2f;
			Color color = lightColor;
			color = projectile.GetAlpha(color);
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
			{
				Color color2 = color;
				color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[projectile.type];
				Vector2 vector = projectile.oldPos[i];
				float rotation = projectile.oldRot[i];
				Main.spriteBatch.Draw(texture2D, vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color2, rotation, origin, projectile.scale, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, this.projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(30, 600, true);
		}
	}
}
