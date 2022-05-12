using Albedo.Buffs;
using Albedo.Items.Materials;
using Albedo.NPCs.Boss.HellGuard;
using Terraria;
using Terraria.DataStructures;
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
            if (npc.type == NPCID.GiantBat)
            {
                if (Main.rand.Next(0, 100) >= 30)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Gunpowder>());
                }
            }
            
            if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AlbedoPlayer>().CanGrap) {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GunGodSoul>());
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

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!AlbedoWorld.DownedHellGuard)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active && !Main.player[i].dead)
                        {
                            Main.player[i].AddBuff(ModContent.BuffType<HellGuardCurse>(), 36000, false);
                            break;
                        }
                    }
                }
            }

            return true;
        }
    }
}
