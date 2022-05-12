using System;
using System.Linq;
using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class GunGodSword : BaseDeathray
	{
		public int Counter;

		public bool SpawnedHandle;

		public GunGodSword()
			: base(300f, "GunGodDeathray")
		{
		}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
			projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			NPC val = AlbedoUtils.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
			if (val == null)
			{
				projectile.Kill();
				return;
			}

			projectile.Center = val.Center;
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.localAI[0] == 0f)
			{
				Main.PlaySound(SoundID.Zombie, (int)projectile.position.X, (int)projectile.position.Y, 104);
			}
			float num = 1f;
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= maxTime)
			{
				projectile.Kill();
				return;
			}
			projectile.scale = (float)Math.Sin(projectile.localAI[0] * (float)Math.PI / maxTime) * num * 6f;
			if (projectile.scale > num)
			{
				projectile.scale = num;
			}
			float num2 = projectile.velocity.ToRotation();
			if ((val.velocity != Vector2.Zero || val.ai[0] == 19f) && val.ai[0] != 20f)
			{
				num2 += projectile.ai[0] / projectile.MaxUpdates;
			}
			projectile.rotation = num2 - (float)Math.PI / 2f;
			projectile.velocity = num2.ToRotationVector2();
			float num3 = 3f;
			_ = projectile.width;
			float[] array = new float[(int)num3];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 3000f;
			}
			float num4 = array.Sum();
			num4 /= num3;
			float amount = 0.5f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num4, amount);
			if (projectile.localAI[0] % 2f == 0f)
			{
				if (val.velocity != Vector2.Zero && --Counter < 0)
				{
					Counter = 5;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Vector2 center = projectile.Center;
						Vector2 vector2 = Utils.RotatedBy(projectile.velocity, Math.PI / 2.0 * Math.Sign(projectile.ai[0]), default(Vector2));
						for (int k = 1; k <= 15; k++)
						{
							center += projectile.velocity * 3000f / 15f;
							Projectile.NewProjectile(center, vector2, ModContent.ProjectileType<Sickle2>(), projectile.damage, 0f, projectile.owner, 0f, 0f);
						}
					}
				}
				for (int l = 0; l < 2; l++)
				{
					int num5 = Dust.NewDust(projectile.position + projectile.velocity * Main.rand.NextFloat(3000f), projectile.width, projectile.height, 87, 0f, 0f, 0, Color.White, 6f);
					Main.dust[num5].noGravity = true;
					Dust obj = Main.dust[num5];
					obj.velocity *= 4f;
				}
				Projectile projectile1 = projectile;
				if (++projectile1.frameCounter > 3)
				{
					projectile.frameCounter = 0;
					Projectile projectile2 = projectile;
					if (++projectile2.frame > 10)
					{
						projectile.frame = 0;
					}
				}
			}
			if (!SpawnedHandle)
			{
				SpawnedHandle = true;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(projectile.Center, projectile.velocity, ModContent.ProjectileType<GunGodSwordHandle>(), projectile.damage, projectile.knockBack, projectile.owner, (float)Math.PI / 2f, projectile.identity);
					Projectile.NewProjectile(projectile.Center, projectile.velocity, ModContent.ProjectileType<GunGodSwordHandle>(), projectile.damage, projectile.knockBack, projectile.owner, -(float)Math.PI / 2f, projectile.identity);
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.velocity.X = target.Center.X < Main.npc[(int)projectile.ai[1]].Center.X ? (-15f) : 15f;
			target.velocity.Y = -10f;
			Projectile.NewProjectile(target.Center + Main.rand.NextVector2Circular(100f, 100f), Vector2.Zero, ModContent.ProjectileType<Blast>(), 0, 0f, projectile.owner);
			target.AddBuff(195, 600);
			target.AddBuff(196, 600);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D texture2D = mod.GetTexture("Projectiles/Boss/Deathrays/GunGod/GunGodDeathray_" + projectile.frame);
			Texture2D texture2D2 = mod.GetTexture("Projectiles/Boss/Deathrays/GunGod/GunGodDeathray2_" + projectile.frame);
			Texture2D texture2D3 = mod.GetTexture("Projectiles/Boss/Deathrays/" + texture + "3");
			float num = projectile.localAI[1];
			Color color = new Color(255, 255, 255, 0) * 0.95f;
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D texture2D4 = texture2D;
			Vector2 position = projectile.Center - Main.screenPosition;
			spriteBatch2.Draw(texture2D4, position, null, color, projectile.rotation, Utils.Size(texture2D) / 2f, projectile.scale, SpriteEffects.None, 0f);
			num -= (texture2D.Height / 2 + texture2D3.Height) * projectile.scale;
			Vector2 center = projectile.Center;
			center += projectile.velocity * projectile.scale * texture2D.Height / 2f;
			if (num > 0f)
			{
				float num2 = 0f;
				Rectangle value = new Rectangle(0, 0, texture2D2.Width, 30);
				while (num2 + 1f < num)
				{
					if (num - num2 < (float)value.Height)
					{
						value.Height = (int)(num - num2);
					}
					Main.spriteBatch.Draw(texture2D2, center - Main.screenPosition, value, color, projectile.rotation, new Vector2(value.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
					num2 += (float)value.Height * projectile.scale;
					center += projectile.velocity * value.Height * projectile.scale;
					value.Y += 30;
					if (value.Y + value.Height > texture2D2.Height)
					{
						value.Y = 0;
					}
				}
			}
			SpriteBatch spriteBatch3 = Main.spriteBatch;
			Texture2D texture2D5 = texture2D3;
			Vector2 position2 = center - Main.screenPosition;
			spriteBatch3.Draw(texture2D5, position2, null, color, projectile.rotation, Utils.Frame(texture2D3, 1, 1, 0, 0).Top(), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
