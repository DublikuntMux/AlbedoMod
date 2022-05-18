using Albedo.Base;
using Albedo.Tiles.MusicBox;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.MusicBox
{
	public class HellGuardBox : AlbedoItem
	{
		protected override int Rarity => 9;

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = ModContent.TileType<HellGuardBoxTile>();
			item.width = 24;
			item.height = 24;
			item.value = 100000;
			item.accessory = true;
		}
	}
}