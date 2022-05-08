using Albedo.Items.Materials;
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
	public class LavaDisaster : ModItem
	{
		private const int NumFrames = 1;
		
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
			if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3526), item, (DrawData?)null);
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
			Texture2D texture = mod.GetTexture("Albedo/Items/Weapons/Guns/LavaDisaster_Glow");
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
			float num = speedX + (float)Main.rand.Next(-10, 11) * 0.05f;
			float num2 = speedY + (float)Main.rand.Next(-10, 11) * 0.05f;
			Projectile.NewProjectile(position.X, position.Y, num, num2, 242, damage, knockBack, ((Entity)player).whoAmI, 0f, 0f);
			return false;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.Next(0, 100) >= 50;
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
