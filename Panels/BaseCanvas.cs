using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.Lobby
{
	public class BaseCanvas : MonoBehaviour
	{
		[SerializeField] private Text version;
		[SerializeField] private Text ping;
		[SerializeField] private CanvasGroup loading;
		[SerializeField] private Image spinner;
		[SerializeField] private Image mask;
		[Space]
		[SerializeField] private AudioSource music;
		[Space]
		[SerializeField] private float fadeInTime;
		[SerializeField] private float fadeOutTime;
		[SerializeField] private float iconTurnSpeed;
		private bool isLoading;
		private bool busy;

		void Awake ()
		{
			fadeInTime = 1 / fadeInTime;
			fadeOutTime = 1 / fadeOutTime;
			version.text = "Version\n" + Reference.GameVersion;
		}

		void Start ()
		{
			StopAllCoroutines ();
			StartCoroutine (MaskOff ());
		}

		void Update ()
		{
			if (PhotonNetwork.connected)
				ping.text = "Ping\n" + PhotonNetwork.GetPing ();
			else
				ping.text = "";

			spinner.rectTransform.Rotate (Vector3.forward * iconTurnSpeed * Time.deltaTime);

			if (NetworkLobby.isConnecting () && !isLoading) {//for loading
				StartCoroutine (EnableLoadThread ());
			} else if (!NetworkLobby.isConnecting () && isLoading) {
				StartCoroutine (DisableLoadThread ());
			}

			if (NetworkLobby.isConnecting () & !busy) {//for mask
				StartCoroutine (MaskOn ());
			}
		}

		private IEnumerator EnableLoadThread ()
		{
			float t = 0;
			isLoading = true;

			Util.UI.SetAlpha (loading, 0);

			while (true) {
				t += Time.deltaTime * fadeInTime;
				if (t < 1) {
					float a = Mathf.Lerp (0, 1, Util.LerpType.SmoothStep (t));
					Util.UI.SetAlpha (loading, a);
					yield return null;
				} else {
					Util.UI.SetAlpha (loading, 1);
					break;
				}
			}
		}

		private IEnumerator DisableLoadThread ()
		{
			float t = 0;
			isLoading = false;

			Util.UI.SetAlpha (loading, 1);

			while (true) {
				t += Time.deltaTime * fadeOutTime;
				if (t < 1) {
					float a = Mathf.Lerp (1, 0, Util.LerpType.SmoothStep (t));
					Util.UI.SetAlpha (loading, a);
					yield return null;
				} else {
					Util.UI.SetAlpha (loading, 0);
					break;
				}
			}
		}

		private IEnumerator MaskOff ()
		{
			float t = 0;

			while (true) {
				t += Time.deltaTime * fadeInTime;
				if (t < 1) {
					float a = Mathf.Lerp (1, 0, 1f - Util.LerpType.Coserp (t));
					Util.UI.SetAlpha (mask, a);
					Util.Sound.SetVolume (music, 1 - a);
					yield return null;
				} else {
					Util.UI.SetAlpha (mask, 0);
					break;
				}
			}
		}

		private IEnumerator MaskOn ()
		{
			busy = true;
			float t = 0;

			while (true) {
				t += Time.deltaTime * fadeOutTime;
				if (t < 1) {
					float a = Mathf.Lerp (0, 1, 1f - Util.LerpType.Sinerp (t));
					Util.UI.SetAlpha (mask, a);
					Util.Sound.SetVolume (music, 1 - a);
					yield return null;
				} else {
					Util.UI.SetAlpha (mask, 1);
					break;
				}
			}
		}
	}
}
