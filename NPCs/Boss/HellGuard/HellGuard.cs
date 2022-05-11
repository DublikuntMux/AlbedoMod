using System;
using Albedo.Global;
using Albedo.Projectiles.Boss.HellGuard;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.NPCs.Boss.HellGuard
{
  [AutoloadBossHead]
  public class HellGuard : ModNPC
  {
    public int ritualProj;
    public override void SetStaticDefaults()
    {
      Main.npcFrameCount[npc.type] = 2;
      NPCID.Sets.TrailCacheLength[npc.type] = 6;
      NPCID.Sets.TrailingMode[npc.type] = 1;
    }

    public override void SetDefaults()
    {
      npc.width = 120; 
      npc.height = 180;
      npc.damage = 50;
      npc.defense = 10;
      npc.lifeMax = 6000;
      npc.HitSound = SoundID.NPCHit41;
      npc.DeathSound = SoundID.NPCDeath44;
      npc.noGravity = true;
      npc.noTileCollide = true;
      npc.knockBackResist = 0.0f;
      npc.lavaImmune = true;
      npc.aiStyle = -1;
      npc.value = Item.buyPrice(0, 15);
      npc.boss = true;
      npc.buffImmune[46] = true;
      npc.buffImmune[24] = true;
      npc.buffImmune[68] = true;
      npc.trapImmune = true;
      npc.dontTakeDamage = true;
      npc.alpha = byte.MaxValue;
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
      npc.damage = 140;
      npc.defense = 18;
      npc.lifeMax = 11000;
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

    public override void AI()
    {
      AlbedoGlobalNpc.HellGuard = npc.whoAmI;
      if (npc.localAI[3] == 0.0)
      {
        if (!npc.HasValidTarget)
          npc.TargetClosest(false);
        if (npc.ai[1] == 0.0)
        {
          npc.Center = Main.player[npc.target].Center + new Vector2(500 * Math.Sign(npc.Center.X - Main.player[npc.target].Center.X), -250f);
          if (Main.netMode != NetmodeID.MultiplayerClient)
          {
            Projectile.NewProjectile(npc.Center + Vector2.UnitY * 1000f, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0f, Main.myPlayer, 0f - Utils.ToRotation(Vector2.UnitY), 10f);
            Projectile.NewProjectile(npc.Center - Vector2.UnitY * 1000f, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), 10f);
          }
        }
        if (++npc.ai[1] <= 54.0)
          return;
        npc.localAI[3] = 1f;
        npc.ai[1] = 0.0f;
        npc.netUpdate = true;
        if (!Main.dedServ && Main.LocalPlayer.active)
          Main.LocalPlayer.GetModPlayer<AlbedoPlayer>().Screenshake = 30;
        if (Main.netMode == NetmodeID.MultiplayerClient)
          return;
        for (int index = 0; index < 8; ++index)
        {
          float num = (float) (0.78 * (index + (double) Main.rand.NextFloat(-0.5f, 0.5f)));
          Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, num, 3f);
        }
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
          ritualProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Arena>(), npc.damage / 4, 0f, Main.myPlayer, 0f, npc.whoAmI);
        }
        int num1 = NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, ModContent.NPCType<HellGuardHand>(), npc.whoAmI, 0.0f, 0.0f, npc.whoAmI, 1f);
        if (num1 < 200 && Main.netMode == NetmodeID.Server)
          NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num1);
        int num2 = NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, ModContent.NPCType<HellGuardHand>(), npc.whoAmI, 0.0f, 0.0f, npc.whoAmI, -1f);
        if (num2 >= 200 || Main.netMode != NetmodeID.Server)
          return;
        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num2);
      }
      else
      {
        Player player = Main.player[npc.target];
        if (npc.HasValidTarget && npc.Distance(player.Center) < 2500.0 && player.ZoneUnderworldHeight)
          npc.timeLeft = 600;
        npc.dontTakeDamage = false;
        npc.alpha = 0;
        switch (npc.ai[0])
        {
          case -1f:
            npc.localAI[2] = 1f;
            ++npc.ai[1];
            NPC npc1 = npc;
            npc1.velocity *= 0.95f;
            if (npc.ai[1] == 120.0)
            {
              Main.PlaySound(SoundID.NPCDeath10, npc.Center);
              if (Main.netMode == NetmodeID.MultiplayerClient)
                break;
              if (!Main.dedServ && Main.LocalPlayer.active)
                Main.LocalPlayer.GetModPlayer<AlbedoPlayer>().Screenshake = 30;
              if (Main.netMode == NetmodeID.MultiplayerClient)
                break;
              float num3 = 0.7853982f * Main.rand.NextFloat();
              for (int index = 0; index < 8; ++index)
              {
                float num4 = num3 + (float) (0.78 * (index + (double) Main.rand.NextFloat(-0.5f, 0.5f)));
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, num4, 3f);
              }
              break;
            }
            if (npc.ai[1] <= 120.0)
              break;
            NPC npc2 = npc;
            npc2.velocity *= 0.9f;
            int num = (int) (npc.lifeMax / 2 / 120 * (double) Main.rand.NextFloat(1f, 1.5f));
            npc.life += num;
            if (npc.life > npc.lifeMax)
              npc.life = npc.lifeMax;
            CombatText.NewText(npc.Hitbox, CombatText.HealLife, num);
            for (int index1 = 0; index1 < 5; ++index1)
            {
              int index2 = Dust.NewDust(npc.Center, 0, 0, 174, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity *= 8f;
            }
            if (npc.ai[1] <= 240.0)
              break;
            ++npc.ai[0];
            npc.ai[1] = 0.0f;
            npc.ai[2] = 0.0f;
            npc.netUpdate = true;
            break;
          case 0.0f:
            if (!player.active || player.dead || (double) Vector2.Distance(npc.Center, player.Center) > 2500.0 || !player.ZoneUnderworldHeight)
            {
              npc.TargetClosest(false);
              if (npc.timeLeft > 30)
                npc.timeLeft = 30;
              npc.noTileCollide = true;
              npc.noGravity = true;
              ++npc.velocity.Y;
              break;
            }
            Vector2 center1 = player.Center;
            center1.Y -= 325f;
            if (((Entity) npc).Distance(center1) > 50.0)
              Movement(center1, 0.4f, 16f, true);
            if (npc.localAI[2] != 0.0 || npc.life >= npc.lifeMax / 2)
              break;
            npc.ai[0] = -1f;
            npc.ai[1] = 0.0f;
            npc.ai[2] = 0.0f;
            npc.ai[3] = 0.0f;
            for (int index = 0; index < 200; ++index)
            {
              if (Main.npc[index].active && Main.npc[index].type == ModContent.NPCType<HellGuardHand>() && Main.npc[index].ai[2] == (double) npc.whoAmI)
              {
                Main.npc[index].ai[0] = -1f;
                Main.npc[index].ai[1] = 0.0f;
                Main.npc[index].localAI[0] = 0.0f;
                Main.npc[index].localAI[1] = 0.0f;
                Main.npc[index].netUpdate = true;
              }
            }
            break;
          case 1f:
            if (!player.active || player.dead || (double) Vector2.Distance(npc.Center, player.Center) > 2500.0 || !player.ZoneUnderworldHeight)
            {
              npc.TargetClosest(false);
              if (npc.timeLeft > 30)
                npc.timeLeft = 30;
              npc.noTileCollide = true;
              npc.noGravity = true;
              ++npc.velocity.Y;
              break;
            }
            Vector2 center2 = player.Center;
            for (int index = 0; index < 22; ++index)
            {
              center2.Y -= 16f;
              Tile tileSafely = Framing.GetTileSafely(center2);
              if (tileSafely.active() && !tileSafely.inActive() && Main.tileSolid[tileSafely.type] && !Main.tileSolidTop[tileSafely.type])
              {
                center2.Y += 66f;
                break;
              }
            }
            if (((Entity) npc).Distance(center2) > 50.0)
            {
              Movement(center2, 0.2f, fastY: true);
              NPC npc3 = npc;
              npc3.position += (center2 - npc.Center) / 30f;
            }
            if (--npc.ai[2] < 0.0)
            {
              npc.ai[2] = 75f;
              Main.PlaySound(SoundID.NPCKilled, npc.Center, 13);
              if (npc.ai[1] > 10.0 && Main.netMode != NetmodeID.MultiplayerClient)
              {
                for (int index = -1; index <= 1; ++index)
                  Projectile.NewProjectile(npc.Center + Vector2.UnitY * 60f, (npc.localAI[2] == 1f ? 12 : 8) * Utils.RotatedBy(npc.DirectionTo(player.Center), (double)MathHelper.ToRadians(8 * index), default(Vector2)), 258, npc.damage / 4, 0f, Main.myPlayer);
              }
            }
            if (++npc.ai[1] > 480.0)
            {
              ++npc.ai[0];
              npc.ai[1] = 0.0f;
              npc.netUpdate = true;
            }
            if (npc.localAI[2] != 0.0 || npc.life >= npc.lifeMax / 2)
              break;
            npc.ai[0] = -1f;
            npc.ai[1] = 0.0f;
            npc.ai[2] = 0.0f;
            npc.ai[3] = 0.0f;
            for (int index = 0; index < 200; ++index)
            {
              if (Main.npc[index].active && Main.npc[index].type == ModContent.NPCType<HellGuardHand>() && Main.npc[index].ai[2] == (double) npc.whoAmI)
              {
                Main.npc[index].ai[0] = -1f;
                Main.npc[index].ai[1] = 0.0f;
                Main.npc[index].localAI[0] = 0.0f;
                Main.npc[index].localAI[1] = 0.0f;
                Main.npc[index].netUpdate = true;
              }
            }
            break;
          default:
            npc.ai[0] = 0.0f;
            goto case 0.0f;
        }
      }
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
    {
      if (npc.Center.X < (double) targetPos.X)
      {
        npc.velocity.X += speedModifier;
        if (npc.velocity.X < 0.0)
          npc.velocity.X += speedModifier * 2f;
      }
      else
      {
        npc.velocity.X -= speedModifier;
        if (npc.velocity.X > 0.0)
          npc.velocity.X -= speedModifier * 2f;
      }
      if (npc.Center.Y < (double) targetPos.Y)
      {
        npc.velocity.Y += fastY ? speedModifier * 2f : speedModifier;
        if (npc.velocity.Y < 0.0)
          npc.velocity.Y += speedModifier * 2f;
      }
      else
      {
        npc.velocity.Y -= fastY ? speedModifier * 2f : speedModifier;
        if (npc.velocity.Y > 0.0)
          npc.velocity.Y -= speedModifier * 2f;
      }
      if (Math.Abs(npc.velocity.X) > (double) cap)
        npc.velocity.X = cap * Math.Sign(npc.velocity.X);
      if (Math.Abs(npc.velocity.Y) <= (double) cap)
        return;
      npc.velocity.Y = cap * Math.Sign(npc.velocity.Y);
    }

    public override void FindFrame(int frameHeight)
    {
      npc.frame.Y = 0;
      switch ((int) npc.ai[0])
      {
        case -1:
          if (npc.ai[1] <= 120.0)
            break;
          npc.frame.Y = frameHeight;
          break;
        case 1:
          if (npc.ai[2] >= 20.0)
            break;
          npc.frame.Y = frameHeight;
          break;
      }
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      target.AddBuff(24, 300);
      target.AddBuff(67, 300);
    }

    public override void BossLoot(ref string name, ref int potionType) => potionType = 188;

    public override void NPCLoot()
    {
      AlbedoWorld.DownedHellGuard = true;
      if (Main.netMode == NetmodeID.Server)
        NetMessage.SendData(MessageID.WorldData);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Rectangle frame = npc.frame;
			Vector2 origin = Utils.Size(frame) / 2f;
			Color color = lightColor;
			color = npc.GetAlpha(color);
			SpriteEffects effects = ((npc.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++)
			{
				Color color2 = color * 0.5f;
				color2 *= (NPCID.Sets.TrailCacheLength[npc.type] - i) / (float)NPCID.Sets.TrailCacheLength[npc.type];
				Vector2 vector = npc.oldPos[i];
				float rotation = npc.rotation;
				Main.spriteBatch.Draw(texture, vector + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, color2, rotation, origin, npc.scale, effects, 0f);
			}
			Texture2D texture2 = ModContent.GetTexture("Albedo/NPCs/Boss/HellGuard/HellGuard_Glow");
			if (npc.dontTakeDamage)
			{
				Vector2 vector2 = Vector2.UnitX * Main.rand.NextFloat(-180f, 180f);
				Main.spriteBatch.Draw(texture, npc.Center + vector2 - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor) * 0.5f, npc.rotation, origin, npc.scale, effects, 0f);
				Main.spriteBatch.Draw(texture2, npc.Center + vector2 - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor) * 0.5f, npc.rotation, origin, npc.scale, effects, 0f);
			}
			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor), npc.rotation, origin, npc.scale, effects, 0f);
			Main.spriteBatch.Draw(texture2, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, Color.White * npc.Opacity, npc.rotation, origin, npc.scale, effects, 0f);
			return false;
		}
  }
}
