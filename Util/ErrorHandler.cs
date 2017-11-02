using UnityEngine;
using UnityEngine.UI;

namespace Util
{
	///Universal Message Box for handling any errors.
	///Attach to canvas and don't destroy on load
	///Call from other classes or override errors
	public class ErrorHandler : Photon.PunBehaviour
	{
		public static ErrorHandler instance;

		[SerializeField] private Canvas canvas;
		[SerializeField] private Text message;

		void Awake ()
		{
			if (instance != this && instance != null)
				Destroy (gameObject);
			DontDestroyOnLoad (gameObject);
			instance = this;
		}

		public void ToggleMessageBox (bool toggle)
		{
			canvas.enabled = toggle;
		}

		public void ToggleMessageBox (bool toggle, string data)
		{
			message.text = data;
			canvas.enabled = toggle;
		}

		public override void OnConnectionFail (DisconnectCause cause)
		{
			message.text = cause.ToString ();
			ToggleMessageBox (true);
		}

		public override void OnPhotonCreateRoomFailed (object[] codeAndMsg)
		{
			message.text = codeAndMsg [1].ToString ();
			ToggleMessageBox (true);
		}

		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			message.text = codeAndMsg [1].ToString ();
			ToggleMessageBox (true);
		}

		public override void OnPhotonJoinRoomFailed (object[] codeAndMsg)
		{
			message.text = codeAndMsg [1].ToString ();
			ToggleMessageBox (true);
		}

		public override void OnFailedToConnectToPhoton (DisconnectCause cause)
		{
			message.text = cause.ToString ();
			ToggleMessageBox (true);
		}

		public override void OnPhotonMaxCccuReached ()
		{
			message.text = "Max Server Capacity Reached! Contact me @ TinForge.com";
			ToggleMessageBox (true);
		}

		public override void OnDisconnectedFromPhoton ()
		{
			message.text = "Disconnected from server";
			ToggleMessageBox (true);
		}
	}
}
