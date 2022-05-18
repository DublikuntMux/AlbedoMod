using Albedo.Base;
using Albedo.Projectiles.Combined;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Combined
{
	public class CosmoPouch : AlbedoItem
	{
		protected override int Rarity => 10;

		public override void SetDefaults()
		{
			item.damage = 100;
			item.ranged = true;
			item.width = 26;
			item.height = 26;
			item.knockBack = 8f;
			item.consumable = false;
			item.maxStack = 1;
			item.shoot = ModContent.ProjectileType<CosmoBulletProjectile>();
			item.shootSpeed = 15f;
			item.value = Item.buyPrice(50);
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			var val = new ModRecipe(mod);
			val.AddIngredient(ModContent.ItemType<PrehardmodePouch>());
			val.AddIngredient(ModContent.ItemType<HardmodePouch>());
			val.AddIngredient(ModContent.ItemType<PostLunarPouch>());
			val.AddTile(ModContent.TileType<WeaponStation3Tile>());
			val.SetResult(this);
			val.AddRecipe();
		}
	}
}