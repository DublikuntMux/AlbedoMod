using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Albedo.Global;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class GlowLine : ModProjectile
	{
		public Color color = Color.White;

		private int _counter;

		private int drawLayers = 1;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glow Line");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.alpha = 255;
			projectile.hide = true;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool CanDamage()
		{
			return false;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(_counter);
			writer.Write(projectile.localAI[0]);
			writer.Write(projectile.localAI[1]);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			_counter = reader.ReadInt32();
			projectile.localAI[0] = reader.ReadSingle();
			projectile.localAI[1] = reader.ReadSingle();
		}

		public override void AI()
		{
			int num = 60;
			float num2 = 3f;
			switch ((int)this.projectile.ai[0])
			{
			case 0:
			{
				color = Color.Yellow;
				num = 30;
				num2 = 10f;
				NPC val13 = AlbedoUtils.NpcExists(projectile.localAI[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
				if (val13 != null)
				{
					projectile.Center = val13.Center;
					projectile.rotation = val13.DirectionTo(Main.player[val13.target].Center).ToRotation() + projectile.ai[1];
				}
				break;
			}
			case 1:
				color = Color.Yellow;
				num = 150;
				projectile.rotation = projectile.ai[1];
				num2 = 1f;
				if (_counter < 90)
				{
					num2 = 0f;
				}
				else
				{
					projectile.velocity = Vector2.Zero;
				}
				break;
			case 2:
				color = Color.HotPink;
				num = 90;
				this.projectile.rotation = this.projectile.ai[1];
				num2 = 1f;
				if (this.projectile.velocity != Vector2.Zero)
				{
					if (_counter == 0)
					{
						projectile.localAI[1] = (0f - projectile.velocity.Length()) / num;
					}
					float num6 = projectile.velocity.Length();
					num6 += projectile.localAI[1];
					projectile.velocity = Vector2.Normalize(projectile.velocity) * num6;
				}
				break;
			case 3:
			{
				color = Color.Yellow;
				num = 60;
				num2 = 6f;
				NPC val10 = AlbedoUtils.NpcExists(projectile.localAI[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
				if (val10 != null)
				{
					projectile.Center = val10.Center;
					if (_counter == 0)
					{
						projectile.rotation = val10.DirectionTo(Main.player[val10.target].Center).ToRotation();
					}
					float num5;
					for (num5 = val10.DirectionTo(Main.player[val10.target].Center).ToRotation() + projectile.ai[1]; num5 < -(float)Math.PI; num5 += (float)Math.PI * 2f)
					{
					}
					while (num5 > (float)Math.PI)
					{
						num5 -= (float)Math.PI * 2f;
					}
					projectile.rotation = projectile.rotation.AngleLerp(num5, 0.05f);
				}
				break;
			}
			case 4:
			{
				color = Color.Yellow;
				num = 150;
				num2 = 7f;
				NPC val6 = AlbedoUtils.NpcExists(projectile.localAI[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
				if (val6 != null)
				{
					projectile.Center = val6.Center;
					float num4;
					for (num4 = projectile.ai[1]; num4 < -(float)Math.PI; num4 += (float)Math.PI * 2f)
					{
					}
					while (num4 > (float)Math.PI)
					{
						num4 -= (float)Math.PI * 2f;
					}
					projectile.velocity = projectile.velocity.ToRotation().AngleLerp(num4, 0.05f).ToRotationVector2();
				}
				Projectile projectile5 = projectile;
				projectile5.position -= projectile.velocity;
				projectile.rotation = projectile.velocity.ToRotation();
				break;
			}
			case 5:
			{
				color = new Color(0f, 1f, 1f);
				num = 150;
				num2 = 10f;
				NPC val12 = AlbedoUtils.NpcExists(projectile.localAI[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
				if (val12 != null)
				{
					Vector2 value = val12.Center + Vector2.UnitX * projectile.ai[1];
					projectile.Center = Vector2.Lerp(projectile.Center, value, 0.025f);
				}
				Projectile projectile9 = projectile;
				projectile9.position -= projectile.velocity;
				projectile.rotation = projectile.velocity.ToRotation();
				break;
			}
			case 6:
			{
				color = new Color(51, 255, 191);
				num = 90;
				Player val9 = AlbedoUtils.PlayerExists(projectile.ai[1]);
				if (val9 != null)
				{
					projectile.rotation = projectile.DirectionTo(val9.Center).ToRotation();
				}
				else
				{
					projectile.ai[1] = Player.FindClosest(projectile.Center, 0, 0);
				}
				Projectile projectile6 = projectile;
				projectile6.position -= projectile.velocity;
				Projectile projectile7 = projectile;
				projectile7.rotation += projectile.velocity.ToRotation();
				break;
			}
			case 7:
			{
				switch ((int)projectile.ai[1])
				{
				case 0:
					color = Color.Magenta;
					break;
				case 1:
					color = Color.Orange;
					break;
				case 2:
					color = new Color(51, 255, 191);
					break;
				default:
					color = Color.SkyBlue;
					break;
				}
				num = 20;
				num2 = 2f;
				Projectile projectile8 = projectile;
				projectile8.position -= projectile.velocity;
				projectile.rotation = projectile.velocity.ToRotation();
				break;
			}
			case 8:
			{
				color = Color.Yellow;
				num = 60;
				NPC val4 = AlbedoUtils.NpcExists(projectile.ai[1], 128, 131, 129, 130);
				if (val4 != null)
				{
					projectile.Center = val4.Center;
					projectile.rotation = val4.rotation + (float)Math.PI / 2f;
					Projectile projectile2 = projectile;
					projectile2.position -= projectile.velocity;
					Projectile projectile3 = projectile;
					projectile3.rotation += projectile.velocity.ToRotation();
					break;
				}
				projectile.Kill();
				return;
			}
			case 9:
			{
				color = Color.Red;
				num = 120;
				num2 = 2f;
				NPC val2 = AlbedoUtils.NpcExists(projectile.ai[1], 125);
				if (val2 != null)
				{
					Vector2 vector = Utils.RotatedBy(new Vector2(val2.width - 24, 0f), val2.rotation + 1.57079633, default(Vector2));
					projectile.Center = val2.Center + vector;
					projectile.rotation = val2.rotation + (float)Math.PI / 2f;
					break;
				}
				projectile.Kill();
				return;
			}
			case 10:
			{
				color = Color.Purple;
				num = 90;
				num2 = 2f;
				NPC val3 = AlbedoUtils.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
				if (val3 != null)
				{
					projectile.Center = val3.Center;
					projectile.rotation = val3.localAI[0];
					break;
				}
				projectile.Kill();
				return;
			}
			}
			if (++_counter > num)
			{
				projectile.Kill();
				return;
			}
			if (num2 >= 0f)
			{
				projectile.alpha = 255 - (int)(255.0 * Math.Sin(Math.PI / num * _counter) * num2);
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
			}
			color.A = 0;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return color * ((ModProjectile)this).projectile.Opacity * ((float)(int)Main.mouseTextColor / 255f) * 0.9f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y = num * projectile.frame;
			Rectangle rectangle = new Rectangle(0, y, texture2D.Width, num);
			Vector2 origin = Utils.Size(rectangle) / 2f;
			Vector2 vector = projectile.rotation.ToRotationVector2() * 3000f / 2f;
			Vector2 vector2 = projectile.Center - Main.screenLastPosition + new Vector2(0f, projectile.gfxOffY) + vector;
			Rectangle destinationRectangle = new Rectangle((int)vector2.X, (int)vector2.Y, 3000, (int)((float)rectangle.Height * projectile.scale));
			Color alpha = projectile.GetAlpha(lightColor);
			for (int i = 0; i < drawLayers; i++)
			{
				Main.spriteBatch.Draw(texture2D, destinationRectangle, rectangle, alpha, projectile.rotation, origin, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
