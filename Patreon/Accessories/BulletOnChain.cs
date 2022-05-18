using Albedo.Items.Materials;
using Albedo.Patreon.Buffs;
using Albedo.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Patreon.Accessories
{
	public class BulletOnChain : PatreonItem
	{
		protected override string Owner => "Bochok";

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 30;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.noMelee = true;
			item.value = Item.sellPrice(gold: 5, silver: 50);
			item.shoot = ModContent.ProjectileType<BulletPet>();
			item.buffType = ModContent.BuffType<BulletPetBuff>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) player.AddBuff(item.buffType, 3600);
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HellGuardSoul>(), 20);
			recipe.AddIngredient(ModContent.ItemType<GunDemonSoul>(), 20);
			recipe.AddIngredient(ModContent.ItemType<GunGodSoul>(), 20);
			recipe.SetResult(this);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.AddRecipe();
		}
	}
}