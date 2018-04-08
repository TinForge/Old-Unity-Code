using UnityEngine;
using UnityEngine.UI;

namespace Util
{
	public static class UI
	{
		public static void SetAlpha (this Image image, float alpha)
		{
			Color c = image.color;
			c.a = alpha;
			image.color = c;
		}

		public static void SetAlpha (this Text text, float alpha)
		{
			Color c = text.color;
			c.a = alpha;
			text.color = c;
		}

		public static void SetAlpha (this CanvasGroup cg, float alpha)
		{
			cg.alpha = alpha;
			if (alpha == 0) {
				cg.interactable = false;
				cg.blocksRaycasts = false;
			} else {
				cg.interactable = true;
				cg.blocksRaycasts = true;
			}
		}

		public static void SetFill (this Image image, float fill)
		{
			image.fillAmount = fill;
		}
	}
}
