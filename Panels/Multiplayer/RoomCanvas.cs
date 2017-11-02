using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;
using Tin.Networking;

namespace Scene.Lobby
{
	public class RoomCanvas : MonoBehaviour
	{
		public RoomInfo room;
		public Text roomName;
		public Text mapName;
		public Text modeName;

		public void Initialize (RoomInfo r)
		{
			room = r;

			roomName.text = room.Name;
			mapName.text = (string)room.CustomProperties [GameProperties.Key_Map];
			modeName.text = (string)room.CustomProperties [GameProperties.Key_GameState];
		}

		public void JoinRoom ()
		{
			NetworkLobby.JoinRoom (room.Name);
		}

		public void Destroy ()
		{
			Destroy (gameObject);
		}

	}
}
