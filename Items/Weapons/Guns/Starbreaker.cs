using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Guns
{
	public class Starbreaker : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 81;
			item.crit = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item36;
			item.useTime = item.useAnimation = 30;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.LightRed;
			item.value = Item.buyPrice(0, 5);
			item.shootSpeed = 28f;
			item.useAmmo = AmmoID.FallenStar;
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
			Item val = Main.item[whoAmI];
			Texture2D texture = mod.GetTexture("Items/Weapons/Guns/Starbreaker_Glow");
			int num = texture.Height;
			int width = texture.Width;
			SpriteEffects effects = val.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Rectangle rectangle = new Rectangle(0, 0, width, num);
			Vector2 vector = new Vector2(val.Center.X, val.position.Y + val.height - num / 2);
			Main.spriteBatch.Draw(texture, vector - screenPosition, rectangle, Color.White, rotation, Utils.Size(rectangle) / 2f, scale, effects, 0f);
		}
	}
}
