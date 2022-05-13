using Terraria;
using Terraria.ModLoader;

namespace Albedo.Buffs.Boss
{
    public class GunGodCurse : ModBuff
    {
        public override void SetDefaults() {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = 0;
            player.lifeRegenTime = 0;
            player.allDamage /= 1.3f;
            player.statDefense -= 10;
            player.endurance /= 1.3f;
            player.maxMinions /= 2;
        }
    }
}