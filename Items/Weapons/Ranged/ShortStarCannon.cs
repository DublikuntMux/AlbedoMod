using Albedo.Base;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Ranged
{
	public class ShortStarCannon : AlbedoItem
	{
		protected override int Rarity => 5;

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
			item.value = Item.buyPrice(0, 5);
			item.shootSpeed = 28f;
			item.useAmmo = AmmoID.FallenStar;
			item.shoot = ProjectileID.FallingStar;
			item.autoReuse = true;
			item.reuseDelay = 30;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-5f, -2f);

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < rand.Next(3, 6); i++) {
				var vector = new Vector2(speedX, speedY).RotatedByRandom(0.35) * rand.NextFloat(0.6f, 1.2f);
				var obj = Projectile.NewProjectileDirect(position + vector * 0.5f, vector, type, damage, knockBack,
					player.whoAmI);
				obj.timeLeft = rand.Next(90, 140);
				obj.scale = rand.NextFloat(0.75f, 1.1f);
			}

			return true;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/Weapons/Ranged/ShortStarCannon_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}