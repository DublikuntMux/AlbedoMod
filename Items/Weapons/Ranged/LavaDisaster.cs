using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Items.Weapons.Ranged
{
	public class LavaDisaster : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 25;
			item.ranged = true;
			item.width = 70;
			item.height = 18;
			item.useTime = 4;
			item.reuseDelay = 15;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 2f;
			item.value = Item.buyPrice(0, 60, 0, 0);
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item31;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Bullet;
		}
		
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.LiveRarity(3526, line);
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/LavaDisaster_Glow");
			AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = speedX + rand.Next(-10, 11) * 0.05f;
			float num2 = speedY + rand.Next(-10, 11) * 0.05f;
			Projectile.NewProjectile(position.X, position.Y, num, num2, 242, damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return rand.Next(0, 100) >= 50;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 15);
			recipe.AddIngredient(ItemID.AshBlock, 50);
			recipe.AddIngredient(ItemID.HellfireArrow, 50);
			recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 15);
			recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
