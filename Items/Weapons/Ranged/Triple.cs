using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
	public class Triple : AlbedoItem
	{
		protected override int Rarity => 3;

		public override void SetDefaults()
		{
			item.damage = 20;
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
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 1f;
			item.useAmmo = AmmoID.Bullet;
			item.crit = 12;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			position += new Vector2(speedX, speedY) * 1.5f;
			return true;
		}

		public override bool ConsumeAmmo(Player player) => player.itemAnimation >= item.useAnimation - 2;

		public override Vector2? HoldoutOffset() => new Vector2(-12f, 0f);
	}
}