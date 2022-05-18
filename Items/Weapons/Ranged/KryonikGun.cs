using Albedo.Base;
using Albedo.Helper;
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
	public class KryonikGun : AlbedoItem
	{
		protected override int Rarity => 3;

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
			item.shoot = ModContent.ProjectileType<IceShard>();
			item.shootSpeed = 15f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/Weapons/Ranged/KryonikGun_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-5f, 0f);

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY),
				type == 14 ? ModContent.ProjectileType<IceShard>() : type, damage, knockBack, player.whoAmI);
			return false;
		}
	}
}