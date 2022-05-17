using Albedo.Base;
using Albedo.Helper;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Ranged
{
    public class LavaDisaster : AlbedoItem
    {
        protected override int Rarity => 6;

        public override void SetDefaults()
        {
            item.damage = 25;
            item.ranged = true;
            item.width = 70;
            item.height = 18;
            item.useTime = 4;
            item.reuseDelay = 15;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = Item.buyPrice(0, 60);
            item.UseSound = SoundID.Item31;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 11f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/Weapons/Ranged/LavaDisaster_Glow");
            GameHelper.GlowMask(texture, rotation, scale, whoAmI);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            var num = speedX + rand.Next(-10, 11) * 0.05f;
            var num2 = speedY + rand.Next(-10, 11) * 0.05f;
            Projectile.NewProjectile(position.X, position.Y, num, num2, 242, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return rand.Next(0, 100) >= 50;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ItemID.AshBlock, 50);
            recipe.AddIngredient(ItemID.HellfireArrow, 50);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 15);
            recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}