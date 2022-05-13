using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.NPCs.Boss.GunDemon
{
	public class MagmaLeechHead : ModNPC
	{
		private bool _tailSpawned;
		public override void SetDefaults()
		{
			npc.lifeMax = 250;
			npc.damage = 15;
			npc.defense = 10;
			npc.knockBackResist = 0f;
			npc.width = 74;
			npc.height = 82;
			npc.aiStyle = 6;
			npc.npcSlots = 1f;
			npc.noTileCollide = true;
			npc.behindTiles = true;
			npc.friendly = false;
			npc.dontTakeDamage = false;
			npc.noGravity = true;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.buffImmune[24] = true;
			npc.buffImmune[67] = true;
			npc.lavaImmune = true;
		}

		public override void AI()
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 200, npc.color, 1f);
				Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 200, npc.color, 1f);
			}

			npc.position += npc.velocity;

			if (!_tailSpawned && Main.netMode != 1)
			{
				int previous = npc.whoAmI;
				for (int i = 0; i < 14; i++)
				{
					var newNpcIndex = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + (npc.width / 2), i < 13 ? mod.NPCType("MagmaLeechBody") : mod.NPCType("MagmaLeechTail"), npc.whoAmI);
					Main.npc[newNpcIndex].realLife = npc.whoAmI;
					Main.npc[newNpcIndex].ai[2] = npc.whoAmI;
					Main.npc[newNpcIndex].ai[1] = previous;
					Main.npc[previous].ai[0] = newNpcIndex;
					NetMessage.SendData(23, -1, -1, null, newNpcIndex, 0f, 0f, 0f, 0, 0, 0);
					previous = newNpcIndex;
				}
				_tailSpawned = true;
			}

			if ((int)(Main.time % 180) == 0)
			{
				Vector2 vector = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + npc.height / 2);
				float birdRotation = (float)Math.Atan2(vector.Y - (Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5f), vector.X - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
				npc.velocity.X = (float)(Math.Cos(birdRotation) * 7) * -1;
				npc.velocity.Y = (float)(Math.Sin(birdRotation) * 7) * -1;
				npc.netUpdate = true;
			}
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.rand.NextBool())
				player.AddBuff(BuffID.OnFire, 180, true);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D drawTexture = Main.npcTexture[npc.type];
			Vector2 origin = new Vector2(drawTexture.Width / 2 * 0.5F, drawTexture.Height / Main.npcFrameCount[npc.type] * 0.5F);

			Vector2 drawPos = new Vector2(
				npc.position.X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width / 2 * npc.scale / 2f + origin.X * npc.scale,
				npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + npc.gfxOffY);

			SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(drawTexture, drawPos, npc.frame, Color.White, npc.rotation, origin, npc.scale, effects, 0);

			return false;
		}
	}
}