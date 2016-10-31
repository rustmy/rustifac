using Oxide.Core.Plugins;
using Rust.Xp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
namespace Oxide.Plugins {
	[Info("OreAndOil", "Yi", 1.2)]
	[Description("Of Ore and Oil")]
	class OreAndOil : RustPlugin {
		public string bannedItem;
		public string thisItem;
		string[] banned = {
			"Mining Quarry",
			"Pump Jack"
		};
		string[] bannedShortnames = {
			"mining.quarry",
			"mining.pumpjack"
		};
		void OnItemCraft(ItemCraftTask task, BasePlayer player) {
			var itemname = task.blueprint.targetItem.displayName.english;
			if (banned.Contains(itemname)) { /* cancel their craft */
				this.SendReply(player, "<color=#ce422b>This item cannot be crafted.</color>");
				task.cancelled = true;
				foreach (var amount in task.blueprint.ingredients)
					player.inventory.GiveItem(ItemManager.CreateByItemID(amount.itemid, (int)amount.amount * task.amount));
			}
		}
		private void OnQuarryGather(MiningQuarry quarry, Item item) {
			/// Quarries and pumpjacks gather at 5x the usual rate
			item.amount = (int)(item.amount * 5);
		}
		private void OnSurveyGather(SurveyCharge surveyCharge, Item item) {
			/// Survey charges reflect quarry rate change
			item.amount = (int)(item.amount * 5);
		}
		void OnDispenserGather(ResourceDispenser dispenser, BaseEntity entity, Item item) {
			thisItem = item.info.name;
			/// Mining ore nodes gives half the usual amount
			if (thisItem.Contains("ore")) {
				item.amount = (int)(item.amount * 0.25);
			}
			/// Mining ore nodes gives half the usual amount
			if (thisItem.Contains("stone")) {
				item.amount = (int)(item.amount * 0.15);
			}
			/// Gathering animal fat returns half the usual amount
			if (thisItem.Contains("fat.animal")) {
				item.amount = (int)(item.amount * 0.5);
			}
		}
		/// XP bonus of level 34 on first connection
		void OnPlayerInit(BasePlayer player) {
			var unspent = player.xp.UnspentXp;
			var spent = player.xp.SpentXp;
			float playerxp = (float)34;
			float toobig = (float)100;
			float thisxp = player.xp.CurrentLevel;
			if (thisxp < playerxp) {
				player.xp.Reset();
				player.xp.Add(Rust.Xp.Definitions.Cheat, (float)1090);
			}
			if (thisxp > toobig) {
				player.xp.Reset();
				player.xp.Add(Rust.Xp.Definitions.Cheat, (float)1090);
				Puts($"Fixing {player}'s XP. was -2,147,483,648");
			}
		}
		/// Block earning XP other than the initial bonus
		object OnXpEarn(ulong id, float amount, string source) {
			string cheat = "Cheat";
			if (source == cheat) {
				return amount;
			} else {
				return (float)(amount * 0);
			}
		}
		/// Despawn mining quarries and pumpjacks that get spawned
		void OnItemAddedToContainer(ItemContainer container, Item item)
		{
			if (bannedShortnames.Contains(item.info.shortname)) {
				item.RemoveFromContainer();
				item.Remove(0f);
			}
		}
		/// Remove mining quarries and pumpjacks that slipped through
		void OnLootItem(BasePlayer player, Item item)
		{
			if (bannedShortnames.Contains(item.info.shortname)) {
				item.RemoveFromContainer();
				item.Remove(0f);
			}
		}
	}
}
