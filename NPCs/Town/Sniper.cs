using Albedo.Items.Ammos.Bullets;
using Albedo.Items.Materials;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.NPCs.Town
{
    [AutoloadHead]
    public class Sniper : ModNPC
    {
        public override bool Autoload(ref string name) 
        {
            name = "Sniper";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 500;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 45;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = -2;
        }
        
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 50;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = 17;
        }
        
        public override void AI()
        {
            npc.breath = 200;
        }
        
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return !toKingStatue;
        }
        
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return NPC.downedBoss1;
        }
        
        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(7))
            {
                case 0:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name1");
                case 1:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name2");
                case 2:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name3");
                case 3:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name4");
                case 4:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name5");
                case 5:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name6");
                case 6:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name7");
                default:
                    return Language.GetTextValue("Mods.Albedo.Sniper.name7");
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            switch (Main.hardMode)
            {
                case false:
                    damage = 20;
                    break;
                case true when !NPC.downedMoonlord:
                    damage = 100;
                    break;
            }

            if (NPC.downedMoonlord)
            {
                damage = 1000;
            }
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 3;
            randExtraCooldown = 3;
        }
        
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 6f;
            randomOffset = 0f;
        }
        
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            attackDelay = 10;
            switch (Main.hardMode)
            {
                case false:
                    projType = ModContent.ProjectileType<GelBulletProjectile>();
                    break;
                case true:
                    projType = ModContent.ProjectileType<PinkGelBulletProjectile>();
                    break;
            }
        }
        
        public override string GetChat()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return Language.GetTextValue("Mods.Albedo.Sniper.dialog1");
                case 1:
                    return Language.GetTextValue("Mods.Albedo.Sniper.dialog2");
                case 2:
                    return Language.GetTextValue("Mods.Albedo.Sniper.dialog3");
                case 3:
                    return Language.GetTextValue("Mods.Albedo.Sniper.dialog4");
                default:
                    return Language.GetTextValue("Mods.Albedo.Sniper.dialog4");
            }
        }
        
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }
        
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<DirtBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<StoneBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<SnowBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<IceBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GelBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PinkGelBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GemBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:7);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<CopperBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<IronBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<LeadBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<PreciousBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<TungstenBullet>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:5);
            nextSlot++;

            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<HellBullet>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:40);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CobaltBullet>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:60);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MythrilBullet>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper:80);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AdamantiteBullet>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver:1);
                nextSlot++;
            }

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Gunpowder>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver:20);
            nextSlot++;
        }
    }
}
