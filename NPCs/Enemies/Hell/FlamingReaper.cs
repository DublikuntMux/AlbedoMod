using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Hell
{
    public class FlamingReaper : ModNPC
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 100;
            npc.damage = 60;
            npc.defense = 24;
            npc.knockBackResist = 0f;
            npc.width = 34;
            npc.height = 34;
            animationType = 0;
            npc.aiStyle = 63;
            npc.noGravity = true;
            npc.npcSlots = 15f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 0, 9);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
                for (var k = 0; k < 20; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, 2.5f * hitDirection, -2.5f, 0,
                        default, 0.7f);
        }
    }
}