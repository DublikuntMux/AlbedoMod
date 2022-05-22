using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class GemBullet : BaseBullet
	{
		protected override float ShootSpeed => 4.5f;
		protected override int Damage => 3;
		protected override float KnockBack => 2f;
		protected override int Price => Item.buyPrice(copper: 2);
		protected override int BulletMaterial => ItemID.Amber;
		protected override int BulletProjectile => ModContent.ProjectileType<GemBulletProjectile>();

		public override void AddRecipes()
		{
			var recipeSp = new ModRecipe(mod);
			recipeSp.AddIngredient(ItemID.Sapphire, 2);
			recipeSp.AddTile(TileID.Anvils);
			recipeSp.SetResult(this, 20);
			recipeSp.AddRecipe();

			var recipeRb = new ModRecipe(mod);
			recipeRb.AddIngredient(ItemID.Ruby, 2);
			recipeRb.AddTile(TileID.Anvils);
			recipeRb.SetResult(this, 15);
			recipeRb.AddRecipe();

			var recipeEmer = new ModRecipe(mod);
			recipeEmer.AddIngredient(ItemID.Emerald, 2);
			recipeEmer.AddTile(TileID.Anvils);
			recipeEmer.SetResult(this, 15);
			recipeEmer.AddRecipe();

			var recipeTo = new ModRecipe(mod);
			recipeTo.AddIngredient(ItemID.Topaz, 2);
			recipeTo.AddTile(TileID.Anvils);
			recipeTo.SetResult(this, 20);
			recipeTo.AddRecipe();

			var recipeAme = new ModRecipe(mod);
			recipeAme.AddIngredient(ItemID.Amethyst, 2);
			recipeAme.AddTile(TileID.Anvils);
			recipeAme.SetResult(this, 15);
			recipeAme.AddRecipe();

			var recipeDi = new ModRecipe(mod);
			recipeDi.AddIngredient(ItemID.Diamond, 2);
			recipeDi.AddTile(TileID.Anvils);
			recipeDi.SetResult(this, 25);
			recipeDi.AddRecipe();
		}
	}
}