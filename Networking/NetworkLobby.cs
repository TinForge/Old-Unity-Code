using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tin.Resources;
using Tin.Networking;

namespace Scene.Lobby
{
	///Strictly designed for lobby. Try not to incorporate game mechanics here.
	/// Perhaps destroy once in room
	public class NetworkLobby : Photon.PunBehaviour
	{
		public static string errorLog;

		public ClientState debug;

		public static int invokeConnect;

		#region Lobby

		public static void ConnectToLobby ()
		{
			Debug.Log ("Connecting");
			PhotonNetwork.ConnectUsingSettings (Reference.GameVersion);
			PhotonNetwork.automaticallySyncScene = false;
		}

		public override void OnJoinedLobby ()
		{
			Debug.Log ("Connected");
		}

		public override void OnConnectionFail (DisconnectCause cause)
		{
			errorLog = cause.ToString ();
		}

		#endregion

		void Update ()
		{
			debug = PhotonNetwork.connectionStateDetailed;
		}

		#region Transition

		public static void RandomGame ()
		{
			PhotonNetwork.JoinRandomRoom ();
		}

		public static void CreateRoom (string name)
		{
			if (name.Trim () == "")
				Util.ErrorHandler.instance.ToggleMessageBox (true, "Room needs a name");
			else {
				RoomOptions roomOptions = defaultRoomOptions ();
				PhotonNetwork.CreateRoom (name, roomOptions, null);
			}
		}

		public static RoomInfo[] GetRooms ()
		{
			return PhotonNetwork.GetRoomList ();
		}

		public static void JoinRoom (string id)
		{
			PhotonNetwork.JoinRoom (id);
		}

		public override void OnLeftLobby ()
		{
			if (PhotonNetwork.connected) {//In case user disconnects
				///Customization will load customProperties
				StartCoroutine (LoadLevel ());
			}
		}

		public static IEnumerator LoadLevel ()
		{
			Debug.Log ("Loading Level - Lobby");
			PhotonNetwork.isMessageQueueRunning = false;
			yield return new WaitForSeconds (2);
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("Game");
			while (!asyncLoad.isDone)
				yield return null;
			PhotonNetwork.isMessageQueueRunning = true;
			Debug.Log ("Loaded Level - Lobby");
		}

		public static bool isConnecting ()
		{
			if (PhotonNetwork.connectionStateDetailed == ClientState.ConnectingToGameserver)
				return true;
			else if (PhotonNetwork.connectionStateDetailed == ClientState.ConnectedToGameserver)
				return true;
			else if (PhotonNetwork.connectionStateDetailed == ClientState.Joining)
				return true;
			else if (PhotonNetwork.connectionStateDetailed == ClientState.Joined)
				return true;
			else
				return false;
		}

		private static RoomOptions defaultRoomOptions ()
		{
			RoomOptions roomOptions = new RoomOptions ();
			roomOptions.IsOpen = true;
			roomOptions.IsVisible = true;
			roomOptions.PublishUserId = true;
			roomOptions.MaxPlayers = Reference.MaxPlayersPerRoom;
			roomOptions.CustomRoomPropertiesForLobby = new string[]{ GameProperties.Key_Map, GameProperties.Key_GameState };
			return roomOptions;
		}

		public override void OnJoinedRoom ()
		{
			Debug.Log ("OnJoinedRoom - Lobby");
			//Destroy (this);
		}




		#endregion

	}
}
