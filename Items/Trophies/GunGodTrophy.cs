using Albedo.Base;
using Albedo.Tiles.Trophies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Trophies
{
	public class GunGodTrophy : AlbedoItem
	{
		protected override int Rarity => 11;

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.value = Item.sellPrice(0, 1);
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<GunGodTrophyTile>();
		}
	}
}