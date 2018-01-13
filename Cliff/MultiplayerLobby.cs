using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;
using Tin.Networking;

namespace Scene.Lobby.UI
{
	public class MultiplayerLobby : Photon.PunBehaviour
	{
		public string roomName;
		public Text gamesFound;
		public Transform listPanel;
		public GameObject roomObject;

		public void SetRoomName (string value)
		{
			roomName = value;
		}

		public void RandomGame ()
		{
			AudioPlayer.Play (Library.ClickUI);
			PhotonNetwork.JoinRandomRoom ();
		}

		public void CreateGame ()
		{
			AudioPlayer.Play (Library.ClickUI);
			LobbyNetworking.CreateRoom (roomName);
		}

		public void ListGame ()
		{
			AudioPlayer.Play (Library.ClickUI);
			DisplayRooms ();
		}

		public override void OnReceivedRoomListUpdate ()
		{
			DisplayRooms ();
		}

		private void DisplayRooms ()
		{
			if (PhotonNetwork.insideLobby) {
				foreach (RoomCanvas rc in listPanel.GetComponentsInChildren <RoomCanvas>())
					rc.Destroy ();

				RoomInfo[] rooms = LobbyNetworking.GetRooms ();
				gamesFound.text = "Games Found: " + rooms.Length;
				foreach (RoomInfo ri in rooms) {
					GameObject roomGO = (GameObject)Instantiate (roomObject, listPanel);
					roomGO.transform.localScale = Vector3.one;
					roomGO.GetComponent<RoomCanvas> ().Initialize (ri);
				}
			}
		}

		public void JoinGame (string id)
		{
			AudioPlayer.Play (Library.ClickUI);
			LobbyNetworking.JoinRoom (id);
		}
	}
}
