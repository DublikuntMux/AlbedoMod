using Albedo.Base;
using Albedo.Tiles.Ores;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Materials
{
    public class AngeliteOre : AlbedoItem
    {
        protected override int Rarity => 4;

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.value = 12500;
            item.rare = ItemRarityID.Green;
            item.createTile = ModContent.TileType<AngeliteOreTile>();
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
        }
    }
}