using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Invasion.PossessedWeapon
{
	public class LiveArmorGold : ModNPC
	{
		public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.PossessedArmor];

		public override void SetDefaults() {
			npc.width = 28;
			npc.height = 45;
			npc.damage = 55;
			npc.defense = 28;
			npc.lifeMax = 260;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = Item.buyPrice(0, 0, 1);
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = NPCID.PossessedArmor;
			animationType = NPCID.PossessedArmor;
			banner = Item.NPCtoBanner(NPCID.PossessedArmor);
			bannerItem = Item.BannerToItem(banner);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.damage = 110;
			npc.lifeMax = 520;
			npc.defense = 30;
		}
	}
}