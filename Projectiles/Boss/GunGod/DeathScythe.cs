using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class DeathScythe : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_274";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scythe");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
			projectile.aiStyle = -1;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override bool CanDamage()
		{
			return false;
		}

		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				projectile.localAI[0] = !Main.rand.NextBool() ? 1 :-1;
				Main.PlaySound(SoundID.Item71, projectile.Center);
			}
			projectile.velocity.X *= 0.96f;
			projectile.velocity.Y -= 0.6f;
			Projectile projectile1 = projectile;
			projectile1.rotation += projectile.localAI[0];
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
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * projectile.Opacity;
		}
	}
}
