using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ID;
using Terraria.ModLoader;
using static Albedo.AlbedoUtils;
using Microsoft.Xna.Framework;

namespace Albedo.Items.Weapons.Ranged
{
	public class RustleDunes : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 19;
			item.ranged = true;
			item.width = 48;
			item.height = 30;
			item.useTime = 3;
			item.reuseDelay = 8;
			item.useAnimation = 3;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 6f;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 6f;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return CustomRarity(3533, line);
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SandBlock, 50);
			recipe.AddIngredient(ItemID.AntlionMandible, 15);
			recipe.AddIngredient(ItemID.Amber, 10);
			recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 15);
			recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}