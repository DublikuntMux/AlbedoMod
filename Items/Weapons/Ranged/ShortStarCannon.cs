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
	public class ShortStarCannon : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 50;
			item.crit = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item36;
			item.useTime = 30;
			item.useAnimation = 30;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Green;
			item.value = Item.buyPrice(0, 5);
			item.shootSpeed = 28f;
			item.useAmmo = AmmoID.FallenStar;
			item.shoot = ProjectileID.FallingStar;
			item.autoReuse = true;
			item.reuseDelay = 30;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, -2f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < rand.Next(3, 6); i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), 0.35) * rand.NextFloat(0.6f, 1.2f);
				Projectile obj = Projectile.NewProjectileDirect(position + vector * 0.5f, vector, type, damage, knockBack, player.whoAmI, 0f, 0f);
				obj.timeLeft = rand.Next(90, 140);
				obj.scale = rand.NextFloat(0.75f, 1.1f);
			}
			return true;
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/ShortStarCannon_Glow");
			AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.Minishark);
			recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 20);
			recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
