using System;
using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Hell
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
			if (Main.rand.NextBool(3)) {
				Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 200, npc.color);
				Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 200, npc.color);
			}

			npc.position += npc.velocity;

			if (!_tailSpawned && Main.netMode != NetmodeID.MultiplayerClient) {
				int previous = npc.whoAmI;
				for (int i = 0; i < 14; i++) {
					int newNpcIndex = NPC.NewNPC((int) npc.position.X + npc.width / 2,
						(int) npc.position.Y + npc.width / 2,
						i < 13 ? mod.NPCType("MagmaLeechBody") : mod.NPCType("MagmaLeechTail"), npc.whoAmI);
					Main.npc[newNpcIndex].realLife = npc.whoAmI;
					Main.npc[newNpcIndex].ai[2] = npc.whoAmI;
					Main.npc[newNpcIndex].ai[1] = previous;
					Main.npc[previous].ai[0] = newNpcIndex;
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, newNpcIndex);
					previous = newNpcIndex;
				}

				_tailSpawned = true;
			}

			if ((int) (Main.time % 180) == 0) {
				var vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height / 2);
				float birdRotation =
					(float) Math.Atan2(
						vector.Y - (Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5f),
						vector.X - (Main.player[npc.target].position.X + Main.player[npc.target].width * 0.5f));
				npc.velocity.X = (float) (Math.Cos(birdRotation) * 7) * -1;
				npc.velocity.Y = (float) (Math.Sin(birdRotation) * 7) * -1;
				npc.netUpdate = true;
			}
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.rand.NextBool())
				player.AddBuff(BuffID.OnFire, 180);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
			SpawnHelper.NormalSpawn(spawnInfo) && SpawnHelper.NoInvasion(spawnInfo) &&
			!AlbedoWorld.DownedGunDemon && spawnInfo.player.ZoneUnderworldHeight
				? 0.07f
				: 0f;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var drawTexture = Main.npcTexture[npc.type];
			var origin = new Vector2(drawTexture.Width / 2 * 0.5F,
				drawTexture.Height / Main.npcFrameCount[npc.type] * 0.5F);

			var drawPos = new Vector2(
				npc.position.X - Main.screenPosition.X + npc.width / 2 -
				Main.npcTexture[npc.type].Width / 2 * npc.scale / 2f + origin.X * npc.scale,
				npc.position.Y - Main.screenPosition.Y + npc.height -
				Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f +
				origin.Y * npc.scale + npc.gfxOffY);

			var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(drawTexture, drawPos, npc.frame, Color.White, npc.rotation, origin, npc.scale, effects, 0);

			return false;
		}
	}
}