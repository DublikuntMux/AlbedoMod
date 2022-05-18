using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Hell
{
	public class MagmaLeechTail : ModNPC
	{
		public override void SetDefaults()
		{
			npc.lifeMax = 250;
			npc.damage = 15;
			npc.defense = 10;
			npc.width = 30;
			npc.height = 62;
			npc.noTileCollide = true;
			npc.behindTiles = true;
			npc.friendly = false;
			npc.noGravity = true;
			npc.aiStyle = 6;
			npc.HitSound = SoundID.NPCHit2;
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

			if (!Main.npc[(int) npc.ai[1]].active) {
				npc.life = 0;
				npc.HitEffect();
				npc.active = false;
			}
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.rand.NextBool())
				player.AddBuff(BuffID.OnFire, 180);
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

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