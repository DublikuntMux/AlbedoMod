using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BaseGun : ModItem
	{
		protected abstract float ShootSpeed { get; }
		protected abstract int Damage { get; }
		protected abstract float KnockBack { get; }
		protected abstract int Crit { get; }
		protected abstract int Price { get; }
		protected abstract int Rare { get; }
		protected abstract int UseTime { get; }
		
		public override void SetDefaults() 
		{
			
			item.width = 40;
			item.height = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Price;
			item.rare = Rare;
			item.UseSound = SoundID.Item11;
			
			item.knockBack = KnockBack;
			item.damage = Damage;
			item.autoReuse = true;
			item.crit = Crit;
			item.ranged = true;
			item.noMelee = true;
			item.useTime = UseTime;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = ShootSpeed;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}
