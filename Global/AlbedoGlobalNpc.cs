using Albedo.Buffs.Boss;
using Albedo.Items.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Global
{
	public class AlbedoGlobalNpc : GlobalNPC
	{
		public static int HellGuard;
		public static int GunGod;

		public override void NPCLoot(NPC npc)
		{
			switch (npc.type) {
				case NPCID.EaterofSouls:
					if (Main.rand.Next(1000) <= 1)
						Item.NewItem(npc.getRect(), ModContent.ItemType<DistortedGun>());
					break;
				case NPCID.LavaSlime:
					if (Main.rand.Next(800) <= 1)
						Item.NewItem(npc.getRect(), ModContent.ItemType<Magmum>());
					break;
				case NPCID.MartianOfficer:
					if (Main.rand.Next(800) <= 1)
						Item.NewItem(npc.getRect(), ModContent.ItemType<AlienBlaster>());
					break;
				case NPCID.MartianWalker:
					if (Main.rand.Next(800) <= 1)
						Item.NewItem(npc.getRect(), ModContent.ItemType<CosmicAssaultRifle>());
					break;
				case NPCID.IceGolem:
					if (Main.rand.Next(800) <= 1)
						Item.NewItem(npc.getRect(), ModContent.ItemType<KryonikGun>());
					break;
			}
		}

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			switch (type) {
				case NPCID.Pirate:
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<Musket>());
					shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 70);
					if (NPC.downedMechBoss2) {
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<ImprovedMusket>());
						shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 10);
					}

					break;
			}
		}

		public override bool PreAI(NPC npc)
		{
			if (npc.type == NPCID.WallofFlesh)
				if (!AlbedoWorld.DownedHellGuard)
					for (int i = 0; i < 255; i++)
						if (Main.player[i].active && !Main.player[i].dead) {
							Main.player[i].AddBuff(ModContent.BuffType<HellGuardCurse>(), 100, false);
							break;
						}

			return true;
		}
	}
}