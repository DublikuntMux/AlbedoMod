using Albedo.Base;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
    public class Magmum : AlbedoItem
    {
        protected override int Rarity => 6;

        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 60;
            item.height = 22;
            item.useTime = 15;
            item.useAnimation = 15;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4;
            item.value = 10000;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/Weapons/Ranged/Magmum_Glow");
            GameHelper.GlowMask(texture, rotation, scale, whoAmI);
        }
    }
}