using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.Buffs.Boss
{
    public class HellGuardCurse : ModBuff
    {
        private bool _broadcast;

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!_broadcast)
            {
                GameHelper.Chat(Language.GetTextValue("Mods.Albedo.BuffDescription.HellGuardCurse"), Color.Red, false);
                player.GetModPlayer<AlbedoPlayer>().Screenshake = 60;
                _broadcast = true;
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