using Albedo.Global;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Buffs.Boss
{
    public class HellGuardCurse : ModBuff
    {
        private bool _brodcast;
        public override void SetDefaults() {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!_brodcast)
            {
                AlbedoUtils.Chat(Language.GetTextValue("Mods.Albedo.BuffDescription.HellGuardCurse"), Color.Red, false);
                player.GetModPlayer<AlbedoPlayer>().Screenshake = 60;
                _brodcast = true;
            }
            player.buffTime[buffIndex] = 36000;
            player.bleed = true;
            player.statDefense /= 2;
            player.endurance /= 2f;
            player.onFire2 = true;
            player.lifeRegen = 0;
            player.allDamage /= 10f;
            player.lifeRegenTime = 0;
        }
    }
}