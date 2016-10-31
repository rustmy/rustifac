namespace Oxide.Plugins {
	[Info("ColonialEraBuilding", "Yi", 1.0)]
	[Description("Colonial Era for Rust Factions")]
	class ColonialEraBuilding : RustPlugin {
		private object OnStructureUpgrade(BuildingBlock block, BasePlayer player, BuildingGrade.Enum grade) {
			if (block.name.Contains("foundation.prefab")) {
				if (grade != BuildingGrade.Enum.Wood && grade != BuildingGrade.Enum.Stone) {
					this.SendReply(player, "<color=#ce422b>You can not upgrade past stone.</color>");
					return true;
				}
			} else {
				if (grade != BuildingGrade.Enum.Wood && grade != BuildingGrade.Enum.Stone) {
					this.SendReply(player, "<color=#ce422b>You can not upgrade past stone.</color>");
					return true;
				}
			}
			return null;
		}
	}
}
