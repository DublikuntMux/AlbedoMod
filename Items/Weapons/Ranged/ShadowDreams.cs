using Albedo.Base;
using Albedo.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Weapons.Ranged
{
	public class ShadowDreams : AlbedoItem
	{
		protected override int Rarity => 9;

		public override void SetDefaults()
		{
			item.damage = 99;
			item.ranged = true;
			item.width = 76;
			item.height = 36;
			item.useTime = 10;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 2f;
			item.UseSound = SoundID.Item34;
			item.value = 1253000;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<EaterofDreamsProjectile>();
			item.shootSpeed = 5f;
			item.useAmmo = AmmoID.Gel;
		}
	}
}