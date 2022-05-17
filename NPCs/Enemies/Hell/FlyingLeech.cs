using Albedo.Helper;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Hell
{
    public class FlyingLeech : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 80;
            npc.damage = 20;
            npc.defense = 10;
            npc.knockBackResist = 0.5f;
            npc.width = 74;
            npc.height = 42;
            animationType = 2;
            npc.aiStyle = 2;
            npc.noGravity = true;
            npc.npcSlots = 1f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 32, 20);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.NormalSpawn(spawnInfo) && SpawnHelper.NoZoneAllowWater(spawnInfo) && NPC.downedBoss2 &&
                   spawnInfo.player.ZoneCrimson && spawnInfo.spawnTileY < Main.worldSurface
                ? 0.02f
                : 0;
        }
    }
}