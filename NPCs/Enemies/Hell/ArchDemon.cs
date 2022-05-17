using Albedo.Helper;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Hell
{
    public class ArchDemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 300;
            npc.damage = 62;
            npc.defense = 2;
            npc.knockBackResist = 0.3f;
            npc.width = 66;
            npc.height = 58;
            animationType = 62;
            npc.aiStyle = 14;
            npc.npcSlots = 15f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = Item.buyPrice(0, 0, 2, 50);
            banner = npc.type;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextFloat(100) <= 5f)
                NPC.NewNPC((int) npc.position.X - 50, (int) npc.position.Y, ModContent.NPCType<FlamingReaper>());
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
                for (var k = 0; k < 20; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.SeaSnail, 2.5f * hitDirection, -2.5f, 0,
                        default, 0.7f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.NormalSpawn(spawnInfo) && SpawnHelper.NoInvasion(spawnInfo) &&
                   spawnInfo.player.ZoneUnderworldHeight
                ? 0.07f
                : 0f;
        }
    }
}