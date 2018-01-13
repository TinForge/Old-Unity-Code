using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;

namespace Scene.Lobby
{
	public class LobbySceneManager : MonoBehaviour
	{
		public bool debug;
		[SerializeField] private UIElementBase offlineCanvas;
		[SerializeField] private UIElementBase onlineCanvas;
		[SerializeField] private AudioSource audioPlayer;

		void Start ()
		{
			if (debug) {
				NetworkLobby.ConnectToLobby ();
			} else {
				offlineCanvas.Disable ();
				onlineCanvas.Disable ();
			}

			ResetMouse ();

			Audio.instance.SetVolume ();
			SettingsManager.instance.LoadUserSettings ();
		}

		void Update ()
		{
			MonitorConnectionState ();
		}

		private void MonitorConnectionState ()
		{
			if (PhotonNetwork.connected && offlineCanvas.active && !offlineCanvas.busy)
				offlineCanvas.Disable ();
			if ((!PhotonNetwork.connected && !debug) && onlineCanvas.active && !onlineCanvas.busy)
				onlineCanvas.Disable ();
		
			if (!offlineCanvas.active && !onlineCanvas.active) {
				if (!PhotonNetwork.connected)
					offlineCanvas.Enable ();
				else
					onlineCanvas.Enable ();
			}
		}

		private void ResetMouse ()
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		public void PlaySound (int num)
		{
			switch (num) {
			case 1://Click
				audioPlayer.PlayOneShot (Library.ClickUI);
				return;
			case 2://Success
				audioPlayer.PlayOneShot (Library.SuccessUI);
				return;
			case 3://Failure
				audioPlayer.PlayOneShot (Library.FailureUI);
				return;
			}
		}
	}
}
