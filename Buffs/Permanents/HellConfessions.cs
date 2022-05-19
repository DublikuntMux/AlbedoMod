using Albedo.Global;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Buffs.Permanents
{
	public class HellConfessions : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex) =>
			player.GetModPlayer<AlbedoPlayer>().HellConfession = true;
	}
}