using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Items.Weapons.Guns
{
	public class Walter : ModItem
	{
		public override void SetDefaults()
		{
			((ModItem)this).get_item().damage = 21;
			((Entity)((ModItem)this).get_item()).width = 28;
			((Entity)((ModItem)this).get_item()).height = 26;
			((ModItem)this).get_item().useTime = 8;
			((ModItem)this).get_item().useAnimation = 24;
			((ModItem)this).get_item().reuseDelay = 12;
			((ModItem)this).get_item().useStyle = 5;
			((ModItem)this).get_item().ranged = (((ModItem)this).get_item().noMelee = true);
			((ModItem)this).get_item().knockBack = 0f;
			((ModItem)this).get_item().value = Item.sellPrice(0, 1, 10, 0);
			((ModItem)this).get_item().rare = 2;
			((ModItem)this).get_item().UseSound = SoundID.Item36;
			((ModItem)this).get_item().autoReuse = true;
			((ModItem)this).get_item().shoot = ModContent.ProjectileType<Holowave>();
			((ModItem)this).get_item().shootSpeed = 1f;
			((ModItem)this).get_item().useAmmo = ModContent.ItemType<Holoclip>();
			((ModItem)this).get_item().glowMask = SplitGlowMask.Get("Walter");
			((ModItem)this).get_item().crit = 12;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.itemAnimation >= ((ModItem)this).get_item().useAnimation - 2)
			{
				Helper.PlaySound("Items/walterShot", null, 0.75f, Utils.NextFloat(Main.rand, 0f, 0.3f), sync: true);
			}
			position += new Vector2(speedX, speedY) * 1.5f;
			return true;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return player.itemAnimation >= ((ModItem)this).get_item().useAnimation - 2;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 0f);
		}

		public Walter()
			: this()
		{
		}
	}
}
