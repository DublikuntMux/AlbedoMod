using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
    public class DistortedGun : AlbedoItem
    {
        protected override int Rarity => 9;

        public override void SetDefaults()
        {
            item.damage = 26;
            item.ranged = true;
            item.width = 42;
            item.height = 30;

            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 2);
            item.UseSound = SoundID.Item40;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 15f;
            item.useAmmo = AmmoID.Bullet;
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            type = 307;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override Vector2? HoldoutOffset()
        {
            return Vector2.Zero;
        }
    }
}