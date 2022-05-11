using System;
using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunMaster
{
	public class Flaming : ModProjectile
	{
		public override string Texture => "Terraria/Item_95";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scythe");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 80;
			projectile.height = 80;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 720;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.scale = 2.5f;
			cooldownSlot = 1;
		}

		public override bool CanDamage()
		{
			return projectile.ai[1] <= 0f;
		}

		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				projectile.localAI[0] = (Main.rand.NextBool() ? 1 : (-1));
				projectile.localAI[1] = projectile.ai[1] - projectile.ai[0];
				projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			}
			if ((projectile.ai[0] -= 1f) == 0f)
			{
				projectile.netUpdate = true;
				projectile.velocity = Vector2.Zero;
			}
			if ((projectile.ai[1] -= 1f) == 0f)
			{
				projectile.netUpdate = true;
				Player val = Main.player[Player.FindClosest(projectile.position, projectile.width, projectile.height)];
				projectile.velocity = projectile.DirectionTo(val.Center);
				if (AlbedoUtils.BossIsAlive(ref AlbedoGlobalNpc.GunMaster, ModContent.NPCType<NPCs.Boss.GunMaster.GunMaster>()) && Main.npc[AlbedoGlobalNpc.GunMaster].localAI[3] > 1f)
				{
					Projectile projectile1 = projectile;
					projectile1.velocity *= 7f;
				}
				else
				{
					Projectile projectile2 = projectile;
					projectile2.velocity *= 24f;
				}
				Main.PlaySound(SoundID.Item84, projectile.Center);
			}
			float num = ((projectile.ai[0] < 0f && projectile.ai[1] > 0f) ? (1f - projectile.ai[1] / projectile.localAI[1]) : 0.8f);
			Projectile projectile3 = projectile;
			projectile3.rotation += num * projectile.localAI[0];
		}

		public override void Kill(int timeLeft)
		{
			int num = 20;
			float num2 = 12f;
			for (int i = 0; i < num; i++)
			{
				int num3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 87, 0f, 0f, 0, default(Color), 3.5f);
				Dust obj = Main.dust[num3];
				obj.velocity *= num2;
				Main.dust[num3].noGravity = true;
			}
			for (int j = 0; j < num; j++)
			{
				int num4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 3.5f);
				Dust obj2 = Main.dust[num4];
				obj2.velocity *= num2;
				Main.dust[num4].noGravity = true;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(24, 900);
			target.AddBuff(33, 900);
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
			return new Color(255, 255, 255, (projectile.ai[1] < 0f) ? 150 : 255) * projectile.Opacity * ((projectile.ai[1] < 0f) ? 1f : 0.5f);
		}
	}
}
