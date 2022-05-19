using System;
using System.Collections;
using Albedo.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.Main;

namespace Albedo.Helper
{
	public class BossHelper
	{
		private static NPC NpcExists(int whoAmI, params int[] types)
		{
			if (whoAmI <= -1 || whoAmI >= 200 || !npc[whoAmI].active ||
			    (types.Length != 0 && !((IList) types).Contains(npc[whoAmI].type))) return null;
			return npc[whoAmI];
		}

		public static NPC NpcExists(float whoAmI, params int[] types) => NpcExists((int) whoAmI, types);

		public static bool CanDeleteProjectile(Projectile projectile, int deletionRank = 0)
		{
			if (!projectile.active) return false;
			if (projectile.damage <= 0) return false;
			if (projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank > deletionRank)
				return false;
			if (projectile.friendly) {
				if (projectile.whoAmI == player[projectile.owner].heldProj) return false;
				if (IsMinionDamage(projectile, false)) return false;
			}

			return true;
		}

		private static bool IsMinionDamage(Projectile projectile, bool includeMinionShot = true)
		{
			if (projectile.melee || projectile.ranged || projectile.magic) return false;
			if (!projectile.minion && !projectile.sentry && !(projectile.minionSlots > 0f)) {
				if (includeMinionShot)
					return ProjectileID.Sets.MinionShot[projectile.type] ||
					       ProjectileID.Sets.SentryShot[projectile.type];
				return false;
			}

			return true;
		}

		public static bool BossIsAlive(ref int bossId, int bossType)
		{
			if (bossId != -1) {
				if (npc[bossId].active && npc[bossId].type == bossType) return true;
				bossId = -1;
				return false;
			}

			return false;
		}

		public static Vector2 ClosestPointInHitbox(Entity entity, Vector2 desiredLocation)
		{
			var vector = desiredLocation - entity.Center;
			vector.X = Math.Min(Math.Abs(vector.X), entity.width / 2) * Math.Sign(vector.X);
			vector.Y = Math.Min(Math.Abs(vector.Y), entity.height / 2) * Math.Sign(vector.Y);
			return entity.Center + vector;
		}

		public static Projectile ProjectileExists(int whoAmI, params int[] types)
		{
			if (whoAmI <= -1 || whoAmI >= 1000 || !projectile[whoAmI].active ||
			    (types.Length != 0 && !((IList) types).Contains(projectile[whoAmI].type))) return null;
			return projectile[whoAmI];
		}

		public static void ClearFriendlyProjectiles(int deletionRank = 0, int bossNpc = -1) =>
			ClearProjectiles(false, true, deletionRank, bossNpc);

		public static void ClearHostileProjectiles(int deletionRank = 0, int bossNpc = -1) =>
			ClearProjectiles(true, false, deletionRank, bossNpc);

		private static void ClearProjectiles(bool clearHostile, bool clearFriendly, int deletionRank = 0,
			int bossNpc = -1)
		{
			if (netMode == NetmodeID.MultiplayerClient) return;
			if (OtherBossAlive(bossNpc)) clearHostile = false;
			for (int i = 0; i < 2; i++)
			for (int j = 0; j < 1000; j++) {
				var val = projectile[j];
				if (val.active && ((val.hostile && clearHostile) || (val.friendly && clearFriendly)) &&
				    CanDeleteProjectile(val, deletionRank)) val.Kill();
			}
		}

		public static bool OtherBossAlive(int npcId)
		{
			if (npcId > -1 && npcId < 200)
				for (int i = 0; i < 200; i++)
					if (npc[i].active && npc[i].boss && i != npcId)
						return true;
			return false;
		}

		private static Player PlayerExists(int whoAmI)
		{
			if (whoAmI <= -1 || whoAmI >= 255 || !player[whoAmI].active || player[whoAmI].ghost) return null;
			return player[whoAmI];
		}

		public static Player PlayerExists(float whoAmI) => PlayerExists((int) whoAmI);

		public static int GetByUuidReal(int player, int projectileIdentity, params int[] projectileType)
		{
			for (int i = 0; i < 1000; i++)
				if (projectile[i].active && projectile[i].identity == projectileIdentity &&
				    projectile[i].owner == player &&
				    (projectileType.Length == 0 || ((IList) projectileType).Contains(projectile[i].type)))
					return i;
			return -1;
		}
	}
}