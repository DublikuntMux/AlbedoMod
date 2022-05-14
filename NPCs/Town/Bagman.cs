using Albedo.Items.Ammos.Pouches.Mod;
using Albedo.Items.Ammos.Pouches.Vanila;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.NPCs.Town
{
    [AutoloadHead]
    public class Bagman : ModNPC
    {
        private bool _otherShop;

        public override bool Autoload(ref string name)
        {
            name = "Bagman";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 500;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 45;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = -4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 100;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Mechanic;
        }

        public override void AI()
        {
            npc.breath = 200;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return NPC.downedBoss3;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(7))
            {
                case 0:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name1");
                case 1:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name2");
                case 2:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name3");
                case 3:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name4");
                case 4:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name5");
                case 5:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name6");
                case 6:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name7");
                default:
                    return Language.GetTextValue("Mods.Albedo.Bagman.name7");
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 100;
            knockback = 8f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 3;
            randExtraCooldown = 3;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection,
            ref float randomOffset)
        {
            multiplier = 6f;
            randomOffset = 0f;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            attackDelay = 5;
            projType = ModContent.ProjectileType<GemBulletProjectile>();
        }

        public override string GetChat()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return Language.GetTextValue("Mods.Albedo.Bagman.dialog1");
                case 1:
                    return Language.GetTextValue("Mods.Albedo.Bagman.dialog2");
                case 2:
                    return Language.GetTextValue("Mods.Albedo.Bagman.dialog3");
                case 3:
                    return Language.GetTextValue("Mods.Albedo.Bagman.dialog4");
                default:
                    return Language.GetTextValue("Mods.Albedo.Bagman.dialog4");
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.Albedo.NPC.PreHardmod") + " " +
                     Language.GetTextValue("LegacyInterface.28");
            button2 = Language.GetTextValue("Mods.Albedo.NPC.Hardmod") + " " +
                      Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            shop = true;
            _otherShop = !firstButton;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (_otherShop)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PinkGelPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<HellPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ChlorophytePouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<CrystalPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<CursedPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<IchorPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<NanoPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PartyPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<VelocityPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<VenomPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<CobaltPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<MythrilPouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<AdamantitePouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                }

                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<LuminitePouch>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                    nextSlot++;
                }
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CopperPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DirtPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GelPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GemPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<IcePouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<IronPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LeadPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PreciousPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SnowPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<StonePouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<TungstenPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExplosivePouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoldenPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MeteorPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SilverPouch>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(1);
                nextSlot++;
            }
        }
    }
}