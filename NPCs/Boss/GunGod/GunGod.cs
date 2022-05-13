using System;
using System.IO;
using Albedo.Buffs.Boss;
using Albedo.Global;
using Albedo.Projectiles.Boss.GunGod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace Albedo.NPCs.Boss.GunGod
{
    [AutoloadBossHead]
    public class GunGod : ModNPC
    {
        private int _ritualProj;

        private int _ringProj;

        private int _spriteProj;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 57;
            npc.damage = 150;
            npc.defense = 80;
            npc.lifeMax = 60000 * 3;
            npc.value = Item.buyPrice(0, 20);
            npc.HitSound = SoundID.NPCHit57;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.npcSlots = 50f;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.buffImmune[46] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[68] = true;
            npc.timeLeft = NPC.activeTime * 30;
            //bossBag = ModContent.ItemType<GunGodBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 300;
            npc.lifeMax = (int)(80000 * 2.5f);
            npc.defense = 85;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = 1;
            if (((Entity)npc).Distance(AlbedoUtils.ClosestPointInHitbox((Entity)target, npc.Center)) < 42f && npc.ai[0] != 10f)
            {
                return npc.ai[0] != 18f;
            }
            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(npc.localAI[0]);
            writer.Write(npc.localAI[1]);
            writer.Write(npc.localAI[2]);
            writer.Write(npc.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadSingle();
            npc.localAI[1] = reader.ReadSingle();
            npc.localAI[2] = reader.ReadSingle();
            npc.localAI[3] = reader.ReadSingle();
        }

        public override void AI()
        {
            AlbedoGlobalNpc.GunGod = npc.whoAmI;
            switch (npc.localAI[3])
            {
                case 0f:
                {
                    npc.TargetClosest();
                    if (npc.timeLeft < 30)
                    {
                        npc.timeLeft = 30;
                    }
                    if (npc.Distance(Main.player[npc.target].Center) < 1500f)
                    {
                        npc.localAI[3] = 1f;
                        Main.PlaySound(SoundID.Roar, npc.Center, 0);
                        npc.localAI[0] = Main.rand.Next(3);
                        npc.localAI[1] = Main.rand.Next(2);
                    }

                    break;
                }
                case 1f:
                    Aura(2000f, BuffID.WitheredArmor, reverse: true, 86, checkDuration: false, targetEveryone: false);
                    break;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
				if (npc.localAI[3] == 2f && AlbedoUtils.ProjectileExists(_ritualProj, ModContent.ProjectileType<Arena1>()) == null)
				{
					_ritualProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Arena1>(), npc.damage / 4, 0f, Main.myPlayer, 0f, npc.whoAmI);
				}
				if (AlbedoUtils.ProjectileExists(_ringProj, ModContent.ProjectileType<Arena2>()) == null)
				{
					_ringProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Arena2>(), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
				}
				if (AlbedoUtils.ProjectileExists(_spriteProj, ModContent.ProjectileType<GunGodTrail>()) == null)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						int num = 0;
						for (int num2 = 999; num2 >= 0; num2--)
						{
							if (!Main.projectile[num2].active)
							{
								num = num2;
								break;
							}
						}
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							Projectile obj = Main.projectile[num];
							obj.SetDefaults(ModContent.ProjectileType<GunGodTrail>());
							obj.Center = npc.Center;
							obj.owner = Main.myPlayer;
							obj.velocity.X = 0f;
							obj.velocity.Y = 0f;
							obj.damage = 0;
							obj.knockBack = 0f;
							obj.identity = num;
							obj.gfxOffY = 0f;
							obj.stepSpeed = 1f;
							obj.ai[1] = npc.whoAmI;
							_spriteProj = num;
						}
					}
					else
					{
						_spriteProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GunGodTrail>(), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
					}
				}
            }
            if (Main.player[Main.myPlayer].active && npc.Distance(Main.player[Main.myPlayer].Center) < 3000f)
            {
                Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<GunGodCurse>(), 2);
            }
            Player val = Main.player[npc.target];
            npc.direction = npc.spriteDirection = npc.Center.X < val.Center.X ? 1 : -1;
            Vector2 vector;
            switch ((int)npc.ai[0])
            {
                case -3:
                {
                    npc.velocity *= 0.9f;
                    npc.dontTakeDamage = true;
                    for (int i = 0; i < 5; i++)
                    {
                        int num3 = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 2.5f);
                        Main.dust[num3].noGravity = true;
                        Dust obj2 = Main.dust[num3];
                        obj2.velocity *= 12f;
                    }
                    if (!((npc.ai[1] += 1f) > 180f))
                    {
                        break;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.UnitX, Main.rand.NextDouble() * Math.PI, default(Vector2)) * Main.rand.NextFloat(30f), ModContent.ProjectileType<DeathScythe>(), 0, 0f, Main.myPlayer);
                        }
                    }
                    npc.life = 0;
                    npc.dontTakeDamage = false;
                    npc.checkDead();
                    break;
                }
                case -2:
                    if (AliveCheck(val))
                    {
                        npc.velocity *= 0.9f;
                        npc.dontTakeDamage = true;
                        for (int num52 = 0; num52 < 5; num52++)
                        {
                            int num53 = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 2.5f);
                            Main.dust[num53].noGravity = true;
                            Dust obj8 = Main.dust[num53];
                            obj8.velocity *= 12f;
                        }
                        if ((npc.ai[1] += 1f) > 180f)
                        {
                            npc.netUpdate = true;
                            npc.ai[0] = 9f;
                            npc.ai[1] = 0f;
                        }
                    }
                    break;
                case -1:
                {
                    npc.velocity *= 0.9f;
                    npc.dontTakeDamage = true;
                    if (npc.buffType[0] != 0)
                    {
                        npc.DelBuff(0);
                    }
                    if ((npc.ai[1] += 1f) > 120f)
                    {
                        for (int num59 = 0; num59 < 5; num59++)
                        {
                            int num60 = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 1.5f);
                            Main.dust[num60].noGravity = true;
                            Dust obj10 = Main.dust[num60];
                            obj10.velocity *= 4f;
                        }
                        npc.localAI[3] = 2f;
                        int num61 = (int)(npc.lifeMax / 90 * Main.rand.NextFloat(1f, 1.5f));
                        npc.life += num61;
                        if (npc.life > npc.lifeMax)
                        {
                            npc.life = npc.lifeMax;
                        }
                        CombatText.NewText(npc.Hitbox, CombatText.HealLife, num61);
                        if (npc.ai[1] > 210f)
                        {
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[1] == 120f)
                    {
                        AlbedoUtils.ClearFriendlyProjectiles(1);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            _ritualProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Arena1>(), npc.damage / 4, 0f, Main.myPlayer, 0f, npc.whoAmI);
                        }
                        Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    }
                    break;
                }
                case 0:
                    if (!AliveCheck(val) || Phase2Check())
                    {
                        break;
                    }
                    npc.dontTakeDamage = false;
                    vector = val.Center;
                    vector.X += 500 * (!(npc.Center.X < vector.X) ? 1 : -1);
                    if (((Entity)npc).Distance(vector) > 50f)
                    {
                        float num5 = npc.localAI[3] > 0f ? 0.5f : 2f;
                        if (npc.Center.X < vector.X)
                        {
                            npc.velocity.X += num5;
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X += num5 * 2f;
                            }
                        }
                        else
                        {
                            npc.velocity.X -= num5;
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X -= num5 * 2f;
                            }
                        }
                        if (npc.Center.Y < vector.Y)
                        {
                            npc.velocity.Y += num5;
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y += num5 * 2f;
                            }
                        }
                        else
                        {
                            npc.velocity.Y -= num5;
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y -= num5 * 2f;
                            }
                        }
                        if (npc.localAI[3] > 0f)
                        {
                            if (Math.Abs(npc.velocity.X) > 24f)
                            {
                                npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                            }
                            if (Math.Abs(npc.velocity.Y) > 24f)
                            {
                                npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                            }
                        }
                    }
                    if (npc.localAI[3] > 0f)
                    {
                        npc.ai[1] += 1f;
                        if (npc.ai[3] == 0f)
                        {
                            npc.ai[3] = 1f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int num6 = npc.localAI[3] > 1f ? 6 : 3;
                                for (int k = 0; k < num6; k++)
                                {
                                    float num7 = k * 2 * (float)Math.PI / num6;
                                    int num8 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<MiniGun>(), 0, npc.whoAmI, 0f, num7);
                                    if (num8 != 200 && Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8);
                                    }
                                }
                            }
                        }
                    }
                    if (npc.ai[1] > 120f)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 60;
                        if ((npc.ai[2] += 1f) > 7)
                        {
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.velocity = npc.DirectionTo(val.Center) * 2f;
                        }
                        else if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float num9 = npc.Distance(val.Center) / 30f * 2f;
                            float num10 = npc.localAI[3] > 1f ? 1f : 0f;
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(val.Center) * 30f, ModContent.ProjectileType<ScytheSplit>(), npc.damage / 4, 0f, Main.myPlayer, num9, num10);
                            float num11 = (float)Math.PI * (npc.Center.X < val.Center.X ? 1 : -1);
                            Projectile.NewProjectile(npc.Center, new Vector2(npc.Center.X < val.Center.X ? -1f : 1f, -1f), ModContent.ProjectileType<SDFMG>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.whoAmI, num11 / 60f * 2f);
                        }
                    }
                    break;
                case 1:
                {
                    if (!AliveCheck(val) || Phase2Check())
                    {
                        break;
                    }
                    npc.velocity = npc.DirectionTo(val.Center);
                    npc.velocity *= npc.localAI[3] > 1f  ? 2f : 6f;
                    int num16 = npc.localAI[3] > 1f ? 7 : 6;
                    num16++;
                    if (!((npc.ai[1] -= 1f) < 0f))
                    {
                        break;
                    }
                    if ((npc.ai[2] += 1f) > 4f)
                    {
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else
                    {
                        npc.ai[1] = 80f;
                        float num17 = npc.localAI[3] > 1f ? 40 : 20;
                        float num18 = npc.localAI[3] > 1f ? 90 : 40;
                        float num19 = npc.localAI[3] > 1f ? 40 : 10;
                        float num20 = npc.ai[2] % 2f == 0f ? 0f : 0.5f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int l = 0; l < num16; l++)
                            { 
                                Projectile.NewProjectile(npc.Center, Utils.RotatedBy(npc.DirectionTo(val.Center), (double)((float)Math.PI * 2f / num16 * (l + num20)), default(Vector2)) * num19, ModContent.ProjectileType<Flaming>(), npc.damage / 4, 0f, Main.myPlayer, num17, num17 + num18);
                            }
                        }
                        Main.PlaySound(SoundID.ForceRoar, (int)npc.Center.X, (int)npc.Center.Y, -1);
                    }
                    npc.netUpdate = true;
                    break;
                }
                case 2:
                {
                    if (!AliveCheck(val) || Phase2Check())
                    {
                        break;
                    }
                    npc.velocity *= 0.9f;
                    if (npc.ai[2] == 0f)
                    {
                        if (npc.localAI[3] > 1f)
                        {
                            switch (npc.ai[1])
                            {
                                case 30f:
                                    Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), npc.damage / 4, 0f, Main.myPlayer, 3f, npc.whoAmI);
                                    break;
                                case 210f:
                                {
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {
                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Parry>(), 0, 0f, Main.myPlayer);
                                    }
                                    npc.netUpdate = true;
                                    break;
                                }
                            }
                        }
                        else if (npc.ai[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Parry>(), 0, 0f, Main.myPlayer);
                        }
                    }
                    if (!((npc.ai[1] += 1f) > (npc.ai[2] == 0f && npc.localAI[3] > 1f ? 240 : 30)))
                    {
                        break;
                    }
                    npc.netUpdate = true;
                    npc.ai[0] += 1f;
                    npc.ai[1] = 0f;
                    npc.ai[3] = 0f;
                    if ((npc.ai[2] += 1f) > 5f)
                    {
                        npc.ai[0] += 1f;
                        npc.ai[2] = 0f;
                        break;
                    }
                    npc.velocity = npc.DirectionTo(val.Center + val.velocity) * 30f;
                    if (npc.localAI[3] > 1f)
                    {
                        npc.velocity *= 1.2f;
                        for (int m = 0; m < 128; m++)
                        {
                            Vector2 vector3 = Utils.RotatedBy(-Utils.RotatedBy(Vector2.UnitY, m * 3.14159274101257 * 2.0 / 128.0, default(Vector2)) * new Vector2(8f, 16f), (double)npc.velocity.ToRotation(), default(Vector2));
                            int num21 = Dust.NewDust(npc.Center, 0, 0, 87, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num21].scale = 3f;
                            Main.dust[num21].noGravity = true;
                            Main.dust[num21].position = npc.Center;
                            Main.dust[num21].velocity = Vector2.Zero;
                            Dust obj3 = Main.dust[num21];
                            obj3.velocity += vector3 * 1.5f + npc.velocity * 0.5f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float num22 = 4.712389f * (npc.ai[2] % 2f == 0f ? 1 : -1);
                            Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.Normalize(npc.velocity), (double)((0f - num22) / 2f), default(Vector2)), ModContent.ProjectileType<SDFMG>(), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, num22 / 60f * 2f);
                        }
                    }
                    break;
                }
                case 3:
                    if (Phase2Check())
                    {
                        break;
                    }
                    npc.direction = npc.spriteDirection = Math.Sign(npc.velocity.X);
                    if (npc.localAI[3] > 1f)
                    {
                        for (int num54 = 0; num54 < 2; num54++)
                        {
                            int num55 = Dust.NewDust(npc.Center - npc.velocity * Main.rand.NextFloat(), 0, 0, 87, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num55].scale = 1f + 4f * (1f - npc.ai[1] / 30f);
                            Main.dust[num55].noGravity = true;
                            Dust obj9 = Main.dust[num55];
                            obj9.velocity *= 0.1f;
                        }
                    }
                    if ((npc.ai[3] += 1f) > 5f)
                    {
                        npc.ai[3] = 0f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity), ModContent.ProjectileType<Sickle>(), npc.damage / 4, 0f, Main.myPlayer);
                            if (npc.localAI[3] > 1f)
                            {
                               Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.Normalize(npc.velocity), Math.PI / 2.0, default(Vector2)), ModContent.ProjectileType<Sickle>(), npc.damage / 4, 0f, Main.myPlayer);
                               Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.Normalize(npc.velocity), -Math.PI / 2.0, default(Vector2)), ModContent.ProjectileType<Sickle>(), npc.damage / 4, 0f, Main.myPlayer);
                            }
                        }
                    }
                    if ((npc.ai[1] += 1f) > 30f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] -= 1f;
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                    }
                    break;
                case 4:
                    if (AliveCheck(val))
                    {
                        Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        npc.netUpdate = true;
                        npc.ai[0] += (npc.localAI[0] += 1f);
                        if (npc.localAI[0] >= 3f)
                        {
                            npc.localAI[0] = 0f;
                        }
                    }
                    break;
                case 5:
                    if (!AliveCheck(val) || Phase2Check())
                    {
                        break;
                    }
                    npc.velocity = npc.DirectionTo(val.Center) * 3f;
                    if (!((npc.ai[1] += 1f) > (npc.localAI[3] > 1f ? 75 : 90)))
                    {
                        break;
                    }
                    npc.ai[1] = 0f;
                    if ((npc.ai[2] += 1f) > 3f)
                    {
                        npc.ai[0] = 8f;
                        npc.ai[2] = 0f;
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float num56 = npc.localAI[3] > 1f ? npc.DirectionTo(val.Center).ToRotation() : 0f;
                            float num57 = 1000f;
                            if (npc.localAI[3] > 1f && npc.Distance(val.Center) > num57 / 2f)
                            {
                                num57 = npc.Distance(val.Center);
                            }
                            num57 /= 90f;
                            for (int num58 = 0; num58 < 4; num58++)
                            {
                                Vector2 vector14 = Utils.RotatedBy(new Vector2(num57, 0f), num56 + Math.PI / 2.0 * num58, default(Vector2));
                                Projectile.NewProjectile(npc.Center, vector14, ModContent.ProjectileType<SickleSplit1>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.whoAmI, 0f);
                                Projectile.NewProjectile(npc.Center, vector14, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector14) + (float)Math.PI / 2f);
                                Projectile.NewProjectile(npc.Center, vector14, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector14) - (float)Math.PI / 2f);
                                Projectile.NewProjectile(npc.Center, vector14, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector14) + (float)Math.PI / 4f);
                                Projectile.NewProjectile(npc.Center, vector14, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector14) + (float)Math.PI / 4f + (float)Math.PI);
                                Vector2 vector15 = Utils.RotatedBy(new Vector2(num57, num57), num56 + Math.PI / 2.0 * num58, default(Vector2));
                                Projectile.NewProjectile(npc.Center, vector15, ModContent.ProjectileType<SickleSplit1>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.whoAmI, 0f);
                                Projectile.NewProjectile(npc.Center, vector15, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector15) + (float)Math.PI / 2f);
                                Projectile.NewProjectile(npc.Center, vector15, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, Utils.ToRotation(vector15) - (float)Math.PI / 2f);
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, num56 + (float)Math.PI / 2f * num58);
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 1f, num56 + (float)Math.PI / 2f * (num58 + 0.5f));
                            }
                        }
                        Main.PlaySound(SoundID.ForceRoar, (int)npc.Center.X, (int)npc.Center.Y, -1);
                    }
                    npc.netUpdate = true;
                    break;
                case 6:
                {
                    if (Phase2Check())
                    {
                        break;
                    }
                    npc.velocity *= 0.99f;
                    if (npc.ai[2] == 0f)
                    {
                        npc.ai[2] = 1f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int num34 = -3; num34 <= 3; num34++)
                            {
                                if (num34 != 0)
                                {
                                    Vector2 vector6 = new Vector2(Main.rand.NextFloat(40f), Main.rand.NextFloat(-20f, 20f));
                                    Projectile.NewProjectile(npc.Center, vector6, ModContent.ProjectileType<Flocko>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.whoAmI, (float)(120 * num34));
                                }
                            }
                            if (npc.localAI[3] > 1f)
                            {
                                Vector2 vector7 = new Vector2(Main.rand.NextFloat(40f), Main.rand.NextFloat(-20f, 20f));
                                Projectile.NewProjectile(npc.Center, vector7, ModContent.ProjectileType<Flocko2>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.target, -1f);
                                Projectile.NewProjectile(npc.Center, -vector7, ModContent.ProjectileType<Flocko2>(), npc.damage / 4, 0f, Main.myPlayer, npc.target, 1f);
                            }
                            float num35 = 420f;
                            Projectile.NewProjectile(npc.Center, Main.rand.NextVector2CircularEdge(20f, 20f), ModContent.ProjectileType<Flocko3>(), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, num35);
                            Projectile.NewProjectile(npc.Center, Main.rand.NextVector2CircularEdge(20f, 20f), ModContent.ProjectileType<Flocko3>(), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 0f - num35);
                            for (int num36 = -1; num36 <= 1; num36 += 2)
                            {
                                for (int num37 = -1; num37 <= 1; num37 += 2)
                                {
                                    int num38 = Projectile.NewProjectile(npc.Center + 3000 * num36 * Vector2.UnitX, Vector2.UnitY * num37, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 5f, (float)(220 * num36));
                                    if (num38 != 1000)
                                    {
                                        Main.projectile[num38].localAI[1] = npc.whoAmI;
                                        if (Main.netMode == NetmodeID.Server)
                                        {
                                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num38);
                                        }
                                    }
                                }
                            }
                        }
                        Main.PlaySound(SoundID.Item27, npc.Center);
                        for (int num39 = 0; num39 < 30; num39++)
                        {
                            int num40 = Dust.NewDust(npc.position, npc.width, npc.height, 76, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num40].noGravity = true;
                            Main.dust[num40].noLight = true;
                            Dust obj6 = Main.dust[num40];
                            obj6.velocity *= 5f;
                        }
                    }
                    if ((npc.ai[1] += 1f) > 420f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] = 8f;
                        npc.ai[1] = 0f;
                    }
                    break;
                }
                case 7:
                {
                    if (Phase2Check())
                    {
                        break;
                    }
                    npc.velocity *= 0.99f;
                    if (npc.ai[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, npc.whoAmI, -4f);
                    }
                    if ((npc.ai[1] += 1f) > 420f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] = 8f;
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                        break;
                    }
                    if (npc.ai[1] > 60f)
                    {
                        if (npc.localAI[3] > 1f)
                        {
                            npc.ai[3] = MathHelper.Lerp(npc.ai[3], 1f, 0.05f);
                        }
                        else
                        {
                            float num41;
                            for (num41 = npc.DirectionTo(val.Center).ToRotation(); num41 < -(float)Math.PI; num41 += (float)Math.PI * 2f)
                            {
                            }
                            while (num41 > (float)Math.PI)
                            {
                                num41 -= (float)Math.PI * 2f;
                            }
                            npc.ai[3] = npc.ai[3].AngleLerp(num41, 0.05f);
                        }
                        if ((npc.ai[2] += 1f) > 1f)
                        {
                            npc.ai[2] = 0f;
                            Main.PlaySound(SoundID.Item12, npc.Center);
                            if (Main.netMode != 1)
                            {
                                if (npc.localAI[3] > 1f)
                                {
                                    float num42 = MathHelper.Lerp(180f, 20f, npc.ai[3]);
                                    for (int num43 = -1; num43 <= 1; num43 += 2)
                                    {
                                        Vector2 vector8 = 16f * Utils.RotatedBy(npc.DirectionTo(val.Center), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 3.0, default(Vector2));
                                        Projectile.NewProjectile(npc.Center, Utils.RotatedBy(vector8, (double)MathHelper.ToRadians(num42 * num43), default(Vector2)), ModContent.ProjectileType<Laser>(), npc.damage / 4, 0f, Main.myPlayer);
                                    }
                                }
                                else
                                {
                                    for (int num44 = 0; num44 < 2; num44++)
                                    {
                                        Vector2 vector9 = 16f * Utils.RotatedBy(npc.ai[3].ToRotationVector2(), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0, default(Vector2));
                                        Projectile.NewProjectile(npc.Center, vector9, ModContent.ProjectileType<Laser>(), npc.damage / 4, 0f, Main.myPlayer, 0f, 0f);
                                    }
                                }
                            }
                        }
                        if ((npc.localAI[2] += 1f) > 60f)
                        {
                            npc.localAI[2] = 0f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Vector2 vector10 = Utils.RotatedBy(npc.DirectionTo(val.Center), 1.5707963705062866, default(Vector2));
                                vector10 *= (float)((npc.localAI[3] > 1f) ? 5 : 8);
                                Projectile.NewProjectile(npc.Center, Utils.RotatedBy(vector10, (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0, default(Vector2)), ModContent.ProjectileType<Rocket>(), npc.damage / 4, 0f, Main.myPlayer, npc.target, 30f);
                                Projectile.NewProjectile(npc.Center, -Utils.RotatedBy(vector10, (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0, default(Vector2)), ModContent.ProjectileType<Rocket>(), npc.damage / 4, 0f, Main.myPlayer, npc.target, 30f);
                                Vector2 vector11 = Utils.RotatedBy(npc.DirectionTo(val.Center), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0, default(Vector2));
                                vector11 *= (float)((npc.localAI[3] > 1f) ? 5 : 8);
                                Projectile.NewProjectile(npc.Center, vector11, ModContent.ProjectileType<Rocket>(), npc.damage / 4, 0f, Main.myPlayer, (float)npc.target, 60f);
                            }
                        }
                        break;
                    }
                    if (npc.localAI[3] > 1f)
                    {
                        npc.ai[3] = 0f;
                    }
                    else
                    {
                        npc.ai[3] = npc.DirectionFrom(val.Center).ToRotation() - 0.001f;
                        while (npc.ai[3] < -(float)Math.PI)
                        {
                            npc.ai[3] += (float)Math.PI * 2f;
                        }
                        while (npc.ai[3] > (float)Math.PI)
                        {
                            npc.ai[3] -= (float)Math.PI * 2f;
                        }
                    }
                    Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    for (int num45 = 0; num45 < 5; num45++)
                    {
                        int num46 = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num46].noGravity = true;
                        Dust obj7 = Main.dust[num46];
                        obj7.velocity *= 4f;
                    }
                    break;
                }
                case 8:
                {
                    if (!AliveCheck(val) || Phase2Check())
                    {
                        break;
                    }
                    npc.velocity *= 0.9f;
                    npc.localAI[2] = 0f;
                    if (!((npc.ai[1] += 1f) > 120f))
                    {
                        break;
                    }
                    Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    npc.netUpdate = true;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    if (npc.localAI[3] > 1f)
                    {
                        if (npc.localAI[1] == 0f)
                        {
                            npc.localAI[1] = 1f;
                            npc.ai[0] = 15f;
                        }
                        else
                        {
                            npc.localAI[1] = 0f;
                            npc.ai[0] += 1f;
                        }
                    }
                    else
                    {
                        npc.ai[0] = 0f;
                    }
                    break;
                }
                case 9:
                    if (npc.ai[1] == 0f && !AliveCheck(val))
                    {
                        break;
                    }
                    npc.velocity = Vector2.Zero;
                    npc.localAI[2] = 0f;
                    if (npc.ai[1] < 60f)
                    {
                        FancyFireballs((int)npc.ai[1]);
                    }
                    if ((npc.ai[1] += 1f) == 1f)
                    {
                        Main.PlaySound(SoundID.Roar, npc.Center, 0);
                        npc.ai[3] = npc.DirectionTo(val.Center).ToRotation();
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(npc.Center, npc.ai[3].ToRotationVector2(), ModContent.ProjectileType<GunGodDeathraySmall>(), 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, -npc.ai[3].ToRotationVector2(), ModContent.ProjectileType<GunGodDeathraySmall>(), 0, 0f, Main.myPlayer);
                        }
                    }
                    else if (npc.ai[1] == 61f)
                    {
                        for (int n = -1; n <= 1; n += 2)
                        {
                            Vector2 vector4 = npc.ai[3].ToRotationVector2() * n * 3f;
                            for (int num23 = 0; num23 < 20; num23++)
                            {
                                int num24 = Dust.NewDust(npc.Center, 0, 0, 31, vector4.X, vector4.Y, 0, default(Color), 3f);
                                Dust obj4 = Main.dust[num24];
                                obj4.velocity *= 1.4f;
                            }
                            for (int num25 = 1; num25 <= 14; num25++)
                            {
                                float num26 = num25 * n * 100f / 30f;
                                float num27 = ((num25 % 2 != 0) ? 1 : (-1));
                                Vector2 vector5 = num26 * npc.ai[3].ToRotationVector2();
                                for (int num28 = 0; num28 < 3; num28++)
                                {
                                    int num29 = Dust.NewDust(npc.Center, 0, 0, 70, vector5.X, vector5.Y, 0, default(Color), 3f);
                                    Dust obj5 = Main.dust[num29];
                                    obj5.velocity *= 1.5f;
                                    Main.dust[num29].noGravity = true;
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(npc.Center, vector5, ModContent.ProjectileType<ScytheSpin>(), npc.damage * 3 / 8, 0f, Main.myPlayer, (float)npc.whoAmI, num27);
                                }
                            }
                        }
                    }
                    else if (npc.ai[1] > 481f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                    }
                    break;
                case 10:
                    if (!(npc.ai[1] < 90f) || AliveCheck(val))
                    {
                        if (npc.ai[2] == 0f && npc.ai[3] == 0f)
                        {
                            npc.ai[2] = npc.Center.X + (val.Center.X < npc.Center.X ? -1400 : 1400);
                        }
                        if (npc.localAI[2] == 0f)
                        {
                            npc.localAI[2] = !(npc.ai[2] > npc.Center.X) ? 1 : -1;
                        }
                        if (npc.ai[1] > 90f)
                        {
                            FancyFireballs((int)npc.ai[1] - 90);
                        }
                        else
                        {
                            npc.ai[3] = val.Center.Y - 300f;
                        }
                        vector = new Vector2(npc.ai[2], npc.ai[3]);
                        Movement(vector, 1.4f);
                        if ((npc.ai[1] += 1f) > 150f)
                        {
                            Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                            npc.netUpdate = true;
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = npc.localAI[2];
                            npc.ai[3] = 0f;
                            npc.localAI[2] = 0f;
                        }
                    }
                    break;
                case 11:
                    npc.velocity.X = npc.ai[2] * 18f;
                    MovementY(val.Center.Y - 250f, Math.Abs(val.Center.Y - npc.Center.Y) < 200f ? 2f : 0.7f);
                    npc.direction = npc.spriteDirection = Math.Sign(npc.velocity.X);
                    if ((npc.ai[3] += 1f) > 5f)
                    {
                        npc.ai[3] = 0f;
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        float num12 = 2400f / Math.Abs(npc.velocity.X) * 2f - npc.ai[1] + 120f;
                        if (npc.ai[1] <= 15f)
                        {
                            num12 = 0f;
                        }
                        else
                        {
                            if (npc.localAI[2] != 0f)
                            {
                                num12 = 0f;
                            }
                            if ((npc.localAI[2] += 1f) > 2f)
                            {
                                npc.localAI[2] = 0f;
                            }
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.UnitY, (double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), default(Vector2)), ModContent.ProjectileType<GunGodDeathrayMark>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num12);
                            Projectile.NewProjectile(npc.Center, -Utils.RotatedBy(Vector2.UnitY, (double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), default(Vector2)), ModContent.ProjectileType<GunGodDeathrayMark>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num12);
                        }
                    }
                    if ((npc.ai[1] += 1f) > 2400f / Math.Abs(npc.velocity.X))
                    {
                        npc.netUpdate = true;
                        npc.velocity.X = npc.ai[2] * 18f;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                    }
                    break;
                case 12:
                    if (!(npc.ai[1] < 150f) || AliveCheck(val))
                    {
                        npc.velocity.Y = 0f;
                        npc.velocity *= 0.947f;
                        npc.ai[3] += npc.velocity.Length();
                        if (npc.ai[1] > 150f)
                        {
                            FancyFireballs((int)npc.ai[1] - 150);
                        }
                        if ((npc.ai[1] += 1f) > 210f)
                        {
                            Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                            npc.netUpdate = true;
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                    }
                    break;
                case 13:
                    npc.velocity.X = npc.ai[2] * -18f;
                    MovementY(val.Center.Y - 250f, Math.Abs(val.Center.Y - npc.Center.Y) < 200f ? 2f : 0.7f);
                    npc.direction = (npc.spriteDirection = Math.Sign(npc.velocity.X));
                    if ((npc.ai[3] += 1f) > 5f)
                    {
                        npc.ai[3] = 0f;
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        float num33 = 2400f / Math.Abs(npc.velocity.X) * 2f - npc.ai[1] + 120f;
                        if (npc.ai[1] <= 15f)
                        {
                            num33 = 0f;
                        }
                        else
                        {
                            if (npc.localAI[2] != 0f)
                            {
                                num33 = 0f;
                            }
                            if ((npc.localAI[2] += 1f) > 2f)
                            {
                                npc.localAI[2] = 0f;
                            }
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(npc.Center, Utils.RotatedBy(Vector2.UnitY, (double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), default(Vector2)), ModContent.ProjectileType<GunGodDeathrayMark>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num33);
                            Projectile.NewProjectile(npc.Center, -Utils.RotatedBy(Vector2.UnitY, (double)MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), default(Vector2)), ModContent.ProjectileType<GunGodDeathrayMark>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num33);
                        }
                    }
                    if ((npc.ai[1] += 1f) > 2400f / Math.Abs(npc.velocity.X))
                    {
                        npc.netUpdate = true;
                        npc.velocity.X = npc.ai[2] * -18f;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                    break;
                case 14:
                    if (AliveCheck(val))
                    {
                        npc.velocity *= 0.9f;
                        if ((npc.ai[1] += 1f) > 60f)
                        {
                            npc.netUpdate = true;
                            npc.ai[0] = (npc.dontTakeDamage ? (npc.ai[0] + 1f) : 0f);
                            npc.ai[1] = 0f;
                        }
                    }
                    break;
                case 15:
                {
                    npc.velocity *= 0.9f;
                    if (npc.ai[1] < 60f)
                    {
                        FancyFireballs((int)npc.ai[1]);
                    }
                    if (npc.ai[1] == 0f && npc.ai[2] != 2f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float num13 = npc.ai[2] != 1f ? 1 : -1;
                        num13 *= MathHelper.ToRadians(270f) / 120f * -1f * 60f;
                        int num14 = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 3f, num13);
                        if (num14 != 1000)
                        {
                            Main.projectile[num14].localAI[1] = npc.whoAmI;
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num14);
                            }
                        }
                    }
                    if ((npc.ai[1] += 1f) > 90f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.velocity = npc.DirectionTo(val.Center) * 3f;
                    }
                    else if (npc.ai[1] == 60f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;
                        npc.velocity = Vector2.Zero;
                        Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        float num15 = ((npc.ai[2] != 1f) ? 1 : (-1));
                        num15 *= MathHelper.ToRadians(270f) / 120f;
                        Vector2 vector2 = Utils.RotatedBy(npc.DirectionTo(val.Center), (double)((0f - num15) * 60f), default(Vector2));
                        Projectile.NewProjectile(npc.Center, vector2, ModContent.ProjectileType<GunGodSword>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num15, (float)npc.whoAmI);
                    }
                    break;
                }
                case 16:
                    npc.direction = (npc.spriteDirection = Math.Sign(npc.velocity.X));
                    if ((npc.ai[1] += 1f) > 120f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                    }
                    break;
                case 17:
                    if (!AliveCheck(val))
                    {
                        break;
                    }
                    vector = val.Center + val.DirectionTo(npc.Center) * 500f;
                    if (((Entity)npc).Distance(vector) > 50f)
                    {
                        Movement(vector, 0.7f);
                    }
                    if ((npc.ai[1] += 1f) > 60f)
                    {
                        npc.netUpdate = true;
                        if ((npc.ai[2] += 1f) < 2f)
                        {
                            npc.ai[0] -= 2f;
                        }
                        else
                        {
                            npc.ai[0] += 1f;
                            npc.ai[2] = 0f;
                        }
                        npc.ai[1] = 0f;
                    }
                    break;
                case 18:
                {
                    if (npc.ai[1] < 90f && !AliveCheck(val))
                    {
                        break;
                    }
                    if (npc.ai[2] == 0f && npc.ai[3] == 0f)
                    {
                        npc.netUpdate = true;
                        npc.ai[2] = val.Center.X;
                        npc.ai[3] = val.Center.Y;
                        if (AlbedoUtils.ProjectileExists(_ritualProj, ModContent.ProjectileType<Arena1>()) != null)
                        {
                            npc.ai[2] = Main.projectile[_ritualProj].Center.X;
                            npc.ai[3] = Main.projectile[_ritualProj].Center.Y;
                        }
                        Vector2 vector12 = default(Vector2);
                        vector12.X = Math.Sign(val.Center.X - npc.ai[2]);
                        vector12.Y = Math.Sign(val.Center.Y - npc.ai[3]);
                        npc.localAI[2] = Utils.ToRotation(vector12);
                    }
                    Vector2 vector13 = (float)Math.Sqrt(2880000.0) * npc.localAI[2].ToRotationVector2();
                    vector13.Y -= 450 * Math.Sign(vector13.Y);
                    vector = new Vector2(npc.ai[2], npc.ai[3]) + vector13;
                    Movement(vector, 1f);
                    if (npc.ai[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float num47 = Math.Sign(npc.ai[2] - vector.X);
                        float num48 = Math.Sign(npc.ai[3] - vector.Y);
                        float num49 = (num47 > 0f) ? (MathHelper.ToRadians(0.1f) * (0f - num48)) : ((float)Math.PI - MathHelper.ToRadians(0.1f) * (0f - num48));
                        float num50 = (num47 > 0f) ? ((float)Math.PI) : 0f;
                        int num51 = Projectile.NewProjectile(npc.Center, num49.ToRotationVector2(), ModContent.ProjectileType<GlowLine>(), npc.damage / 4, 0f, Main.myPlayer, 4f, num50);
                        if (num51 != 1000)
                        {
                            Main.projectile[num51].localAI[1] = npc.whoAmI;
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num51);
                            }
                        }
                    }
                    if (npc.ai[1] > 90f)
                    {
                        FancyFireballs((int)npc.ai[1] - 90);
                    }
                    if ((npc.ai[1] += 1f) > 150f)
                    {
                        npc.netUpdate = true;
                        npc.velocity = Vector2.Zero;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                    }
                    break;
                }
                case 19:
                    npc.direction = (npc.spriteDirection = Math.Sign(npc.ai[2] - npc.Center.X));
                    if (npc.ai[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float num30 = Math.Sign(npc.ai[2] - npc.Center.X);
                        float num31 = Math.Sign(npc.ai[3] - npc.Center.Y);
                        float num32 = num30 * (float)Math.PI / 60f * num31;
                        Projectile.NewProjectile(npc.Center, Vector2.UnitX * (0f - num30), ModContent.ProjectileType<GunGodSword>(), npc.damage * 3 / 8, 0f, Main.myPlayer, num32, (float)npc.whoAmI);
                    }
                    if ((npc.ai[1] += 1f) > 60f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                        npc.velocity.X = 0f;
                        npc.velocity.Y = 24 * Math.Sign(npc.ai[3] - npc.Center.Y);
                    }
                    break;
                case 20:
                {
                    npc.velocity.Y *= 0.97f;
                    npc.position += npc.velocity;
                    npc.direction = (npc.spriteDirection = Math.Sign(npc.ai[2] - npc.Center.X));
                    if ((npc.ai[1] += 1f) > 90f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] += 1f;
                        npc.ai[1] = 0f;
                    }
                    break;
                }
                case 21:
                    if (AliveCheck(val))
                    {
                        npc.localAI[2] = 0f;
                        vector = val.Center;
                        vector.X += 500 * ((!(npc.Center.X < vector.X)) ? 1 : (-1));
                        if (((Entity)npc).Distance(vector) > 50f)
                        {
                            Movement(vector, 0.7f);
                        }
                        if ((npc.ai[1] += 1f) > 60f)
                        {
                            npc.netUpdate = true;
                            npc.ai[0] = (npc.dontTakeDamage ? (-3) : 0);
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                        }
                    }
                    break;
                default:
                    npc.netUpdate = true;
                    npc.ai[0] = 0f;
                    goto case 0;
            }
            if (npc.ai[0] >= 9f && npc.dontTakeDamage)
            {
                for (int num62 = 0; num62 < 5; num62++)
                {
                    int num63 = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 1.5f);
                    Main.dust[num63].noGravity = true;
                    Dust obj11 = Main.dust[num63];
                    obj11.velocity *= 4f;
                }
            }
            if (val.immune || val.hurtCooldowns[0] != 0 || val.hurtCooldowns[1] != 0)
            {
            }
            void FancyFireballs(int repeats)
            {
                float num64 = 0f;
                for (int num65 = 0; num65 < repeats; num65++)
                {
                    num64 = MathHelper.Lerp(num64, 1f, 0.08f);
                }
                float num66 = 1400f * (1f - num64);
                float num67 = (float)Math.PI * 2f * num64;
                for (int num68 = 0; num68 < 4; num68++)
                {
                    int num69 = Dust.NewDust(npc.Center + num66 * Utils.RotatedBy(Vector2.UnitX, (double)(num67 + (float)Math.PI / 2f * num68), default(Vector2)), 0, 0, 70, npc.velocity.X * 0.3f, npc.velocity.Y * 0.3f, 0, Color.White);
                    Main.dust[num69].noGravity = true;
                    Main.dust[num69].scale = 6f - 4f * num64;
                }
            }
        }

        private void Aura(float distance, int buff, bool reverse = false, int dustID = 228, bool checkDuration = false, bool targetEveryone = true)
        {
            Player val = targetEveryone ? Main.player[Main.myPlayer] : Main.player[npc.target];
            float num = npc.Distance(val.Center);
            if (!reverse ? num < distance : (num > distance && num < 5000f))
            {
                val.AddBuff(buff, checkDuration && Main.expertMode && Main.expertDebuffTime > 1f ? 1 : 2);
            }
            for (int i = 0; i < 30; i++)
            {
                Vector2 vector = default(Vector2);
                double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
                vector.X += (float)(Math.Sin(num2) * distance);
                vector.Y += (float)(Math.Cos(num2) * distance);
                Dust val2 = Main.dust[Dust.NewDust(npc.Center + vector - new Vector2(4f, 4f), 0, 0, dustID, 0f, 0f, 100, Color.White, 1.5f)];
                val2.velocity = npc.velocity;
                if (Main.rand.NextBool(3))
                {
                    val2.velocity += Vector2.Normalize(vector) * (reverse ? 5f : -5f);
                }
                val2.noGravity = true;
            }
        }

        private bool AliveCheck(Player player)
        {
            if ((!player.active || player.dead || Vector2.Distance(npc.Center, player.Center) > 5000f) && npc.localAI[3] > 0f)
            {
                npc.TargetClosest();
                player = Main.player[npc.target];
                if (!player.active || player.dead || Vector2.Distance(npc.Center, player.Center) > 5000f)
                {
                    if (npc.timeLeft > 30)
                    {
                        npc.timeLeft = 30;
                    }
                    npc.velocity.Y -= 1f;
                    if (npc.timeLeft == 1)
                    {
                        if (npc.position.Y < 0f)
                        {
                            npc.position.Y = 0f;
                        }
                    }
                    return false;
                }
            }
            if (npc.timeLeft < 600)
            {
                npc.timeLeft = 600;
            }
            return true;
        }

        private bool Phase2Check()
        {
            if (npc.localAI[3] > 1f)
            {
                return false;
            }
            if (npc.life < npc.lifeMax / 2 && Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[0] = -1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    AlbedoUtils.ClearHostileProjectiles(2, npc.whoAmI);
                }
                return true;
            }
            return false;
        }

        private void Movement(Vector2 targetPos, float speedModifier, bool fastX = true)
        {
            if (Math.Abs(npc.Center.X - targetPos.X) > 5f)
            {
                if (npc.Center.X < targetPos.X)
                {
                    npc.velocity.X += speedModifier;
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X += speedModifier * (!fastX ? 1 : 2);
                    }
                }
                else
                {
                    npc.velocity.X -= speedModifier;
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X -= speedModifier * (!fastX ? 1 : 2);
                    }
                }
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += speedModifier;
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y += speedModifier * 2f;
                }
            }
            else
            {
                npc.velocity.Y -= speedModifier;
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y -= speedModifier * 2f;
                }
            }
            if (Math.Abs(npc.velocity.X) > 24f)
            {
                npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
            }
            if (Math.Abs(npc.velocity.Y) > 24f)
            {
                npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
            }
        }

        private void MovementY(float targetY, float speedModifier)
        {
            if (npc.Center.Y < targetY)
            {
                npc.velocity.Y += speedModifier;
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y += speedModifier * 2f;
                }
            }
            else
            {
                npc.velocity.Y -= speedModifier;
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y -= speedModifier * 2f;
                }
            }
            if (Math.Abs(npc.velocity.Y) > 24f)
            {
                npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(30, 600);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 3; i++)
            {
                int num = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].noGravity = true;
                Dust obj = Main.dust[num];
                obj.velocity *= 3f;
            }
        }

        public override bool CheckDead()
        {
            if (npc.ai[0] == -3f && npc.ai[1] >= 180f)
            {
                return true;
            }
            npc.life = 1;
            npc.active = true;
            if (npc.localAI[3] < 2f)
            {
                npc.localAI[3] = 2f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[0] > -2f)
            {
                npc.ai[0] = -2;
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
                npc.ai[3] = 0f;
                npc.localAI[2] = 0f;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                AlbedoUtils.ClearHostileProjectiles(2, npc.whoAmI);
            }
            return false;
        }

        public override void NPCLoot()
        {
            if (!AlbedoWorld.DownedGunGod)
            {
                AlbedoWorld.DownedGunGod = true;
                AlbedoUtils.Chat(Language.GetTextValue("Mods.Albedo.BossMassage.GunGod"), Color.Purple);
            }
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 3544;
        }

        public override void FindFrame(int frameHeight)
        {
            if ((npc.frameCounter += 1.0) > 6.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y >= 4 * frameHeight)
                {
                    npc.frame.Y = 0;
                }
            }
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle frame = npc.frame;
            Vector2 origin = Utils.Size(frame) / 2f;
            Color color = lightColor;
            npc.GetAlpha(color);
            SpriteEffects effects = npc.spriteDirection >= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor), npc.rotation, origin, npc.scale, effects, 0f);
            return false;
        }
    }
}