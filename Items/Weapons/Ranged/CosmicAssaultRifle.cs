using Albedo.Base;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace Albedo.Items.Weapons.Ranged
{
	public class CosmicAssaultRifle : AlbedoItem
	{
		protected override int Rarity => 4;

		public override void SetDefaults()
		{
			item.damage = 190;
			item.width = 64;
			item.height = 28;
			item.ranged = true;
			item.useTime = 15;
			item.shoot = ProjectileID.ChlorophyteBullet;

			item.shootSpeed = 20f;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 1000000;
			item.useAmmo = AmmoID.Bullet;
			item.crit = 7;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			type = 207;
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-22, 0);

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/Weapons/Ranged/CosmicAssaultRifle_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}