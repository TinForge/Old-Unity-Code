using UnityEngine;
using HashTable = ExitGames.Client.Photon.Hashtable;

namespace Tin.Networking
{
	public class GameProperties : Photon.PunBehaviour
	{
		protected Room room;
		protected HashTable properties;

		public const string Key_Map = "map";
		public const string Key_GameState = "gamestate";
		public const string Key_PlayersAlive = "playersalive";

		public string Map {
			get {
				return (string)room.CustomProperties [Key_Map];
			}
			set {
				properties = room.CustomProperties;
				if (properties.ContainsKey (Key_Map))
					properties [Key_Map] = (string)value;
				else
					properties.Add (Key_Map, (string)value);
			
				room.SetCustomProperties (properties);
			}
		}

		public string GameState {
			get {
				return (string)room.CustomProperties [Key_GameState];
			}
			set {
				properties = room.CustomProperties;
				if (properties.ContainsKey (Key_GameState))
					properties [Key_GameState] = (string)value;
				else
					properties.Add (Key_GameState, (string)value);
			
				room.SetCustomProperties (properties);

			}
		}

		public int PlayersAlive {
			get {
				return (int)room.CustomProperties [Key_PlayersAlive];
			}
			set {
				properties = room.CustomProperties;
				if (properties.ContainsKey (Key_PlayersAlive))
					properties [Key_PlayersAlive] = (int)value;
				room.SetCustomProperties (properties);
			}
		}
	}
}

