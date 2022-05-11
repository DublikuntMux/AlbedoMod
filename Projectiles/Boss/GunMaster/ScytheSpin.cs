using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunMaster
{
	public class ScytheSpin : ModProjectile
	{
		public override string Texture => "Terraria/Item_98";

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 420;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			cooldownSlot = 1;
		}

		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				projectile.localAI[0] = 1f;
				Main.PlaySound(SoundID.Item71, projectile.Center);
			}
			if (projectile.timeLeft == 390)
			{
				projectile.velocity = Vector2.Zero;
				projectile.netUpdate = true;
			}
			else if (projectile.timeLeft == 360)
			{
				Main.PlaySound(SoundID.Item84, projectile.Center);
			}
			else if (projectile.timeLeft < 360)
			{
				NPC val = AlbedoUtils.NpcExists(projectile.ai[0], ModContent.NPCType<NPCs.Boss.GunMaster.GunMaster>());
				if (val == null)
				{
					projectile.Kill();
					return;
				}
				Vector2 center = val.Center;
				projectile.velocity = Utils.RotatedBy(center - projectile.Center, Math.PI / 2.0 * projectile.ai[1], default(Vector2));
				Projectile projectile1 = projectile;
				projectile1.velocity *= (float)Math.PI / 180f;
			}
			projectile.spriteDirection = (int)projectile.ai[1];
			Projectile projectile2 = projectile;
			projectile2.rotation += projectile.spriteDirection * 0.5f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item71, projectile.Center);
			for (int i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Main.dust[num].noLight = true;
				Dust obj = Main.dust[num];
				obj.scale += 1f;
				Dust obj2 = Main.dust[num];
				obj2.velocity *= 4f;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int num2 = Player.FindClosest(projectile.Center, 0, 0);
				if (num2 != -1)
				{
					Vector2 vector = 15f * projectile.DirectionTo(Main.player[num2].Center);
					Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<Sickle3>(), projectile.damage, projectile.knockBack, projectile.owner, (float)num2, 0f);
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(30, 600);
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
			SpriteEffects effects = ((projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
			{
				Color color2 = color;
				color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[projectile.type];
				Vector2 vector = projectile.oldPos[i];
				float rotation = projectile.oldRot[i];
				Main.spriteBatch.Draw(texture2D, vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color2, rotation, origin, projectile.scale, effects, 0f);
			}
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, effects, 0f);
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}
