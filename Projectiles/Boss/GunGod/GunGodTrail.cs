using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class GunGodTrail : ModProjectile
	{
		public override string Texture => "Albedo/NPCs/Boss/GunGod/GunGod";
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("GunGod");
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 50;
			projectile.aiStyle = -1;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			NPC val = AlbedoUtils.NpcExists(projectile.ai[1]);
			if (val != null)
			{
				projectile.Center = val.Center;
				projectile.alpha = val.alpha;
				projectile.direction = projectile.spriteDirection = val.direction;
				projectile.timeLeft = 30;
				projectile.localAI[0] = val.localAI[3];
				if (!Main.dedServ)
				{
					projectile.frame = (int)(val.frame.Y / (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[this.projectile.type]));
				}
			}
			else if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				projectile.Kill();
			}
		}

		public override bool CanDamage()
		{
			return false;
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
			SpriteEffects effects = ((projectile.spriteDirection >= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			bool flag = projectile.localAI[0] > 1f;
			if (flag)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
				GameShaders.Armor.GetShaderFromItemId(1016).Apply(projectile);
			}
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
			{
				Color color2;
				if (flag)
				{
					color2 = Color.White;
					color2 *= 0.5f + 0.5f * (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
					color2.A = 0;
				}
				else
				{
					color2 = color;
					color2 *= (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[projectile.type];
				}
				Vector2 vector = projectile.oldPos[i];
				float rotation = projectile.oldRot[i];
				Main.spriteBatch.Draw(texture2D, vector + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color2, rotation, origin, projectile.scale, effects, 0f);
			}
			if (flag)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
			}
			Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, effects, 0f);
			return false;
		}
	}
}
