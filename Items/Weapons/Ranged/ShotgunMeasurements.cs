using Albedo.Base;
using Albedo.Global;
using Albedo.Helper;
using Albedo.Items.Materials;
using Albedo.Projectiles.Weapons.Ranged;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Ranged
{
    public class ShotgunMeasurements : AlbedoItem
    {
        protected override int Rarity => 9;

        public override void SetDefaults()
        {
            item.damage = 695;
            item.ranged = true;
            item.useTime = item.useAnimation = 30;
            item.knockBack = 10f;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<AuralisBullet>();
            item.shootSpeed = 7.5f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item31;
            item.width = 96;
            item.height = 34;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack,
                player.whoAmI);
            return false;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/Weapons/Ranged/ShotgunMeasurements_Glow");
            GameHelper.GlowMask(texture, rotation, scale, whoAmI);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return rand.Next(100) >= 50;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 25);
            recipe.AddIngredient(ItemID.IllegalGunParts, 15);
            recipe.AddIngredient(ItemID.SniperRifle);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 50);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 20);
            recipe.AddTile(ModContent.TileType<WeaponStation3Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}