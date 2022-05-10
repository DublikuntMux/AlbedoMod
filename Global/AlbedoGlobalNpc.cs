using Albedo.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Global
{
    public class AlbedoGlobalNpc : GlobalNPC
    {
        public static int HellGuard;
        public static int GunMaster;
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.GiantBat)
            {
                if (Main.rand.Next(0, 100) >= 30)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Gunpowder>());
                }
            }
            
            if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AlbedoPlayer>().ZoneGrap) {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GunMasterSoul>());
            }
            
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.ArmsDealer)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Gunpowder>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver:20);
                nextSlot++;
            }
        }
    }
}
