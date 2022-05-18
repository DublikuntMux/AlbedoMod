using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BaseBullet : AlbedoItem
	{
		protected override int Rarity => 3;

		protected abstract float ShootSpeed { get; }
		protected abstract int Damage { get; }
		protected abstract float KnockBack { get; }
		protected abstract int Price { get; }
		protected abstract int BulletMaterial { get; }
		protected abstract int BulletProjectile { get; }

		public override void SetDefaults()
		{
			item.damage = Damage;
			item.ranged = true;
			item.width = 40;
			item.height = 40;
			item.knockBack = KnockBack;
			item.value = Price;
			item.consumable = true;
			item.shoot = BulletProjectile;
			item.ammo = AmmoID.Bullet;
			item.maxStack = 999;
			item.shootSpeed = ShootSpeed;
		}

		public override void AddRecipes()
		{
			var val = new ModRecipe(mod);
			val.AddIngredient(BulletMaterial, 2);
			val.AddTile(TileID.Anvils);
			val.SetResult(this, 25);
			val.AddRecipe();
		}
	}
}