using System;
using Albedo.Base;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
    public class AlienBlaster : AlbedoItem
    {
        protected override int Rarity => 4;

        public override void SetDefaults()
        {
            item.damage = 136;
            item.magic = true;
            item.mana = 4;
            item.width = 40;
            item.height = 40;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 28440;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ProjectileID.LaserMachinegunLaser;
            item.shootSpeed = 5f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            const int shotAmt = 2;
            const int spread = 5;
            const float spreadMult = 0.3f;

            for (var i = 0; i < shotAmt; i++)
            {
                var vX = 8 * speedX + Main.rand.Next(-spread, spread + 1) * spreadMult;
                var vY = 8 * speedY + Main.rand.Next(-spread, spread + 1) * spreadMult;

                var angle = (float) Math.Atan(vY / vX);
                var vector2 = new Vector2(position.X + 75f * (float) Math.Cos(angle),
                    position.Y + 75f * (float) Math.Sin(angle));
                var mouseX = Main.mouseX + Main.screenPosition.X;
                if (mouseX < player.position.X)
                    vector2 = new Vector2(position.X - 75f * (float) Math.Cos(angle),
                        position.Y - 75f * (float) Math.Sin(angle));

                Projectile.NewProjectile(vector2.X, vector2.Y, vX, vY, ProjectileID.LaserMachinegunLaser, damage,
                    knockBack, Main.myPlayer);
            }

            return false;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/Weapons/Ranged/AlienBlaster_Glow");
            GameHelper.GlowMask(texture, rotation, scale, whoAmI);
        }
    }
}