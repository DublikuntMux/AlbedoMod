using Albedo.Base;
using Albedo.Global;
using Albedo.NPCs.Boss.HellGuard;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Summons
{
    public class SniperVoodooDoll : AlbedoItem
    {
        protected override int Rarity => 12;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 28;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.maxStack = 20;
            item.value = Item.sellPrice(0, 1);
            item.active = true;
            item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<HellGuard>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<HellGuard>());
            return true;
        }
    }
}