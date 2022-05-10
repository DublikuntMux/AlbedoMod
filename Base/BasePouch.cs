using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BasePouch : ModItem
	{
		protected abstract int AmmunitionItem { get; }

		public override void SetDefaults()
		{
			item.CloneDefaults(AmmunitionItem);
			item.width = 26;
			item.height = 26;
			item.consumable = false;
			item.maxStack = 1;
			Item item1 = item;
			item1.value *= 3996;
			item.rare = ItemRarityID.Yellow;
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.CustomRarity(3027, line);
		}
		
		public override void AddRecipes()
		{
			ModRecipe val = new ModRecipe(mod);
			val.AddIngredient(AmmunitionItem, 3996);
			val.AddTile(TileID.CrystalBall);
			val.SetResult(this);
			val.AddRecipe();
		}
	}
}
