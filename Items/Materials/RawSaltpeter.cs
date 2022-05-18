using Albedo.Base;
using Albedo.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Materials
{
	public class RawSaltpeter : AlbedoItem
	{
		protected override int Rarity => 2;

		public override void SetStaticDefaults() => ItemID.Sets.SortingPriorityMaterials[item.type] = 58;

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.value = Item.buyPrice(copper: 20);
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.maxStack = 999;
			item.consumable = true;
			item.createTile = ModContent.TileType<Saltpeter>();
			item.width = 12;
			item.height = 12;
		}
	}
}