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
	private CanvasGroup cg;

	public bool active{ get { return cg.interactable; } }

	[HideInInspector] public bool busy;

	protected float fadeInBase;
	protected float fadeOutBase;

	public virtual float fadeInBase { get; protected set; }

	public virtual float fadeOutBase { get; protected set; }

	protected virtual void Awake ()
	{
		cg = GetComponent<CanvasGroup> ();

		fadeInBase = 1 / 0.5f;
		fadeOutBase = 1 / 0.25f;
	}

	public virtual void Enable ()
	{
		if (active)
			return;
		StopAllCoroutines ();
		StartCoroutine (EnableThread ());
	}

	private IEnumerator EnableThread ()
	{
		busy = true;
		float t = 0;
		Util.UI.SetAlpha (cg, 0);

		while (true) {
			t += Time.deltaTime * fadeInBase;
			if (t < 1) {
				float a = Mathf.Lerp (0, 1, Util.LerpType.SmoothStep (t));
				Util.UI.SetAlpha (cg, a);
				yield return null;
			} else {
				Util.UI.SetAlpha (cg, 1);
				break;
			}
		}
		busy = false;
	}
public virtual void Disable ()
	{
		if (!active)
			return;
		StopAllCoroutines ();
		StartCoroutine (DisableThread ());

	}

	private IEnumerator DisableThread ()
	{
		busy = true;
		float t = 0;
		Util.UI.SetAlpha (cg, 1);

		while (true) {
			t += Time.deltaTime * fadeOutBase;
			if (t < 1) {
				float a = Mathf.Lerp (1, 0, Util.LerpType.SmoothStep (t));
				Util.UI.SetAlpha (cg, a);
				yield return null;
			} else {
				Util.UI.SetAlpha (cg, 0);
				break;
			}
		}
		busy = false;
	}
}