using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Buffs
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
                _brodcast = true;
            }
            player.buffTime[buffIndex] = 36000;
            player.moonLeech = true;
            player.bleed = true;
            player.statDefense -= 20;
            player.endurance -= 0.2f;
            player.venom = true;
            player.onFire2 = true;
            player.lifeRegen = 0;
            player.allDamage /= 10;
            player.lifeRegenTime = 0;
        }
    }
}