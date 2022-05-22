using Albedo.Global;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Albedo.Buffs.Boss
{
	public class GunDemonCurse : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex) => player.GetModPlayer<AlbedoPlayer>().HellGuardCurse = true;
	}
}