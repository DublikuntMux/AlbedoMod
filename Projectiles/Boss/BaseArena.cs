using System;
using Albedo.Global;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Albedo.Projectiles
{
	public abstract class BaseArena : ModProjectile
	{
		protected float rotationPerTick;

		protected readonly int npcType;

		protected readonly int dustType;

		protected readonly int increment;

		protected float threshold;

		protected float targetPlayer;

		protected BaseArena(float rotationPerTick, float threshold, int npcType, int dustType = 135, int increment = 2)
		{
			this.rotationPerTick = rotationPerTick;
			this.threshold = threshold;
			this.npcType = npcType;
			this.dustType = dustType;
			this.increment = increment;
		}

		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.hostile = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 600;
			cooldownSlot = 0;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			projectile.hide = true;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 3;
		}

		public override bool CanDamage()
		{
			return projectile.alpha == 0;
		}

		public override bool CanHitPlayer(Player target)
		{
			if (targetPlayer == target.whoAmI)
			{
				return target.hurtCooldowns[cooldownSlot] == 0;
			}
			return false;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return Math.Abs((Utils.ToVector2(targetHitbox.Center) - Utils.ToVector2(projHitbox.Center)).Length() - threshold) < projectile.width / 2 * projectile.scale;
		}

		protected virtual void Movement(NPC npc)
		{
		}

		public override void AI()
		{
			NPC val = AlbedoUtils.NpcExists(projectile.ai[1], npcType);
			if (val != null)
			{
				Projectile projectile1 = projectile;
				projectile1.alpha -= increment;
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
				Movement(val);
				targetPlayer = val.target;
				Player localPlayer = Main.LocalPlayer;
				if (localPlayer.active && !localPlayer.dead && !localPlayer.ghost)
				{
					float num = localPlayer.Distance(projectile.Center);
					if (num > threshold && num < threshold * 4f)
					{
						if (num > threshold * 2f)
						{
							localPlayer.controlLeft = false;
							localPlayer.controlRight = false;
							localPlayer.controlUp = false;
							localPlayer.controlDown = false;
							localPlayer.controlUseItem = false;
							localPlayer.controlUseTile = false;
							localPlayer.controlJump = false;
							localPlayer.controlHook = false;
							if (localPlayer.mount.Active)
							{
								localPlayer.mount.Dismount(localPlayer);
							}
							localPlayer.velocity.X = 0f;
							localPlayer.velocity.Y = -0.4f;
						}
						Vector2 vector = projectile.Center - localPlayer.Center;
						float num2 = vector.Length() - threshold;
						vector.Normalize();
						vector *= ((num2 < 17f) ? num2 : 17f);
						localPlayer.position += vector;
						for (int i = 0; i < 20; i++)
						{
							int num3 = Dust.NewDust(localPlayer.position, localPlayer.width, localPlayer.height, dustType, 0f, 0f, 0, default(Color), 2.5f);
							Main.dust[num3].noGravity = true;
							Dust obj = Main.dust[num3];
							obj.velocity *= 5f;
						}
					}
				}
			}
			else
			{
				projectile.velocity = Vector2.Zero;
				Projectile projectile2 = projectile;
				projectile2.alpha += increment;
				if (projectile.alpha > 255)
				{
					projectile.Kill();
					return;
				}
			}
			projectile.timeLeft = 2;
			projectile.scale = (1f - projectile.alpha / 255f) * 2f;
			projectile.ai[0] += rotationPerTick;
			if (projectile.ai[0] > (float)Math.PI)
			{
				projectile.ai[0] -= (float)Math.PI * 2f;
				projectile.netUpdate = true;
			}
			else if (projectile.ai[0] < -(float)Math.PI)
			{
				projectile.ai[0] += (float)Math.PI * 2f;
				projectile.netUpdate = true;
			}
			projectile.localAI[0] = threshold;
		}

		public override void PostAI()
		{
			projectile.hide = false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.velocity = target.DirectionTo(projectile.Center) * 4f;
		}

		public override void Kill(int timeLeft)
		{
			float num = (255f - projectile.alpha) / 255f;
			float num2 = threshold * num;
			int num3 = (int)(300f * num);
			for (int i = 0; i < num3; i++)
			{
				int num4 = Dust.NewDust(projectile.Center, 0, 0, dustType, 0f, 0f, 0, default(Color), 4f);
				Dust obj = Main.dust[num4];
				obj.velocity *= 6f;
				Main.dust[num4].noGravity = true;
				Main.dust[num4].position = projectile.Center + num2 * Utils.RotatedByRandom(Vector2.UnitX, Math.PI * 2.0);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * projectile.Opacity * ((targetPlayer == Main.myPlayer) ? 1f : 0.15f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			Color alpha = projectile.GetAlpha(lightColor);
			for (int i = 0; i < 32; i++)
			{
				int num2 = (projectile.frame + i) % Main.projFrames[projectile.type];
				int y = num * num2;
				Rectangle rectangle = new Rectangle(0, y, texture2D.Width, num);
				Vector2 origin = Utils.Size(rectangle) / 2f;
				float num3 = (float)Math.PI / 16f * i + projectile.ai[0];
				Vector2 vector = Utils.RotatedBy(new Vector2(threshold * projectile.scale / 2f, 0f), (double)projectile.ai[0], default(Vector2));
				vector = Utils.RotatedBy(vector, (double)((float)Math.PI / 16f * i), default(Vector2));
				for (int j = 0; j < 4; j++)
				{
					Color color = alpha;
					color *= (4 - j) / 4f;
					Vector2 vector2 = projectile.Center + Utils.RotatedBy(vector, (double)(rotationPerTick * -j), default(Vector2));
					float rotation = num3 + projectile.rotation;
					Main.spriteBatch.Draw(texture2D, vector2 - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, rotation, origin, projectile.scale, SpriteEffects.None, 0f);
				}
				float rotation2 = num3 + projectile.rotation;
				Main.spriteBatch.Draw(texture2D, projectile.Center + vector - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, alpha, rotation2, origin, this.projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
