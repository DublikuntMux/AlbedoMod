using Albedo.NPCs.Boss.GunGod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Summons
{
	public class GodTribute : ModItem
	{

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Expert;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.maxStack = 1;
			item.value = Item.sellPrice(0, 1);
			item.active = true;
			item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<GunGod>());
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<GunGod>());
			return true;
		}
		
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.LiveRarity(3556, line);
		}
	}
}
