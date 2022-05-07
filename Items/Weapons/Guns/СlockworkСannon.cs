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
	public class СlockworkСannon : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 55;
			item.ranged = true;
			item.width = 66;
			item.height = 34;
			item.useTime = 3;
			item.reuseDelay = 12;
			item.useAnimation = 9;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3.75f;
			item.value = Item.buyPrice(0, 80);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item31;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 20f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3597), item, (DrawData?)null);
				Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
				return false;
			}
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = speedX + (float)Main.rand.Next(-15, 16) * 0.05f;
			float num2 = speedY + (float)Main.rand.Next(-15, 16) * 0.05f;
			Projectile.NewProjectile(position.X, position.Y, num, num2, type == 14 ? 242 : type, damage, knockBack,
				((Entity) player).whoAmI, 0f, 0f);
			return false;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.Next(0, 100) >= 33;
		}
	}
}
