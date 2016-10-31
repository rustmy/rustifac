namespace Oxide.Plugins {
	[Info("ColonialEraLoot", "Yi", 1.0)]
	[Description("Remove banned items from crates")]
	class ColonialEraLoot : RustPlugin {
		string[] banned = {
			"pistol.semiauto",
			"rifle.semiauto",
			"rifle.ak",
			"rifle.lr300",
			"rocket.launcher",
			"shotgun.pump",
			"smg.thompson",
			"smg.2",
			"explosives.item",
			"flamethrower",
			"weapon.mod.silencer",
			"weapon.mod.muzzlebrake",
			"weapon.mod.muzzleboost",
			"weapon.mod.lasersight",
			"weapon.mod.holosight",
			"weapon.mod.flashlight",
			"grenade.f1",
			"explosive.timed",
			"autoturret",
			"ammo.pistol.hv",
			"ammo.rifle.explosive",
			"ammo.pistol.fire",
			"ammo.rifle.incendiary",
			"ammo.rocket.basic",
			"ammo.rocket.fire",
			"ammo.rocket.hv",
			"crossbow",
			"door.double.hinged.toptier",
			"door.hinged.toptier",
			"roadsign.jacket",
			"roadsign.kilt",
			"smg.mp5"
		};
		void OnItemAddedToContainer(ItemContainer container, Item item)
		{
			if (banned.Contains(item.info.shortname)) {
				item.RemoveFromContainer();
				item.Remove(0f);
			}
		}
		void OnLootItem(BasePlayer player, Item item)
		{
			if (banned.Contains(item.info.shortname)) {
				item.RemoveFromContainer();
				item.Remove(0f);
			}
		}
	}
}
