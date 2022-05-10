using Albedo.Global;
using Albedo.Items.Materials;
using Albedo.Projectiles.GunProjectiles;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Guns
{
	public class ShotgunMeasurements : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 695;
			item.ranged = true;
			item.useTime = (item.useAnimation = 30);
			item.knockBack = 10f;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<AuralisBullet>();
			item.shootSpeed = 7.5f;
			item.useAmmo = AmmoID.Bullet;
			item.rare = ItemRarityID.Purple;
			item.width = 96;
			item.height = 34;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.CustomRarity(3039, line);
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item val = Main.item[whoAmI];
			Texture2D texture = mod.GetTexture("Items/Weapons/Guns/ShotgunMeasurements_Glow");
			int num = texture.Height;
			int width = texture.Width;
			SpriteEffects effects = val.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Rectangle rectangle = new Rectangle(0, 0, width, num);
			Vector2 vector = new Vector2(val.Center.X, val.position.Y + val.height - num / 2);
			Main.spriteBatch.Draw(texture, vector - screenPosition, rectangle, Color.White, rotation, Utils.Size(rectangle) / 2f, scale, effects, 0f);
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10f, 0f);
		}

		public override bool ConsumeAmmo(Player player)
		{
			return rand.Next(100) >= 50;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 25);
			recipe.AddIngredient(ItemID.IllegalGunParts, 15);
			recipe.AddIngredient(ItemID.SniperRifle);
			recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 50);
			recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 20);
			recipe.AddTile(ModContent.TileType<WeaponStation3Tile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
