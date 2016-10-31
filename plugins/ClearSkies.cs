using UnityEngine;
using System.Reflection;
namespace Oxide.Plugins
{
	[Info("ClearSkies", "Yi", "1.2")]
	class ClearSkies : RustPlugin
	{
		public class StopDecayReset : RustPlugin {
	        private void Loaded() {
	            ConsoleSystem.Run.Server.Normal($"heli.lifetimeminutes 0");
	        }
	    }
		void OnEntitySpawned(BaseNetworkable entity)
		{
			string plane = "cargo_plane";
			string nade = "grenade.smoke.deployed";
			string item = entity.name;

			if (item.Contains(plane) || item.Contains(nade)) {
				Puts($"{entity.name}");
				entity.Kill();
			}
		}
	}
}
