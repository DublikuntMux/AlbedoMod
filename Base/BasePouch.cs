using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BasePouch : AlbedoItem
	{
		protected abstract int AmmunitionItem { get; }

		protected override int Rarity => 8;

		public override void SetDefaults()
		{
			item.CloneDefaults(AmmunitionItem);
			base.SetDefaults();
			item.width = 26;
			item.height = 26;
			item.consumable = false;
			item.maxStack = 1;
			var item1 = item;
			item1.value *= 3996;
		}

		public override void AddRecipes()
		{
			var val = new ModRecipe(mod);
			val.AddIngredient(AmmunitionItem, 3996);
			val.AddTile(TileID.CrystalBall);
			val.SetResult(this);
			val.AddRecipe();
		}
	}
}