using Albedo.Projectiles.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Items.Weapons.Ranged
{
	public class Triple : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 21;
			item.width = 28;
			item.height = 26;
			item.useTime = 8;
			item.useAnimation = 24;
			item.reuseDelay = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.ranged = true;
			item.noMelee = true;
			item.knockBack = 0f;
			item.value = Item.sellPrice(0, 1, 10);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Holowave>();
			item.shootSpeed = 1f;
			item.useAmmo = AmmoID.Bullet;
			item.crit = 12;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position += new Vector2(speedX, speedY) * 1.5f;
			return true;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return player.itemAnimation >= item.useAnimation - 2;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 0f);
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/Triple_Glow");
			AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}
