using System;
using System.IO;
using Albedo.Global;
using Albedo.Projectiles.Boss;
using Albedo.Projectiles.Boss.HellGuard;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.NPCs.Boss.HellGuard
{
  public class HellGuardHand : ModNPC
  {
    public override void SetStaticDefaults()
    {
      Main.npcFrameCount[npc.type] = 2;
      NPCID.Sets.TrailCacheLength[npc.type] = 6;
      NPCID.Sets.TrailingMode[npc.type] = 1;
    }

    public override void SetDefaults()
    {
      npc.width = 100;
      npc.height = 100;
      npc.damage = 50;
      npc.defense = 10;
      npc.lifeMax = 6000;
      npc.HitSound = SoundID.NPCHit41;
      npc.DeathSound = SoundID.NPCDeath44;
      npc.noGravity = true;
      npc.noTileCollide = true;
      npc.knockBackResist = 0.0f;
      npc.lavaImmune = true;
      npc.aiStyle = -1;
      for (int index = 0; index < npc.buffImmune.Length; ++index)
        npc.buffImmune[index] = true;
      npc.trapImmune = true;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
	    npc.damage = 140;
	    npc.defense = 18;
	    npc.lifeMax = 11000;
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
      cooldownSlot = 1;
      return npc.localAI[3] == 1.0;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(npc.localAI[0]);
      writer.Write(npc.localAI[1]);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
      npc.localAI[0] = reader.ReadSingle();
      npc.localAI[1] = reader.ReadSingle();
    }

    public override void AI()
		{
			NPC val = AlbedoUtils.NpcExists(npc.ai[2], ModContent.NPCType<HellGuard>());
			if (val == null)
			{
				npc.life = 0;
				npc.checkDead();
				npc.active = false;
				return;
			}
			npc.lifeMax = val.lifeMax;
			npc.damage = val.damage;
			npc.defDamage = val.defDamage;
			npc.defense = val.defense;
			npc.defDefense = val.defDefense;
			npc.target = val.target;
			npc.life = npc.lifeMax;
			Player val2 = Main.player[npc.target];
			npc.direction = (npc.spriteDirection = (int)npc.ai[3]);
			npc.localAI[3] = 0f;
			Vector2 vector;
			switch ((int)npc.ai[0])
			{
			case -1:
				vector = val.Center;
				vector.Y += val.height;
				vector.X += val.width * npc.ai[3] / 2f;
				if (npc.ai[3] > 0f)
				{
					npc.rotation = -(float)Math.PI / 4f;
				}
				else
				{
					npc.rotation = (float)Math.PI / 4f;
				}
				if (npc.ai[1] == 120f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					float num = (float)Math.PI / 4f * Main.rand.NextFloat();
					for (int i = 0; i < 8; i++)
					{
						float num2 = num + (float)Math.PI / 4f * (i + Main.rand.NextFloat(-0.5f, 0.5f));
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0f, Main.myPlayer, num2, 3f);
					}
				}
				if ((npc.ai[1] += 1f) > 120f)
				{
					Movement(vector, 0.6f, 32f);
					npc.localAI[3] = 1f;
					if (npc.ai[3] > 0f)
					{
						npc.rotation = (float)Math.PI * -3f / 4f;
					}
					else
					{
						npc.rotation = (float)Math.PI * 3f / 4f;
					}
					if (npc.ai[1] > 240f)
					{
						npc.ai[0] += 1f;
						npc.ai[1] = 0f;
						npc.netUpdate = true;
					}
				}
				else
				{
					Movement(vector, 1.2f, 32f);
				}
				break;
			case 0:
			case 2:
			case 4:
			case 9:
				npc.noTileCollide = true;
				vector = val.Center;
				vector.Y += 250f;
				vector.X += 300f * (0f - npc.ai[3]);
				Movement(vector, 0.8f, 32f);
				npc.rotation = 0f;
				if ((npc.ai[1] += 1f) > 60f)
				{
					npc.ai[0] += 1f;
					npc.ai[1] = 0f;
					npc.netUpdate = true;
				}
				break;
			case 1:
				if ((npc.ai[1] += 1f) < 0f)
				{
					npc.rotation = npc.velocity.ToRotation() - (float)Math.PI / 2f;
					npc.localAI[3] = 1f;
				}
				else if (npc.ai[1] < 75f)
				{
					npc.rotation = 0f;
					vector = val2.Center + val2.DirectionTo(npc.Center) * 400f;
					if (npc.ai[3] < 0f && vector.X < val2.Center.X + 400f)
					{
						vector.X = val2.Center.X + 400f;
					}
					if (npc.ai[3] > 0f && vector.X > val2.Center.X - 400f)
					{
						vector.X = val2.Center.X - 400f;
					}
					if (((Entity)npc).Distance(vector) > 50f)
					{
						Movement(vector, (val.localAI[2] == 1f) ? 2.4f : 1.2f, 32f);
					}
					if (val.localAI[2] == 1f)
					{
						NPC npc2 = npc;
						npc2.position += val2.velocity / 3f;
					}
				}
				else if (npc.ai[1] < 120f)
				{
					if (val.localAI[2] == 1f)
					{
						NPC npc3 = npc;
						npc3.position += val2.velocity / 10f;
					}
					npc.localAI[3] = 1f;
					NPC npc4 = npc;
					npc4.velocity *= ((npc.localAI[2] == 1f) ? 0.8f : 0.95f);
					npc.rotation = npc.DirectionTo(val2.Center).ToRotation() - (float)Math.PI / 2f;
				}
				else if (npc.ai[1] == 120f)
				{
					npc.localAI[3] = 1f;
					npc.velocity = npc.DirectionTo(val2.Center) * ((val.localAI[2] == 1f) ? 20 : 16);
				}
				else
				{
					NPC npc5 = npc;
					npc5.velocity *= 1.02f;
					npc.localAI[3] = 1f;
					npc.rotation = npc.velocity.ToRotation() - (float)Math.PI / 2f;
					for (int j = 0; j < 5; j++)
					{
						int num3 = Dust.NewDust(npc.position, npc.width, npc.height, 6, (0f - npc.velocity.X) * 0.25f, (0f - npc.velocity.Y) * 0.25f, 0, default(Color), 3f);
						Dust obj = Main.dust[num3];
						obj.position -= Vector2.Normalize(npc.velocity) * npc.width / 2f;
						Main.dust[num3].noGravity = true;
						Dust obj2 = Main.dust[num3];
						obj2.velocity *= 4f;
					}
					if (((npc.localAI[1] += 1f) > 60f && npc.Distance(val2.Center) > 1000f) || ((npc.ai[3] > 0f) ? (npc.Center.X > Math.Min(val.Center.X, val2.Center.X) + 300f) : (npc.Center.X < Math.Max(val.Center.X, val2.Center.X) - 300f)))
					{
						npc.ai[1] = ((val.localAI[2] == 1f) ? 15 : 0);
						npc.localAI[1] = 0f;
						npc.netUpdate = true;
						if (val.localAI[2] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
						{
							Projectile.NewProjectile(npc.Center + Vector2.Normalize(npc.velocity) * 100f, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast>(), npc.damage / 4, 0f, Main.myPlayer, npc.velocity.ToRotation(), 7f);
						}
						npc.velocity = Vector2.Normalize(npc.velocity) * 0.1f;
					}
				}
				if ((npc.localAI[0] += 1f) > 660f)
				{
					npc.ai[0] += 1f;
					npc.ai[1] = 0f;
					npc.localAI[0] = 0f;
					npc.netUpdate = true;
				}
				break;
			case 3:
				if (npc.ai[3] > 0f)
				{
					vector = val2.Center;
					vector.Y += val2.velocity.Y * 60f;
					vector.X = val2.Center.X - 400f;
					if (((Entity)npc).Distance(vector) > 50f)
					{
						Movement(vector, 0.4f, 32f);
					}
				}
				else
				{
					vector = val2.Center;
					vector.X += 400f;
					vector.Y += 600f * (float)Math.Sin(0.08159980918415047 * npc.ai[1]);
					Movement(vector, 0.8f, 32f);
				}
				if ((npc.localAI[0] += 1f) > (val.localAI[2] == 1f ? 18 : 24))
				{
					npc.localAI[0] = 0f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.UnitX * npc.ai[3], ModContent.ProjectileType<FlowerPetal>(), npc.damage / 4, 0f, Main.myPlayer, 1f, 0f);
					}
				}
				npc.position.X += npc.velocity.X;
				if ((npc.ai[1] += 1f) > 360f)
				{
					npc.ai[0] += 1f;
					npc.ai[1] = 0f;
					npc.localAI[0] = 0f;
					npc.netUpdate = true;
				}
				break;
			case 5:
			case 6:
			case 7:
			{
				if ((npc.ai[1] += 1f) < 90f)
				{
					npc.noTileCollide = true;
					vector = val.Center;
					vector.Y -= val.height;
					vector.X += 50f * (0f - npc.ai[3]);
					Movement(vector, 2f, 32f);
					npc.rotation = 0f;
					break;
				}
				if (npc.ai[1] == 90f)
				{
					npc.velocity = Vector2.UnitY * ((val.localAI[2] == 1f) ? 36 : 24);
					npc.localAI[0] = val2.position.Y;
					npc.netUpdate = true;
					break;
				}
				npc.localAI[3] = 1f;
				if (npc.ai[3] > 0f)
				{
					npc.rotation = -(float)Math.PI / 2f;
				}
				else
				{
					npc.rotation = (float)Math.PI / 2f;
				}
				if (npc.position.Y + npc.height > npc.localAI[0])
				{
					npc.noTileCollide = false;
				}
				if (!npc.noTileCollide && (Collision.SolidCollision(npc.position, npc.width, npc.height) || npc.position.Y + npc.height > Main.maxTilesY * 16 - 16))
				{
					npc.velocity.Y = 0f;
				}
				if (npc.velocity.Y != 0f)
				{
					break;
				}
				if (npc.localAI[0] != 0f)
				{
					npc.localAI[0] = 0f;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(npc.Center, Vector2.Zero, 696, 0, 0f, Main.myPlayer);
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<FuseBomb>(), npc.damage / 4, 0f, Main.myPlayer);
						if (val.localAI[2] == 1f)
						{
							for (int k = 0; k < 4; k++)
							{
								Vector2 vector2 = Utils.RotatedBy(Vector2.Normalize(npc.oldVelocity), Math.PI / 2.0 * (npc.ai[3] < 0f ? k : (k + 0.5)), default(Vector2));
								Projectile.NewProjectile(npc.Center, 1.5f * vector2, ModContent.ProjectileType<LavaPalladOrb>(), npc.damage / 4, 0f, Main.myPlayer);
							}
						}
						else
						{
							Vector2 center = npc.Center;
							for (int l = 0; l <= 3; l++)
							{
								int num5 = (int)center.X / 16 + 250 * l / 16 * (int)(0f - npc.ai[3]);
								int num6 = (int)center.Y / 16;
								Projectile.NewProjectile(num5 * 16 + 8, num6 * 16 + 8, 0f, 0f, ModContent.ProjectileType<LavaGeyser>(), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
							}
						}
					}
				}
				npc.localAI[1] += 1f;
				if (!(npc.localAI[1] > (val.localAI[2] == 1f ? 20 : 30)))
				{
					break;
				}
				npc.netUpdate = true;
				npc.ai[0] += 1f;
				npc.ai[1] = 0f;
				npc.localAI[0] = 0f;
				npc.localAI[1] = 0f;
				npc.velocity = Vector2.Zero;
				for (int m = 0; m < 200; m++)
				{
					if (Main.npc[m].active && Main.npc[m].type == npc.type && m != npc.whoAmI && Main.npc[m].ai[2] == npc.ai[2])
					{
						Main.npc[m].velocity = Vector2.Zero;
						Main.npc[m].ai[0] = npc.ai[0];
						Main.npc[m].ai[1] = npc.ai[1];
						Main.npc[m].localAI[0] = npc.localAI[0];
						Main.npc[m].localAI[1] = npc.localAI[1];
						Main.npc[m].netUpdate = true;
						break;
					}
				}
				break;
			}
			case 8:
				npc.noTileCollide = true;
				vector = val.Center + Vector2.UnitX * (0f - npc.ai[3]) * ((val.localAI[2] == 1f ? 450 : 600) + 500f * (1f - Math.Min(1f, npc.localAI[1] / 240f)));
				if (((Entity)npc).Distance(vector) > 25f)
				{
					Movement(vector, 0.8f, 32f);
				}
				npc.rotation = (float)Math.PI / 2f * (0f - npc.ai[3]) + (float)Math.PI;
				if ((npc.localAI[1] += 1f) > 90f && npc.localAI[1] % 6f == 0f && Main.netMode != 1)
				{
					int num4 = Projectile.NewProjectile(npc.Center + Vector2.UnitY * Main.rand.NextFloat(50f, 100f), Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), npc.damage / 4, 0f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), 16f);
					if (num4 != 1000)
					{
						Main.projectile[num4].localAI[0] = 2f;
					}
				}
				if (npc.ai[1] > 60f)
				{
					if (val.ai[0] != 1f)
					{
						npc.ai[0] += 1f;
						npc.ai[1] = 0f;
						npc.localAI[0] = 0f;
						npc.localAI[1] = 0f;
						npc.netUpdate = true;
					}
				}
				else
				{
					npc.ai[1] += 1f;
					if (val.ai[0] == 0f)
					{
						val.ai[0] = 1f;
						val.netUpdate = true;
					}
				}
				break;
			case 10:
				if (val.localAI[2] == 1f)
				{
					NPC npc1 = npc;
					npc1.position += val2.velocity / 2f;
				}
				if (npc.ai[3] > 0f)
				{
					vector = val2.Center;
					vector.Y = val2.Center.Y - 400f;
					vector.X += val2.velocity.X * 60f;
					if (((Entity)npc).Distance(vector) > 50f)
					{
						Movement(vector, 0.6f, 32f);
					}
					npc.rotation = (float)Math.PI / 2f;
				}
				else
				{
					vector = val2.Center;
					vector.Y -= 300f;
					vector.X += 1000f * (float)Math.Sin(0.08159980918415047 * npc.ai[1]);
					Movement(vector, 1.8f, 32f);
					npc.rotation = -(float)Math.PI / 2f;
					npc.localAI[0] += 0.5f;
				}
				if ((npc.localAI[0] += 1f) > 60f && npc.ai[1] > 120f)
				{
					npc.localAI[0] = 0f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.UnitY * 2f, ModContent.ProjectileType<CrystalBomb>(), npc.damage / 4, 0f, Main.myPlayer, val2.position.Y, 0f);
					}
				}
				if ((npc.ai[1] += 1f) > 600f)
				{
					npc.ai[0] += 1f;
					npc.ai[1] = 0f;
					npc.localAI[0] = 0f;
					npc.netUpdate = true;
				}
				break;
			default:
				npc.ai[0] = 0f;
				goto case 0;
			}
		}

    public override void FindFrame(int frameHeight) => npc.frame.Y = npc.localAI[3] == 1.0 ? 0 : frameHeight;

    public override bool StrikeNPC(
      ref double damage,
      int defense,
      ref float knockback,
      int hitDirection,
      ref bool crit)
    {
      damage = 0.0;
      return true;
    }

    public override void OnHitByProjectile(
      Projectile projectile,
      int damage,
      float knockback,
      bool crit)
    {
      if (!AlbedoUtils.CanDeleteProjectile(projectile))
        return;
      projectile.penetrate = 0;
      projectile.timeLeft = 0;
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if (npc.Center.X < (double) targetPos.X)
      {
        npc.velocity.X += speedModifier;
        if (npc.velocity.X < 0.0)
          npc.velocity.X += speedModifier * 2f;
      }
      else
      {
        npc.velocity.X -= speedModifier;
        if (npc.velocity.X > 0.0)
          npc.velocity.X -= speedModifier * 2f;
      }
      if (npc.Center.Y < (double) targetPos.Y)
      {
        npc.velocity.Y += speedModifier;
        if (npc.velocity.Y < 0.0)
          npc.velocity.Y += speedModifier * 2f;
      }
      else
      {
        npc.velocity.Y -= speedModifier;
        if (npc.velocity.Y > 0.0)
          npc.velocity.Y -= speedModifier * 2f;
      }
      if (Math.Abs(npc.velocity.X) > (double) cap)
        npc.velocity.X = cap * Math.Sign(npc.velocity.X);
      if (Math.Abs(npc.velocity.Y) <= (double) cap)
        return;
      npc.velocity.Y = cap * Math.Sign(npc.velocity.Y);
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      target.AddBuff(24, 300);
      target.AddBuff(67, 300);
    }

    public override bool CheckActive() => false;

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Rectangle frame = npc.frame;
			Vector2 origin = Utils.Size(frame) / 2f;
			Color color = lightColor;
			color = npc.GetAlpha(color);
			SpriteEffects effects = ((npc.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++)
			{
				Color color2 = color * 0.5f;
				color2 *= (NPCID.Sets.TrailCacheLength[npc.type] - i) / (float)NPCID.Sets.TrailCacheLength[npc.type];
				Vector2 vector = npc.oldPos[i];
				float rotation = npc.rotation;
				Main.spriteBatch.Draw(texture, vector + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, color2, rotation, origin, npc.scale, effects, 0f);
			}
			Texture2D texture2 = ModContent.GetTexture("Albedo/NPCs/Boss/HellGuard/HellGuardHand_Glow");
			if (npc.dontTakeDamage)
			{
				Vector2 vector2 = Vector2.UnitX * Main.rand.NextFloat(-180f, 180f);
				Main.spriteBatch.Draw(texture, npc.Center + vector2 - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor) * 0.5f, npc.rotation, origin, npc.scale, effects, 0f);
				Main.spriteBatch.Draw(texture2, npc.Center + vector2 - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor) * 0.5f, npc.rotation, origin, npc.scale, effects, 0f);
			}
			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor), npc.rotation, origin, npc.scale, effects, 0f);
			Main.spriteBatch.Draw(texture2, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0f);
			return false;
		}
  }
}
