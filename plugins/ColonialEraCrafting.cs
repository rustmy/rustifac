	using System;
	using System.Collections.Generic;
	using Oxide.Core;
	using Oxide.Core.Libraries;
	using Oxide.Core.Plugins;
	namespace Oxide.Plugins {
		[Info("ColonialEraCrafting", "Yi", 1.1)]
		[Description("Colonial Era for Rust Factions")]
		class ColonialEraCrafting : RustPlugin {
			#region Initialization
			List<object> defaultList = new List<object>();
			private bool configChanged = false;
			string[] factions_colonial = {
				"HEC",
				"Voyageurs",
				"NPT",
				"Olson",
				"Meridian",
				"UCNW",
				"colony",
				"DTY",
				"NWC",
				"PRH",
				"CDB",
				"LOST",
				"TG",
				"ATC",
				"Salt",
				"MERTH",
				"OldMan",
				"Union",
				"Tiki",
				"Ulfur",
				"Reefer",
				"BOOMERS",
				"TER",
				"lingam",
				"Legion",
				"GTC",
				"UNE"
			};
			string[] factions_native = {
				"Wathaurong",
				"Kiswani",
				"WARZ",
				"Warziri",
				"Hunters",
				"Zulu",
				"Ferox",
				"Konyak",
				"Dakota",
				"Korowa",
				"SKN",
				"Quetzalcoatl",
				"Lakota",
				"WAKAN",
				"Glass",
				"LCT",
				"Witigo",
				"KOS",
				"Lumby",
				"Yava",
				"Wendigo",
				"Koro",
				"Odawa",
				"CREEK",
				"SI",
				"Inuit",
				"Saburo",
				"Hunter",
				"Koyaanisqatsi",
				"yoni",
				"LC"
			};
			string[] banned = {
				"Semi-Automatic Pistol",		// pistol.semiauto
				"Semi-Automatic Rifle",			// rifle.semiauto
				"Assault Rifle",				// rifle.ak
				"LR-300 Assault Rifle",			// rifle.lr300
				"Rocket Launcher",				// rocket.launcher
				"Pump Shotgun",					// shotgun.pump
				"Thompson",						// smg.thompson
				"Custom SMG",					// smg.2
				"Explosives",					// explosives.item
				"Flame Thrower",				// flamethrower
				"4x Zoom Scope",				// flamethrower
				"Silencer",						// 	weapon.mod.silencer
				"Muzzle Brake",					// weapon.mod.muzzlebrake
				"Muzzle Boost",					// weapon.mod.muzzleboost
				"Weapon Lasersight",			// weapon.mod.lasersight
				"Holosight",					// 	weapon.mod.holosight
				"Weapon Flashlight",			// weapon.mod.flashlight
				"F1 Grenade",					// grenade.f1
				"Timed Explosive Charge",		// explosive.timed
				"Auto Turret",					// autoturret
				"HV Pistol Ammo",				// ammo.pistol.hv
				"Explosive 5.56 Rifle Ammo",	// ammo.rifle.explosive
				"Incendiary Pistol Bullet",		// ammo.pistol.fire
				"Incendiary 5.56 Rifle Ammo",	// ammo.rifle.incendiary
				"Rocket",						// ammo.rocket.basic
				"Incendiary Rocket",			// ammo.rocket.fire
				"High Velocity Rocket",			// ammo.rocket.hv
				"Crossbow",						// crossbow
				"Armored Double Door",			// door.double.hinged.toptier
				"Armored Door",					// door.hinged.toptier
				"Road Sign Jacket",				// roadsign.jacket
				"Road Sign Kilt",				// roadsign.kilt
				"Metal Barricade",
				"MP5A4"
			};
			string[] colonists = {
				"Bolt Action Rifle",
				"Double Barrel Shotgun",
				"Waterpipe Shotgun",
				"Eoka Pistol",
				"Revolver",
				"5.56 Rifle Ammo"
			};
			string[] natives = {
				"Bone Jacket",
				"Bone Armor Pants",
				"Bone Armor",
				"Bone Helmet",
				"Wolf Headdress",
				"Hunting Bow",
				"High Velocity Arrow",
			};
			public string yourClan;
			double xpBonus = 120;
			[PluginReference]
			Plugin Clans;
			object GetConfigValue(string category, object defaultValue)
			{
				var data = Config[category] as Dictionary<string, object>;
				if (data == null)
				{
					data = new Dictionary<string, object>();
					Config[category] = data;
					configChanged = true;
				}
				return data;
			}
			protected override void LoadDefaultConfig()
			{
				PrintWarning("Creating a new configuration file");
				Config.Clear();
				Config["factions_colonial"] = factions_colonial;
				Config["factions_native"] = factions_native;
				Config["items_banned"] = banned;
				Config["items_native"] = natives;
				Config["items_colonial"] = colonists;

				SaveConfig();
			}
			[ChatCommand("addnative")]
			void addNative(BasePlayer player, string command, string[] args) {
				Puts($"{player.net.connection.authLevel}");
				if (args.Length == 0) {} else {
					if (player.net.connection.authLevel == 2 || player.net.connection.authLevel == 1) {
						args[0] = args[0].Trim();
						if (factions_native.Contains(args[0])) {
							SendReply(player, "This faction is already listed as a native");
						} else {
							if (factions_colonial.Contains(args[0])) {
							SendReply(player, "This faction is already listed as a colonist");
							} else {
								int newLength = factions_native.Length + 1;
								string[] new_natives = new string[newLength];
								for(int i = 0; i < factions_native.Length; i++)
									new_natives[i] = factions_native[i];
								new_natives[newLength -1] = args[0];
								factions_native = new_natives;

								Puts($"native - {args[0]}");
								SendReply(player, "Done! This faction is now indigenous to Rustifac");
								Config["factions_native"] = new_natives;
								SaveConfig();
							}
						}
					} else {
						SendReply(player, "You do not have permission to use this command");
					}
				}
			}
			[ChatCommand("addcolonial")]
			void addColonial(BasePlayer player, string command, string[] args) {
				Puts($"{player.net.connection.authLevel}");
				if (args.Length == 0) {} else {
					if (player.net.connection.authLevel == 2 || player.net.connection.authLevel == 1) {
						args[0] = args[0].Trim();
						if (factions_native.Contains(args[0])) {
							SendReply(player, "This faction is already listed as a native");
						} else {
							if (factions_colonial.Contains(args[0])) {
							SendReply(player, "This faction is already listed as a colonist");
							} else {
								int newLength = factions_colonial.Length + 1;
									string[] new_colonies = new string[newLength];
								for(int i = 0; i < factions_colonial.Length; i++)
									new_colonies[i] = factions_colonial[i];
								new_colonies[newLength -1] = args[0];
								factions_colonial = new_colonies;

								Puts($"colonial - {args[0]}");
								SendReply(player, "Done! This faction is a colonial power");
								Config["factions_colonial"] = new_colonies;
								SaveConfig();
							}
						}
					} else {
						SendReply(player, "You do not have permission to use this command");
					}
				}
			}
			#endregion
			#region Crafting
			void OnItemCraft(ItemCraftTask task, BasePlayer player){
				var itemname = task.blueprint.targetItem.displayName.english;
				string clanTag = Clans.Call<string>("GetClanOf", player);
				if (banned.Contains(itemname)) {
					this.SendReply(player, "<color=#ce422b>This item cannot be crafted.</color>");
					task.cancelled = true;
					foreach (var amount in task.blueprint.ingredients)
						player.inventory.GiveItem(ItemManager.CreateByItemID(amount.itemid, (int)amount.amount * task.amount));
				} else {
					if (factions_colonial.Contains(clanTag)) {
						if (natives.Contains(itemname)) {
							this.SendReply(player, "<color=#ce422b>Colonists cannot craft this item.</color>");
							task.cancelled = true;
							foreach (var amount in task.blueprint.ingredients)
								player.inventory.GiveItem(ItemManager.CreateByItemID(amount.itemid, (int)amount.amount * task.amount));
						}
					} else if (factions_native.Contains(clanTag)) {
						if (colonists.Contains(itemname)) {
							this.SendReply(player, "<color=#ce422b>Natives cannot craft this item.</color>");
							task.cancelled = true;
							foreach (var amount in task.blueprint.ingredients)
								player.inventory.GiveItem(ItemManager.CreateByItemID(amount.itemid, (int)amount.amount * task.amount));
						}
					} else {
						if (colonists.Contains(itemname) || natives.Contains(itemname)) {
						this.SendReply(player, "<color=#ce422b>You cannot craft this item.</color>");
						Puts($"Unknown player {player} is blocked from crafting {itemname}");
						task.cancelled = true;
						foreach (var amount in task.blueprint.ingredients)
							player.inventory.GiveItem(ItemManager.CreateByItemID(amount.itemid, (int)amount.amount * task.amount));
						}
					}
				}
			}
			#endregion
		}
	}
