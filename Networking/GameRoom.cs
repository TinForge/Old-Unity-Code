using HashTable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;

namespace Tin.Networking
{
	public class GameRoom : GameProperties
	{
		public static GameRoom instance;

		public override void OnCreatedRoom ()
		{
			room = PhotonNetwork.room;
			properties = new HashTable ();

			Map = "Game";
			GameState = "Active";
		}

		public override void OnJoinedRoom ()
		{
			if (instance != this && instance != null)
				Debug.LogError ("RoomProperties instance error");
			instance = this;

			room = PhotonNetwork.room;
			properties = new HashTable ();
		}

		public void DebugCustomProperties ()
		{
			Debug.Log (Key_Map + " " + Map);
			Debug.Log (Key_GameState + " " + GameState);
			Debug.Log (Key_PlayersAlive + " " + PlayersAlive);
		}






		public override void OnPhotonCustomRoomPropertiesChanged (ExitGames.Client.Photon.Hashtable propertiesThatChanged)
		{
			base.OnPhotonCustomRoomPropertiesChanged (propertiesThatChanged);
		}
	}
}
