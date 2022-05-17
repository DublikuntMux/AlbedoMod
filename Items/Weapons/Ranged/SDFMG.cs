using Albedo.Base;
using Albedo.Items.Materials;
using Albedo.Projectiles.Weapons.Ranged;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Ranged
{
    public class SDFMG : AlbedoItem
    {
        protected override int Rarity => 10;

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 74;
            item.height = 34;
            item.useTime = 2;
            item.useAnimation = 2;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.75f;
            item.value = Item.buyPrice(1, 80);
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += 15;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            var num = speedX + rand.Next(-5, 6) * 0.05f;
            var num2 = speedY + rand.Next(-5, 6) * 0.05f;
            if (rand.NextBool(5))
                Projectile.NewProjectile(position.X, position.Y, num, num2, ModContent.ProjectileType<FishronRpg>(),
                    damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, num, num2, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return rand.Next(0, 100) >= 50;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(ItemID.BubbleGun);
            recipe.AddIngredient(ItemID.DukeFishronTrophy);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 50);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 20);
            recipe.AddTile(ModContent.TileType<WeaponStation3Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}