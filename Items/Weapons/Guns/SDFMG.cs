using Albedo.Global;
using Albedo.Items.Materials;
using Albedo.Projectiles.GunProjectiles;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.Main;

namespace Albedo.Items.Weapons.Guns
{
    public class SDFMG : ModItem
    {
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
            item.value = Item.buyPrice(1, 80, 0, 0);
            item.rare = ItemRarityID.Red;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num = speedX + rand.Next(-5, 6) * 0.05f;
            float num2 = speedY + rand.Next(-5, 6) * 0.05f;
            if (rand.NextBool(5))
            {
                Projectile.NewProjectile(position.X, position.Y, num, num2, ModContent.ProjectileType<FishronRpg>(), damage, knockBack, ((Entity)player).whoAmI, 0f, 0f);
            }
            Projectile.NewProjectile(position.X, position.Y, num, num2, type, damage, knockBack, ((Entity)player).whoAmI, 0f, 0f);
            return false;
        }
        
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return AlbedoUtils.CustomRarity(2873, line);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return rand.Next(0, 100) >= 50;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
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
