using System.Collections;
using UnityEngine;
using UnityEngine.UI;

///Inheriting classes become streamlined for Canvas/Panel transitioning
///Set fade times in deriving class
///Use active & busy to check object state or in process of fading
///Call Enable/Disable in deriving classes to start fade
///Override Step() in deriving classes to fade required UI elements
public class UIElementBase: MonoBehaviour
{
	public bool active{ get { return gameObject.activeSelf; } }

	[HideInInspector] public bool busy;

	protected float fadeInBase;
	protected float fadeOutBase;

	protected virtual void EnableStep (float t)
	{
	}

	protected virtual void DisableStep (float t)
	{
	}

	public virtual void Enable ()
	{
		gameObject.SetActive (true);
		StopAllCoroutines ();
		StartCoroutine (EnableThread ());
	}

	private IEnumerator EnableThread ()
	{
		busy = true;
		float t = 0;
		EnableStep (0);

		while (true) {
			t += Time.deltaTime * fadeInBase;
			if (t < 1) {
				float a = Mathf.Lerp (0, 1, Util.LerpType.SmoothStep (t));
				EnableStep (t);
				yield return null;
			} else {
				EnableStep (1);
				break;
			}
		}
		busy = false;
	}

	public virtual void Disable ()
	{
		StopAllCoroutines ();
		StartCoroutine (DisableThread ());
	}

	private IEnumerator DisableThread ()
	{
		busy = true;
		float t = 0;
		DisableStep (1);

		while (true) {
			t += Time.deltaTime * fadeOutBase;
			if (t < 1) {
				float a = Mathf.Lerp (1, 0, Util.LerpType.SmoothStep (t));
				DisableStep (a);
				yield return null;
			} else {
				DisableStep (0);
				break;
			}
		}
		gameObject.SetActive (false);
		busy = false;
	}
}
