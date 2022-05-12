using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class FrostShard : ModProjectile
	{
		public override string Texture => "Terraria/Item_949";

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 1;
			aiType = 349;
			projectile.hostile = true;
			projectile.timeLeft = 360;
			projectile.extraUpdates = 1;
			projectile.coldDamage = true;
		}

		public override void AI()
		{
			Projectile projectile1 = projectile;
			projectile1.frameCounter++;
			if (projectile.frameCounter > 4)
			{
				projectile.frameCounter = 0;
				Projectile projectile2 = projectile;
				projectile2.frame++;
				if (projectile.frame > 5)
				{
					projectile.frame = 0;
				}
			}
			projectile.velocity.X *= 0.95f;
			projectile.position.Y += projectile.velocity.Y / 2f;
			if (AlbedoUtils.BossIsAlive(ref AlbedoGlobalNpc.GunGod, ModContent.NPCType<NPCs.Boss.GunGod.GunGod>()) && projectile.position.Y > Main.npc[AlbedoGlobalNpc.GunGod].Center.Y + ((Main.npc[AlbedoGlobalNpc.GunGod].localAI[3] == 1f) ? 2000 : 1400))
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, projectile.position);
			for (int i = 0; i < 3; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].noLight = true;
				Main.dust[num].scale = 0.7f;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(44, 120);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(200, 200, 200, projectile.alpha);
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
	}
}
