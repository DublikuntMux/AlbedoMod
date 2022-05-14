using Albedo.Items.Materials;
using Albedo.Projectiles.Weapons.Ranged;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Weapons.Ranged
{
    public class KryonikGun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 23;
            item.ranged = true;
            item.width = 64;
            item.height = 30;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = Item.buyPrice(0, 36);
            item.autoReuse = true;
            item.UseSound = SoundID.NPCHit5;
            item.rare = ItemRarityID.Pink;
            item.shoot = ModContent.ProjectileType<IceShard>();
            item.shootSpeed = 15f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return AlbedoUtils.LiveRarity(3554, line);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/Weapons/Ranged/KryonikGun_Glow");
            AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY),
                type == 14 ? ModContent.ProjectileType<IceShard>() : type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 50);
            recipe.AddIngredient(ItemID.Diamond, 15);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 10);
            recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}