using Albedo.Base;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Guns
{
	public class TestGun : BaseGun
	{
		protected override float ShootSpeed => 5f;
		protected override int Damage => 20;
		protected override float KnockBack => 4.75f;
		protected override int Crit => 50;
		protected override int Price => Item.buyPrice(silver: 20);
		protected override int Rare => ItemRarityID.Cyan;
		protected override int UseTime => 26;
	}
}
