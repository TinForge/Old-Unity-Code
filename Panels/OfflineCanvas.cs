using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;

namespace Scene.Lobby
{
	public class OfflineCanvas : UIElementBase
	{
		[SerializeField] private Text title;
		[SerializeField] private Text message;
		[SerializeField] private CanvasGroup connect;
		[SerializeField] private CanvasGroup loading;
		[SerializeField] private Image spinner;
		[Space]
		[SerializeField] private AudioSource audioPlayer;
		[Space]
		[SerializeField] private float fadeInTime;
		[SerializeField] private float fadeOutTime;
		[SerializeField] private float iconTurnSpeed;

		void Awake ()
		{
			fadeInTime = 1 / fadeInTime;
			fadeInBase = fadeInTime;
			fadeOutTime = 1 / fadeOutTime;
			fadeOutBase = fadeOutTime;
			NetworkLobby.errorLog = "Welcome!";
		}

		void Update ()
		{
			title.rectTransform.Translate ((Mathf.PingPong (Time.timeSinceLevelLoad, 0.5f) - 0.25f) * Vector3.up);
			spinner.rectTransform.Rotate (Vector3.forward * iconTurnSpeed * Time.deltaTime);
		}


		public override void Enable ()
		{
			message.text = NetworkLobby.errorLog;
			audioPlayer.PlayOneShot (Library.FailureUI);
			base.Enable ();
			Util.UI.SetAlpha (loading, 0);
		}

		protected override void EnableStep (float t)
		{
			Util.UI.SetAlpha (title, t);
			Util.UI.SetAlpha (message, t);
			Util.UI.SetAlpha (connect, t);
		}

		public override void Disable ()
		{
			if (!active)
				return;
			base.Disable ();
			Util.UI.SetAlpha (message, 0);
			Util.UI.SetAlpha (connect, 0);
		}

		protected override void DisableStep (float t)
		{
			Util.UI.SetAlpha (title, t);
			Util.UI.SetAlpha (loading, t);
		}

		public void Connect ()
		{
			StopAllCoroutines ();
			StartCoroutine (ConnectThread ());

			TryToConnect ();
		}

		private IEnumerator ConnectThread ()
		{
			float t = 0;
			busy = true;

			Util.UI.SetAlpha (title, 1);
			Util.UI.SetAlpha (message, 1);
			Util.UI.SetAlpha (connect, 1);
			Util.UI.SetAlpha (loading, 0);

			audioPlayer.PlayOneShot (Library.ClickUI);

			while (true) {
				t += Time.deltaTime * fadeOutTime;
				if (t < 1) {
					float a = Mathf.Lerp (1, 0, Util.LerpType.SmoothStep (t));
					Util.UI.SetAlpha (message, a);
					Util.UI.SetAlpha (connect, a);
					yield return null;
				} else {
					Util.UI.SetAlpha (message, 0);
					Util.UI.SetAlpha (connect, 0);
					break;
				}
			}
			t = 0;
			while (true) {
				t += Time.deltaTime * fadeInTime;
				if (t < 1) {
					float a = Mathf.Lerp (0, 1, Util.LerpType.SmoothStep (t));
					Util.UI.SetAlpha (loading, a);
					yield return null;
				} else {
					Util.UI.SetAlpha (loading, 1);
					TryToConnect ();
					break;
				}
			}
			busy = false;
		}

		private void TryToConnect ()
		{
			NetworkLobby.ConnectToLobby ();
			message.text = " ";
		}

	}
}
