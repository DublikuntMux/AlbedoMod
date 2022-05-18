using System;
using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class SDFMG : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 74;
			projectile.scale = 1f;
			projectile.hostile = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 60;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
			projectile.hide = true;
			projectile.scale = 2.5f;
		}

		public override void AI()
		{
			projectile.hide = false;
			var val = BossHelper.NpcExists(projectile.ai[0], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
			if (val != null) {
				if (val.ai[0] == 0f) projectile.extraUpdates = 1;
				if (projectile.localAI[0] == 0f) projectile.localAI[1] = projectile.ai[1] / 60f;
				projectile.ai[1] -= projectile.localAI[1];
				projectile.Center = val.Center;
				if (projectile.localAI[0] == 0f) {
					projectile.localAI[0] = 1f;
					Main.PlaySound(SoundID.Item10, projectile.Center);
				}

				projectile.Opacity = (float) Math.Min(1.0,
					(2 - projectile.extraUpdates) * Math.Sin(Math.PI * (60 - projectile.timeLeft) / 60.0));
				projectile.direction = projectile.spriteDirection = Math.Sign(projectile.ai[1]);
				projectile.rotation = projectile.velocity.ToRotation() +
				                      MathHelper.ToRadians(projectile.direction < 0 ? 135 : 45);
			}
			else {
				projectile.Kill();
			}
		}


		public override void OnHitPlayer(Player target, int damage, bool crit) => target.AddBuff(30, 600);

		public override Color? GetAlpha(Color lightColor)
		{
			var value = lightColor * projectile.Opacity;
			value.A = (byte) (255f * projectile.Opacity);
			return value;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y = num * projectile.frame;
			var rectangle = new Rectangle(0, y, texture2D.Width, num);
			var origin = rectangle.Size() / 2f;
			var effects = projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			var color = lightColor;
			color = projectile.GetAlpha(color);
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++) {
				var color2 = color * 0.5f;
				color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) /
				          (float) ProjectileID.Sets.TrailCacheLength[projectile.type];
				var vector = projectile.oldPos[i];
				float rotation = projectile.oldRot[i];
				Main.spriteBatch.Draw(texture2D,
					vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
					rectangle, color2, rotation, origin, projectile.scale, effects, 0f);
			}

			Main.spriteBatch.Draw(texture2D,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color,
				projectile.rotation, origin, projectile.scale, effects, 0f);
			return false;
		}
	}
}