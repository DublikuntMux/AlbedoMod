using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Combined
{
    public class CosmoBulletProjectile : ModProjectile
    {
        private int _bounce = 25;

		private readonly int[] _dusts = {130, 131, 132, 133, 134, 135};

		private int _currentDust;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmo Bullet");
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 200;
			projectile.alpha = 255;
			projectile.light = 0.5f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = 14;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 2;
		}

		public override void AI()
		{
			for (int i = 0; i < 7; i++)
			{
				float x = projectile.position.X - projectile.velocity.X / 10f * i;
				float y = projectile.position.Y - projectile.velocity.Y / 10f * i;
				int num = Dust.NewDust(new Vector2(x, y), 1, 1, _dusts[_currentDust], 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].alpha = projectile.alpha;
				Main.dust[num].position.X = x;
				Main.dust[num].position.Y = y;
				Dust obj = Main.dust[num];
				obj.velocity *= 0f;
				Main.dust[num].noGravity = true;
			}
			_currentDust++;
			if (_currentDust > 5)
			{
				_currentDust = 0;
			}
			float num2 = (float) Math.Sqrt(projectile.velocity.X * projectile.velocity.X +
			                               projectile.velocity.Y * projectile.velocity.Y);
			float num3 = projectile.localAI[0];
			if (num3 == 0f)
			{
				projectile.localAI[0] = num2;
				num3 = num2;
			}
			if (projectile.alpha > 0)
			{
				projectile.alpha -= 25;
			}
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			float num4 = projectile.position.X;
			float num5 = projectile.position.Y;
			float num6 = 300f;
			bool flag = false;
			int num7 = 0;
			if (projectile.ai[1] == 0f)
			{
				for (int j = 0; j < 200; j++)
				{
					if (Main.npc[j].CanBeChasedBy(projectile) &&
					    (projectile.ai[1] == 0f || projectile.ai[1] == (j + 1)))
					{
						float num8 = Main.npc[j].position.X + (Main.npc[j].width / 2f);
						float num9 = Main.npc[j].position.Y + (Main.npc[j].height / 2f);
						float num10 = Math.Abs(projectile.position.X + projectile.width / 2f - num8) +
						              Math.Abs(projectile.position.Y + projectile.height / 2f - num9);
						if (num10 < num6 &&
						    Collision.CanHit(
							    new Vector2(projectile.position.X + projectile.width / 2f,
								    projectile.position.Y + projectile.height / 2f), 1, 1, Main.npc[j].position,
							    Main.npc[j].width, Main.npc[j].height))
						{
							num6 = num10;
							num4 = num8;
							num5 = num9;
							flag = true;
							num7 = j;
						}
					}
				}
				if (flag)
				{
					projectile.ai[1] = num7 + 1;
				}
				flag = false;
			}
			if (projectile.ai[1] > 0f)
			{
				int num11 = (int)(projectile.ai[1] - 1f);
				if (Main.npc[num11].active && Main.npc[num11].CanBeChasedBy(projectile, true) &&
				    !Main.npc[num11].dontTakeDamage)
				{
					float num12 = Main.npc[num11].position.X + Main.npc[num11].width / 2;
					float num13 = Main.npc[num11].position.Y + Main.npc[num11].height / 2;
					if (Math.Abs(projectile.position.X + projectile.width / 2 - num12) +
					    Math.Abs(projectile.position.Y + projectile.height / 2 - num13) < 1000f)
					{
						flag = true;
						num4 = Main.npc[num11].position.X + Main.npc[num11].width / 2;
						num5 = Main.npc[num11].position.Y + Main.npc[num11].height / 2;
					}
				}
				else
				{
					projectile.ai[1] = 0f;
				}
			}
			if (!projectile.friendly)
			{
				flag = false;
			}
			if (flag)
			{
				float num14 = num3;
				Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f,
					projectile.position.Y + projectile.height * 0.5f);
				float num15 = num4 - vector.X;
				float num16 = num5 - vector.Y;
				float num17 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
				num17 = num14 / num17;
				num15 *= num17;
				num16 *= num17;
				int num18 = 8;
				projectile.velocity.X = (projectile.velocity.X * (num18 - 1) + num15) / num18;
				projectile.velocity.Y = (projectile.velocity.Y * (num18 - 1) + num16) / num18;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			OnHit();
			if (_bounce > 1)
			{
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				_bounce--;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = 0f - oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = 0f - oldVelocity.Y;
				}
			}
			else
			{
				projectile.Kill();
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			OnHit();
			target.AddBuff(39, 600, false);
			target.AddBuff(69, 600, false);
			target.AddBuff(70, 600, false);
			target.AddBuff(31, Main.rand.NextBool(3) ? 180 : 60, false);
			target.AddBuff(72, 120, false);
		}

		private void OnHit()
		{
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			for (int i = 0; i < 5; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Dust obj = Main.dust[num];
				obj.velocity *= 1.5f;
				Dust obj2 = Main.dust[num];
				obj2.scale *= 0.9f;
			}
			for (int j = 0; j < 3; j++)
			{
				float num2 = (0f - projectile.velocity.X) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
				float num3 = (0f - projectile.velocity.Y) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
				Projectile.NewProjectile(projectile.position.X + num2, projectile.position.Y + num3, num2, num3, ProjectileID.CrystalShard, projectile.damage, 0f, projectile.owner, 0f, 0f);
			}
			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int k = 0; k < 7; k++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			}
			for (int l = 0; l < 3; l++)
			{
				int num4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[num4].noGravity = true;
				Dust obj3 = Main.dust[num4];
				obj3.velocity *= 3f;
				num4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Dust obj4 = Main.dust[num4];
				obj4.velocity *= 2f;
			}
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[1] = -1f;
				projectile.maxPenetrate = 0;
				projectile.position.X += projectile.width / 2;
				projectile.position.Y += projectile.height / 2;
				projectile.width = 80;
				projectile.height = 80;
				projectile.position.X -= projectile.width / 2;
				projectile.position.Y -= projectile.height / 2;
				projectile.Damage();
			}
		}

		public override void Kill(int timeleft)
		{
			OnHit();
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].scale = Main.rand.Next(1, 10) * 0.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].fadeIn = 1.5f;
				Dust obj = Main.dust[num];
				obj.velocity *= 0.75f;
			}
			for (int j = 0; j < 10; j++)
			{
				int num2 = Main.rand.Next(139, 143);
				int num3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, num2, (0f - projectile.velocity.X) * 0.3f, (0f - projectile.velocity.Y) * 0.3f, 0, default(Color), 1.2f);
				Dust val = Main.dust[num3];
				val.velocity.X += Main.rand.Next(-50, 51) * 0.01f;
				Dust val2 = Main.dust[num3];
				val2.velocity.Y += Main.rand.Next(-50, 51) * 0.01f;
				Dust val3 = Main.dust[num3];
				val3.velocity.X *= (1f + Main.rand.Next(-50, 51) * 0.01f);
				Dust val4 = Main.dust[num3];
				val4.velocity.Y *= (1f + Main.rand.Next(-50, 51) * 0.01f);
				Dust val5 = Main.dust[num3];
				val5.velocity.X += Main.rand.Next(-50, 51) * 0.05f;
				Dust val6 = Main.dust[num3];
				val6.velocity.Y += Main.rand.Next(-50, 51) * 0.05f;
				Dust obj2 = Main.dust[num3];
				obj2.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
    }
}
