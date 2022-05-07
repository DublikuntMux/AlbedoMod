using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Albedo.AlbedoUtils;

namespace Albedo.Items.Weapons.Guns
{
	public class RustleDunes : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 19;
			item.ranged = true;
			item.width = 48;
			item.height = 30;
			item.useTime = 3;
			item.reuseDelay = 8;
			item.useAnimation = 3;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 6f;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 6f;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return CustomRarity(3533, line);
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5f, 0f);
		}
	}
}
