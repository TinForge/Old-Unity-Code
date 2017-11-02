using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.Splash
{

	public class SplashSceneManager : MonoBehaviour
	{
		[SerializeField] private bool debug;

		[SerializeField] private float fadeInDelay;
		[SerializeField] private float animationDelay;
		[SerializeField] private float fadeOutDelay;
		[Space]
		[SerializeField] private float fadeInTime;
		[SerializeField] private float animationTime;
		[SerializeField] private float fadeOutTime;
		[Space]
		[SerializeField] private Image logotype;
		[SerializeField] private Image icon;
		[SerializeField] private Image mask;
		[SerializeField] private AudioSource sound;

		void OnEnable ()
		{
			fadeInTime = 1 / fadeInTime;
			animationTime = 1 / animationTime;
			fadeOutTime = 1 / fadeOutTime;
		}

		void Start ()
		{
			StartCoroutine (Sequence ());
		}

		private IEnumerator Sequence ()
		{
			bool fadeIn = false, animation = false, fadeOut = false;
			float t = 0;

			#region Fade In
			yield return new WaitForSeconds (fadeInDelay);

			while (!fadeIn) {
				t += Time.deltaTime * fadeInTime;
				if (t < 1) {
					float rate = Mathf.Lerp (1, 0, Util.LerpType.Quadratic (t));
					Util.UI.SetAlpha (mask, rate);
					yield return null;
				} else {
					Util.UI.SetAlpha (mask, 0);
					t = 0;
					fadeIn = true;
				}
			}
			#endregion
			#region Animation
			yield return new WaitForSeconds (animationDelay);

			while (!animation) {
				t += Time.deltaTime * animationTime;
				if (t < 1) { 
					float rate = Mathf.Lerp (0, 1, 1f - Util.LerpType.Coserp (t));
					Util.UI.SetFill (logotype, rate);
					Util.UI.SetFill (icon, rate);

					yield return null;
				} else {
					Util.UI.SetFill (logotype, 1);
					Util.UI.SetFill (icon, 1);
					sound.Play ();
					t = 0;
					animation = true;
				}
			}
			#endregion
			#region Fade Out
			yield return new WaitForSeconds (fadeOutDelay);

			while (!fadeOut) {
				t += Time.deltaTime * fadeInTime;
				if (t < 1) {
					float rate = Mathf.Lerp (0, 1, Util.LerpType.Quadratic (t));
					Util.UI.SetAlpha (mask, rate);
					yield return null;
				} else {
					Util.UI.SetAlpha (mask, 1);
					t = 0;
					fadeOut = true;
				}
			}
			if (debug)
				Debug.Log ("Debug is turned on");
			else
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Lobby");
			#endregion
		}


	}
}