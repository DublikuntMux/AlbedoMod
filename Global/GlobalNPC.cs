using Albedo.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Global
{
    public class ModGlobalNpc : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.GiantBat)
            {
                if (Main.rand.Next(0, 100) >= 30)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Gunpowder>());
                }
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
