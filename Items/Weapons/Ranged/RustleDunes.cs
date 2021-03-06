using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
	public class RustleDunes : AlbedoItem
	{
		protected override int Rarity => 6;

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.damage = 15;
			item.ranged = true;
			item.width = 48;
			item.height = 30;
			item.useTime = 3;
			item.reuseDelay = 8;
			item.useAnimation = 3;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 6f;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 6f;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-5f, 0f);
	}
}