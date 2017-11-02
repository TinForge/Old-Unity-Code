using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.Lobby
{
	public class MultiplayerLobby : Photon.PunBehaviour
	{
		public string roomName;
		public Text gamesFound;
		public Transform listPanel;
		public GameObject roomObject;

		void Start ()
		{
			InvokeRepeating ("ListGame", 0, 4);
		}

		public void SetRoomName (string value)
		{
			roomName = value;
		}

		public void RandomGame ()
		{
			PhotonNetwork.JoinRandomRoom ();
		}

		public void CreateGame ()
		{
			NetworkLobby.CreateRoom (roomName);
		}

		public void ListGame ()
		{
			if (PhotonNetwork.insideLobby) {
				foreach (RoomCanvas rc in listPanel.GetComponentsInChildren <RoomCanvas>())
					rc.Destroy ();
				
				RoomInfo[] rooms = NetworkLobby.GetRooms ();
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
			NetworkLobby.JoinRoom (id);
		}
	}
}
