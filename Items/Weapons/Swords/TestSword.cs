using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Weapons.Swords
{
	public class TestSword : ModItem
	{
		public override void SetDefaults() 
		{
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(silver: 2);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			
			item.knockBack = 6;
			item.damage = 50;
			item.melee = true;
			item.autoReuse = true;
		}
	}
}
