using Albedo.Base;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
    public class ImprovedMusket : AlbedoItem
    {
        protected override int Rarity => 7;

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = 36;
            item.useTime = 36;

            item.width = 44;
            item.height = 14;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.damage = 333;
            item.shootSpeed = 9f;
            item.noMelee = true;
            item.value = 100000;
            item.knockBack = 5.25f;
            item.ranged = true;
            item.crit = 7;
        }
    }
}