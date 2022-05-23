using System;
using Albedo.Helper;
using Albedo.Projectiles.Boss.GunGod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Boss.GunGod
{
	public class MiniGun : ModNPC
	{
		public override string Texture => "Terraria/Item_98";

		public override void SetStaticDefaults()
		{
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.width = 54;
			npc.height = 18;
			npc.defense = 100;
			npc.lifeMax = 20000;
			npc.scale = 2f;
			npc.damage = 100;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0.0f;
			npc.lavaImmune = true;
			npc.aiStyle = -1;
			npc.buffImmune[46] = true;
			npc.buffImmune[24] = true;
			npc.buffImmune[68] = true;
			npc.dontTakeDamage = true;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale) => npc.damage = 200;

		public override void AI()
		{
			var val = EnemyHelper.NpcExists(npc.ai[0], ModContent.NPCType<GunGod>());
			if (val == null || val.dontTakeDamage) {
				if (Main.netMode != NetmodeID.MultiplayerClient) {
					npc.life = 0;
					npc.HitEffect();
					npc.checkDead();
					npc.active = false;
				}

				return;
			}

			npc.target = val.target;
			npc.dontTakeDamage = val.ai[0] == 0f && val.ai[2] < 3f;
			if ((npc.ai[1] += 1f) > 90f) {
				npc.velocity = Vector2.Zero;
				if (npc.ai[3] == 0f) {
					npc.localAI[2] = npc.Distance(Main.player[npc.target].Center);
					npc.ai[3] = npc.DirectionTo(Main.player[npc.target].Center).ToRotation();
					if (npc.whoAmI == NPC.FindFirstNPC(npc.type) && Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(Main.player[npc.target].Center, Vector2.Zero,
							ModContent.ProjectileType<Reticle>(), 0, 0f, Main.myPlayer);
				}

				if (npc.ai[1] > 120f) {
					Main.PlaySound(SoundID.Item12, npc.Center);
					if (Main.netMode != NetmodeID.MultiplayerClient)
						for (int i = 0; i < 5; i++) {
							var vector = 16f * npc.ai[3].ToRotationVector2()
								.RotatedBy((Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 12.0);
							vector *= Main.rand.NextFloat(0.9f, 1.1f);
							int num = Projectile.NewProjectile(npc.Center, vector, ModContent.ProjectileType<Laser>(),
								val.damage / 4, 0f, Main.myPlayer);
							if (num != 1000)
								Main.projectile[num].timeLeft = (int) (npc.localAI[2] / vector.Length()) + 1;
						}

					npc.netUpdate = true;
					npc.ai[1] = 0f;
					npc.ai[3] = 0f;
				}
			}
			else {
				var vector2 = Main.player[npc.target].Center +
					Vector2.UnitX.RotatedBy(npc.ai[2]) * (npc.ai[1] < 45f ? 200 : 500) - npc.Center;
				vector2 /= 8f;
				npc.velocity = (npc.velocity * 19f + vector2) / 20f;
			}

			npc.ai[2] -= 0.045f;
			if (npc.ai[2] < -(float) Math.PI) npc.ai[2] += (float) Math.PI * 2f;
			if (npc.localAI[1] == 0f) npc.localAI[1] = Main.rand.NextBool() ? 1 : -1;
			npc.rotation = npc.DirectionFrom(Main.player[npc.target].Center).ToRotation();
			if (npc.localAI[0] > 180f) npc.localAI[0] = 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 3; i++) {
				int num = Dust.NewDust(npc.position, npc.width, npc.height, DustID.TopazBolt);
				Main.dust[num].noGravity = true;
				var obj = Main.dust[num];
				obj.velocity *= 3f;
			}

			if (npc.life <= 0)
				for (int j = 0; j < 30; j++) {
					int num2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.TopazBolt, 0f, 0f, 0, default,
						2f);
					Main.dust[num2].noGravity = true;
					var obj2 = Main.dust[num2];
					obj2.velocity *= 12f;
				}
		}

		public override Color? GetAlpha(Color drawColor) => Color.White;

		public override bool CheckActive() => false;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture = Main.npcTexture[npc.type];
			var frame = npc.frame;
			var origin = frame.Size() / 2f;
			var color = lightColor;
			color = npc.GetAlpha(color);
			var effects = npc.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++) {
				var color2 = color * 0.5f;
				color2 *= (NPCID.Sets.TrailCacheLength[npc.type] - i) / (float) NPCID.Sets.TrailCacheLength[npc.type];
				var vector = npc.oldPos[i];
				float rotation = npc.rotation;
				Main.spriteBatch.Draw(texture,
					vector + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, color2,
					rotation, origin, npc.scale, effects, 0f);
			}

			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame,
				npc.GetAlpha(lightColor), npc.rotation, origin, npc.scale, effects, 0f);
			return false;
		}
	}
}