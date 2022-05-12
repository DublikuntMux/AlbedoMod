using Albedo.Projectiles.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Items.Weapons.Ranged
{
	internal class RocketShotgun : ModItem
	{
		private int _rocket;

		public override void SetDefaults()
		{
			item.damage = 24;
			item.width = 40;
			item.height = 16;
			item.useTime = 27;
			item.useAnimation = 27;
			item.reuseDelay = 21;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.ranged = item.noMelee = true;
			item.knockBack = 4f;
			item.value = Item.sellPrice(0, 1, 25);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 4f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (_rocket++ >= 2)
			{
				_rocket = 0;
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), 0.1) * 1;
				speedX = vector.X;
				speedY = vector.Y;
				type = ModContent.ProjectileType<SwitcherRocket>();
				return true;
			}
			for (int i = 0; i < Main.rand.Next(3, 6); i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), 0.2f) * Main.rand.NextFloat(0.8f, 1.2f);
				Projectile.NewProjectile(position, vector, type, damage, knockBack, ((Entity)player).whoAmI, 0f, 0f);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10f, 0f);
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/Ranged/RocketShotgun_Glow");
			AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}
