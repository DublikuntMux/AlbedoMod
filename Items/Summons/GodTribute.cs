using Albedo.Base;
using Albedo.Helper;
using Albedo.NPCs.Boss.GunGod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Summons
{
	public class GodTribute : AlbedoItem
	{
		protected override int Rarity => 12;

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.maxStack = 1;
			item.value = Item.sellPrice(0, 1);
			item.active = true;
			item.consumable = false;
		}
		
		public override bool CanUseItem(Player player) => !BossHelper.OtherBossAlive(0);
		
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<GunGod>());
			return true;
		}
	}
}