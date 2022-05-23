using System;
using System.Collections.Generic;
using Albedo.Base;
using Albedo.Buffs.Item;
using Albedo.Items.Materials;
using Albedo.Projectiles.Weapons.Ranged.ZenithGun;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Weapons.Ranged
{
	public class ZenithGun : AlbedoItem
	{
		private readonly List<int> _id = new List<int>();
		private readonly Random _random = new Random();

		private readonly List<int> _typeList = new List<int>();
		protected override int Rarity => 12;

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.damage = 100;
			item.ranged = true;
			item.width = 100;
			item.height = 40;
			item.useTime = 4;
			item.useAnimation = 4;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4f;
			item.value = Item.buyPrice(1);
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.buffType = ModContent.BuffType<ZenithGunMinionBuff>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
			ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 10);
			var vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5f));
			speedX = vector.X;
			speedY = vector.Y;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SdmgMinion>()] == 0) {
				_id.Clear();
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<SdmgMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<ChainGunMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<ClockworkAssaultRifleMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<MegasharkMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<VenusMagnumMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<OnyxBlasterMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<PhoenixBlasterMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<XenopopperMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(_random.Next(2) == 0
					? Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
						ModContent.ProjectileType<MusketMinion>(), damage, knockBack, Main.myPlayer)
					: Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
						ModContent.ProjectileType<TheUndertakerMinion>(), damage, knockBack, Main.myPlayer));
				_id.Add(Projectile.NewProjectile(position.X, position.Y, 0f, 0f,
					ModContent.ProjectileType<VortexBeaterMinion>(), damage, knockBack, Main.myPlayer));
				_typeList.Clear();
				_typeList.Add(89);
				_typeList.Add(104);
				_typeList.Add(207);
				_typeList.Add(242);
				_typeList.Add(279);
				_typeList.Add(283);
				_typeList.Add(284);
				_typeList.Add(285);
				_typeList.Add(14);
				_typeList.Add(287);
			}

			if (type == 14) {
				for (int num = _typeList.Count - 1; num > 1; num--) {
					int index = _random.Next(num + 1);
					(_typeList[index], _typeList[num]) = (_typeList[num], _typeList[index]);
				}

				for (int i = 0; i < _id.Count; i++) {
					var position2 = Main.projectile[_id[i]].position;
					var vector2 = Main.MouseWorld - position2;
					vector = new Vector2(vector2.X, vector2.Y).RotatedByRandom(MathHelper.ToRadians(5f));
					vector2.X = vector.X;
					vector2.Y = vector.Y;
					Projectile.NewProjectile(position2.X + 14f, position2.Y, vector2.X, vector2.Y, _typeList[i], damage,
						knockBack, Main.myPlayer);
				}
			}
			else {
				foreach (int t in _id) {
					var position3 = Main.projectile[t].position;
					var vector3 = Main.MouseWorld - position3;
					vector = new Vector2(vector3.X, vector3.Y).RotatedByRandom(MathHelper.ToRadians(5f));
					vector3.X = vector.X;
					vector3.Y = vector.Y;
					Projectile.NewProjectile(position3.X + 14f, position3.Y, vector3.X, vector3.Y, type, damage,
						knockBack, Main.myPlayer);
				}
			}

			return true;
		}

		public override bool ConsumeAmmo(Player player) => Main.rand.NextFloat() >= 0.77f;

		public override Vector2? HoldoutOffset() => new Vector2(-14f, 0f);

		public override void AddRecipes()
		{
			var val = new ModRecipe(mod);
			val.AddTile(ModContent.TileType<WeaponStation3Tile>());
			val.SetResult(this);
			val.AddIngredient(ItemID.SDMG);
			val.AddIngredient(ItemID.VortexBeater);
			val.AddIngredient(ItemID.Xenopopper);
			val.AddIngredient(ItemID.ChainGun);
			val.AddIngredient(ItemID.VenusMagnum);
			val.AddIngredient(ItemID.Megashark);
			val.AddIngredient(ItemID.OnyxBlaster);
			val.AddIngredient(ItemID.ClockworkAssaultRifle);
			val.AddIngredient(ItemID.PhoenixBlaster);
			val.AddIngredient(ItemID.TheUndertaker);
			val.AddIngredient(ModContent.ItemType<GunGodSoul>(), 15);
			val.AddIngredient(ModContent.ItemType<HellGuardSoul>(), 15);
			val.AddIngredient(ModContent.ItemType<GunDemonSoul>(), 15);
			val.AddRecipe();
		}
	}
}