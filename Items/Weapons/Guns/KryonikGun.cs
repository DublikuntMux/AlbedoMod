using Albedo.Items.Materials;
using Albedo.Projectiles.GunProjectiles;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Guns
{
	public class KryonikGun : ModItem
	{
		private const int NumFrames = 1;
		
		public override void SetDefaults()
		{
			item.damage = 23;
			item.ranged = true;
			item.width = 64;
			item.height = 30;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 2.5f;
			item.value = Item.buyPrice(0, 36);
			item.autoReuse = true;
			item.rare = ItemRarityID.Pink;
			item.shoot = ModContent.ProjectileType<IceShard>();
			item.shootSpeed = 15f;
			item.useAmmo = AmmoID.Bullet;
		}
		
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3554), item, (DrawData?)null);
				Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
				return false;
			}
			return true;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item val = Main.item[whoAmI];
			Texture2D texture = mod.GetTexture("Albedo/Items/Weapons/Guns/KryonikGun_Glow");
			int num = texture.Height / NumFrames;
			int width = texture.Width;
			int y = ((NumFrames > 1) ? (num * Main.itemFrame[whoAmI]) : 0);
			SpriteEffects effects = ((val.direction < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			Rectangle rectangle = new Rectangle(0, y, width, num);
			Vector2 vector = new Vector2(val.Center.X, val.position.Y + val.height - num / 2);
			Main.spriteBatch.Draw(texture, vector - Main.screenPosition, rectangle, Color.White, rotation, Utils.Size(rectangle) / 2f, scale, effects, 0f);
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY),
				type == 14 ? ModContent.ProjectileType<IceShard>() : type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ItemID.Diamond, 15);
			recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 10);
			recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
