using Albedo.Base;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Ranged
{
    public class ClockworkCannon : AlbedoItem
    {
        protected override int Rarity => 7;

        public override void SetDefaults()
        {
            item.damage = 55;
            item.ranged = true;
            item.width = 66;
            item.height = 34;
            item.useTime = 3;
            item.reuseDelay = 12;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.75f;
            item.value = Item.buyPrice(0, 80);
            item.UseSound = SoundID.Item31;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            var num = speedX + rand.Next(-15, 16) * 0.05f;
            var num2 = speedY + rand.Next(-15, 16) * 0.05f;
            Projectile.NewProjectile(position.X, position.Y, num, num2, type == 14 ? 242 : type, damage, knockBack,
                player.whoAmI);
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return rand.Next(0, 100) >= 33;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cog, 25);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 15);
            recipe.AddTile(ModContent.TileType<WeaponStation2Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}