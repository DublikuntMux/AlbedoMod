using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Items.Weapons.Guns
{
	internal class Switcher : ModItem
	{
		private int rocket;

		public override void SetDefaults()
		{
			((ModItem)this).item.damage = 24;
			((Entity)((ModItem)this).item).width = 40;
			((Entity)((ModItem)this).item).height = 16;
			((ModItem)this).item.useTime = (((ModItem)this).item.useAnimation = 27);
			((ModItem)this).item.reuseDelay = 21;
			((ModItem)this).item.useStyle = 5;
			((ModItem)this).item.ranged = (((ModItem)this).item.noMelee = true);
			((ModItem)this).item.knockBack = 4f;
			((ModItem)this).item.value = Item.sellPrice(0, 1, 25, 0);
			((ModItem)this).item.rare = 2;
			((ModItem)this).item.UseSound = SoundID.Item36;
			((ModItem)this).item.autoReuse = true;
			((ModItem)this).item.shoot = 10;
			((ModItem)this).item.shootSpeed = 4f;
			((ModItem)this).item.useAmmo = AmmoID.Bullet;
			((ModItem)this).item.glowMask = SplitGlowMask.Get("Switcher");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (rocket++ >= 2)
			{
				rocket = 0;
				VectorHelper.PermutateVelocity(ref speedX, ref speedY, 0.1);
				type = ModContent.ProjectileType<SwitcherRocket>();
				return true;
			}
			for (int i = 0; i < Main.rand.Next(3, 6); i++)
			{
				Vector2 vector = VectorHelper.PermutateVelocity(speedX, speedY, 0.2, 0.8f, 1.2f);
				Projectile.NewProjectile(position, vector, type, damage, knockBack, ((Entity)player).whoAmI, 0f, 0f);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10f, 0f);
		}
	}
}
