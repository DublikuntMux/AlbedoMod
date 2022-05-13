using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Albedo.NPCs.Boss.GunDemon
{
	public class GunDemonTail : WormBase
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 30;
			npc.height = 62;
			npc.lifeMax = 70000;
			npc.damage = 120;
			npc.defense = 50;
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Burning] = true;
			npc.buffImmune[BuffID.Confused] = true;
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.damage = 165;
			npc.defense = 55;
			npc.lifeMax = 98000;
		}

		public override void AI()
		{
			CheckSegments();
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}
