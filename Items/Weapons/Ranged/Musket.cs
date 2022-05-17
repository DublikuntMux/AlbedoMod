using Albedo.Base;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
    public class Musket : AlbedoItem
    {
        protected override int Rarity => 7;

        public override void SetDefaults()
        {
            item.damage = 32;
            item.ranged = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 40;
            item.useAnimation = 40;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 8f;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4;
            item.value = 60000;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
        }
    }
}