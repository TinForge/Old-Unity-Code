using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;

namespace Scene.Lobby.UI
{
	public class OnlineCanvas : UIElementBase
	{
		[SerializeField] private InputField username;
		[Space]
		[SerializeField] private CanvasGroup topPanel;
		[SerializeField] private GameObject noNamePanel;
		[SerializeField] private UIElementBase customizePanel;
		[SerializeField] private UIElementBase multiplayerPanel;
		[SerializeField] private UIElementBase settingsPanel;
		private UIElementBase FocusedPanel;
		[Space]
		[SerializeField] private float fadeInTime;

		void Awake ()
		{
			fadeInTime = 1 / fadeInTime;
			fadeInBase = fadeInTime;
		}

		void Start ()
		{
			FocusedPanel = multiplayerPanel;
			NameCheck ();
		}

		void Update ()
		{
			MonitorPanelState ();
		}

		private void MonitorPanelState ()
		{
			if (!(customizePanel.active || multiplayerPanel.active || settingsPanel.active) && !busy)
				FocusedPanel.Enable ();
		}

		public override void Enable ()
		{
			base.Enable ();
		}

		public override void Disable ()
		{
			if (!active)
				return;
			
			AudioPlayer.Play (Library.FailureUI);
			FocusedPanel.Disable ();
			base.Disable ();
		}

		protected override void EnableStep (float t)
		{
			Util.UI.SetAlpha (topPanel, t);
		}

		protected override void DisableStep (float t)
		{
			Util.UI.SetAlpha (topPanel, t);
		}

		public void ChangePanel (UIElementBase panel)
		{
			AudioPlayer.Play (Library.ClickUI);
			if (FocusedPanel == panel)
				return;
			FocusedPanel.Disable ();
			FocusedPanel = panel;
		}


		public void SetUserName (string input)
		{
			PlayerPreferences.PlayerName = input;
			NameCheck ();
		}

		private void NameCheck ()
		{
			if (PlayerPreferences.PlayerName == "") {
				noNamePanel.SetActive (true);
				AudioPlayer.Play (Library.FailureUI);
			} else {
				noNamePanel.SetActive (false);
				AudioPlayer.Play (Library.SuccessUI);
				username.text = PlayerPreferences.PlayerName;
			}
		}
	}
}
