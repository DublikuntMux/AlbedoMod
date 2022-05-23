using Albedo.Projectiles.Weapons.Ranged.ZenithGun;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Buffs.Item
{
	public class ZenithGunMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SdmgMinion>()] == 0) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}