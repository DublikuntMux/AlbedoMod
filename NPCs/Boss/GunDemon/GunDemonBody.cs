using System;
using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.NPCs.Boss.GunDemon
{
    public class GunDemonBody : WormBase
    {
        private const int MaxCooldown = 240;

        public float ShootCooldown
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 26;
            npc.height = 48;
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
            if (JustSpawned)
            {
                ShootCooldown = MaxCooldown;
                JustSpawned = false;
            }

            CheckSegments();
            TryShoot();
            DustFx();
        }

        private void TryShoot()
        {
            if (Main.rand.NextBool())
                ShootCooldown -= 1;

            npc.TargetClosest(false);

            if (Main.netMode != NetmodeID.MultiplayerClient
                && npc.HasValidTarget
                && ShootCooldown <= 0)
            {
                ShootCooldown = MaxCooldown;
                var velocity = VelocityFptp(npc.Center, Main.player[npc.target].Center, 4);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, 326, 10, 1f);
            }
        }

        private static Vector2 VelocityFptp(Vector2 pos1, Vector2 pos2, float speed)
        {
            var move = pos2 - pos1;
            return move * (speed / (float) Math.Sqrt(move.X * move.X + move.Y * move.Y));
        }

        private void DustFx()
        {
            if (Main.rand.NextBool(3))
                for (var i = 0; i < 2; i++)
                {
                    var dust = Dust.NewDustPerfect(npc.position, 6, Alpha: 200);
                    dust.noGravity = true;
                }
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