using System.Linq;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Global
{
    public static class AlbedoUtils
    {
        private static int _defExtraUpdates = -1;
        
        public static void HomeInOnNPC(Projectile projectile, bool ignoreTiles, float distanceRequired, float homingVelocity, float N)
        {
            if (!projectile.friendly)
            {
                return;
            }
            if (_defExtraUpdates == -1)
            {
                _defExtraUpdates = projectile.extraUpdates;
            }
            Vector2 center =projectile.Center;
            bool flag = false;
            for (int i = 0; i < 200; i++)
            {
                float num = npc[i].width / 2 + npc[i].height / 2;
                if (npc[i].CanBeChasedBy(projectile) && projectile.WithinRange(npc[i].Center, distanceRequired + num) && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, npc[i].Center, 1, 1)))
                {
                    center = npc[i].Center;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                projectile.extraUpdates = _defExtraUpdates + 1;
                Vector2 vector = (center - projectile.Center).SafeNormalize(Vector2.UnitY);
                projectile.velocity = (projectile.velocity * N + vector * homingVelocity) / (N + 1f);
            }
            else
            {
                projectile.extraUpdates = _defExtraUpdates;
            }
        }

        public static Vector2 RandomVelocity(float directionMult, float speedLowerLimit, float speedCap, float speedMult = 0.1f)
        {
            Vector2 vector = new Vector2(rand.NextFloat(0f - directionMult, directionMult), rand.NextFloat(0f - directionMult, directionMult));
            while (vector.X == 0f && vector.Y == 0f)
            {
                vector = new Vector2(rand.NextFloat(0f - directionMult, directionMult), rand.NextFloat(0f - directionMult, directionMult));
            }
            vector.Normalize();
            return vector * (rand.NextFloat(speedLowerLimit, speedCap) * speedMult);
        }

        public static bool CustomRarity(int rarity, DrawableTooltipLine line)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                foreach (Item item1 in item) GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(rarity), item1);
                Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
                return false;
            }
            return true;
        }
        
        public static NPC NpcExists(int whoAmI, params int[] types)
        {
            if (whoAmI <= -1 || whoAmI >= 200 || !npc[whoAmI].active || (types.Length != 0 && !types.Contains(npc[whoAmI].type)))
            {
                return null;
            }
            return npc[whoAmI];
        }

        public static NPC NpcExists(float whoAmI, params int[] types)
        {
            return NpcExists((int)whoAmI, types);
        }
        
        public static bool CanDeleteProjectile(Projectile projectile, int deletionRank = 0)
        {
            if (!projectile.active)
            {
                return false;
            }
            if (projectile.damage <= 0)
            {
                return false;
            }
            if (projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank > deletionRank)
            {
                return false;
            }
            if (projectile.friendly)
            {
                if (projectile.whoAmI == player[projectile.owner].heldProj)
                {
                    return false;
                }
                if (IsMinionDamage(projectile, includeMinionShot: false))
                {
                    return false;
                }
            }
            return true;
        }
        
        public static bool IsMinionDamage(Projectile projectile, bool includeMinionShot = true)
        {
            if (projectile.melee || projectile.ranged || projectile.magic)
            {
                return false;
            }
            if (!projectile.minion && !projectile.sentry && !(projectile.minionSlots > 0f))
            {
                if (includeMinionShot)
                {
                    return ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type];
                }
                return false;
            }
            return true;
        }
        
        public static bool BossIsAlive(ref int bossId, int bossType)
        {
            if (bossId != -1)
            {
                if (npc[bossId].active && npc[bossId].type == bossType)
                {
                    return true;
                }
                bossId = -1;
                return false;
            }
            return false;
        }
    }
}
