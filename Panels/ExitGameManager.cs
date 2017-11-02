using UnityEngine;

namespace Scene.Lobby
{
	public class ExitGameManager : Photon.MonoBehaviour
	{
		[SerializeField] private CanvasGroup exitBox;

		public void ClickExit ()
		{
			exitBox.alpha = 1;
			exitBox.blocksRaycasts = true;
			exitBox.interactable = true;
		}

		public void ClickYes ()
		{
			if (PhotonNetwork.connected)
				PhotonNetwork.Disconnect ();

			exitBox.alpha = 0;
			exitBox.blocksRaycasts = false;
			exitBox.interactable = false;

			Invoke ("Exit", 1);
		}

		public void ClickNo ()
		{
			exitBox.alpha = 0;
			exitBox.blocksRaycasts = false;
			exitBox.interactable = false;
		}


		public void Exit ()
		{
			Application.Quit ();
		}
	}
}
