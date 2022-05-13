using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
    public abstract class WormBase : ModNPC
    {
        public bool JustSpawned
		{
			get => npc.localAI[0] == 0f;
			set => npc.localAI[0] = value ? 0f : 1f;
		}

		public NPC PrevSegment => Main.npc[(int) npc.ai[1]];
		public NPC ParentHead => Main.npc[(int) npc.ai[2]];

		public override void SetStaticDefaults()
		{
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = 6;
			npc.npcSlots = 5f;
			npc.knockBackResist = 0f;
			music = 17;
			npc.noTileCollide = true;
			npc.behindTiles = true;
			npc.friendly = false;
			npc.noGravity = true;
			npc.dontTakeDamage = false;
			npc.dontCountMe = true;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath10;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode || Main.rand.NextBool())
			{
				player.AddBuff(BuffID.OnFire, 180);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D drawTexture = Main.npcTexture[npc.type];
			Vector2 origin = new Vector2(drawTexture.Width / 2 * 0.5f, drawTexture.Height / Main.npcFrameCount[npc.type] * 0.5f);

			Vector2 drawPos = new Vector2(
				npc.position.X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width / 2 * npc.scale / 2f + origin.X * npc.scale,
				npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + origin.Y * npc.scale + npc.gfxOffY);

			SpriteEffects effects =
				npc.spriteDirection == -1
					? SpriteEffects.None
					: SpriteEffects.FlipHorizontally;

			spriteBatch.Draw(drawTexture, drawPos, npc.frame, Color.White, npc.rotation, origin, npc.scale, effects, 0);

			return false;
		}

		public void CheckSegments()
		{
			if (!PrevSegment.active
				|| !ParentHead.active)
			{
				npc.VanillaHitEffect();
				npc.HitEffect();
				NPCLoot();
				npc.life = 0;
				npc.timeLeft = 0;
				npc.active = false;
			}
		}
    }
}