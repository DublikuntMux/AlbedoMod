using System.Text.RegularExpressions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BasePouch : ModItem
	{
		protected abstract int AmmunitionItem { get; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(Language.ActiveCulture == GameCulture.Russian
				? Regex.Replace(Name, "([А-Я])", " $1").Trim()
				: Regex.Replace(Name, "([A-Z])", " $1").Trim());
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(AmmunitionItem);
			item.width = 26;
			item.height = 26;
			item.consumable = false;
			item.maxStack = 1;
			Item item1 = item;
			item1.value *= 3996;
			Item item2 = item;
			item2.rare++;
		}

		public override void AddRecipes()
		{
			ModRecipe val = new ModRecipe(mod);
			val.AddIngredient(AmmunitionItem, 3996);
			val.AddTile(TileID.CrystalBall);
			val.SetResult(this, 1);
			val.AddRecipe();
		}
	}
}
