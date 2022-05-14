using Albedo.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Materials
{
    public class AlbedoOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityMaterials[item.type] = 58;
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.value = Item.buyPrice(silver: 5);
            item.rare = ItemRarityID.Pink;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            item.createTile = ModContent.TileType<AlbedoOreTitle>();
            item.width = 12;
            item.height = 12;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return AlbedoUtils.LiveRarity(3025, line);
        }
    }
}