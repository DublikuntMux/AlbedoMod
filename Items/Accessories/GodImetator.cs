using Albedo.Base;
using Albedo.Global;
using Terraria;

namespace Albedo.Items.Accessories
{
	public class GodImetator : AlbedoItem
	{
		protected override int Rarity => 9;

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.width = 30;
			item.height = 30;
			item.useTime = 20;
			item.accessory = true;
			item.value = Item.sellPrice(0, 20);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) =>
			player.GetModPlayer<AlbedoPlayer>().GodImitator = true;
	}
}