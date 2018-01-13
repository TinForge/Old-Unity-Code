using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;

namespace Scene.Lobby
{
	public class SettingsCanvas : UIElementBase
	{
		[SerializeField] private CanvasGroup group;
		[Space]	
		[SerializeField] private GameObject generalPanel;
		[SerializeField] private GameObject controlsPanel;
		[SerializeField] private GameObject graphicsPanel;
		[SerializeField] private GameObject audioPanel;
		[Space]
		[SerializeField] private AudioSource audioPlayer;
		[Space]
		[SerializeField] protected float fadeInTime;
		[SerializeField] protected float fadeOutTime;

		void Awake ()
		{
			fadeInTime = 1 / fadeInTime;
			fadeInBase = fadeInTime;
			fadeOutTime = 1 / fadeOutTime;
			fadeOutBase = fadeOutTime;
		}

		public override void Enable ()
		{
			DisablePanels ();
			base.Enable ();
		}

		public override void Disable ()
		{
			if (!active)
				return;
			base.Disable ();
		}

		protected override void EnableStep (float t)
		{
			Util.UI.SetAlpha (group, t);
		}

		protected override void DisableStep (float t)
		{
			Util.UI.SetAlpha (group, t);
		}

		public void SwitchPanels (GameObject panel)
		{
			audioPlayer.PlayOneShot (Library.ClickUI);
			generalPanel.SetActive (false);
			controlsPanel.SetActive (false);
			graphicsPanel.SetActive (false);
			audioPanel.SetActive (false);
			panel.SetActive (true);
		}

		private void DisablePanels ()
		{
			generalPanel.SetActive (false);
			controlsPanel.SetActive (false);
			graphicsPanel.SetActive (false);
			audioPanel.SetActive (false);
		}

	}
}
