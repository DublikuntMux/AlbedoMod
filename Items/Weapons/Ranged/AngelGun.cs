using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
	public class AngelGun : AlbedoItem
	{
		protected override int Rarity => 4;

		public override void SetDefaults()
		{
			item.damage = 236;
			item.ranged = true;
			item.width = 50;
			item.maxStack = 1;
			item.height = 30;
			item.useTime = 10;
			item.useAnimation = 15;
			item.shoot = ProjectileID.GreenLaser;

			item.useAmmo = AmmoID.Bullet;
			item.shootSpeed = 15f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4;
			item.value = 31000;

			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -4);

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			type = 20;
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}