using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tin.Resources;

namespace Scene.Lobby
{
	public class MultiplayerCanvas : UIElementBase
	{
		[SerializeField] private CanvasGroup group;
		[Space]
		[SerializeField] private GameObject randomPanel;
		[SerializeField] private GameObject createPanel;
		[SerializeField] private GameObject listPanel;
		[SerializeField] private GameObject filterPanel;
		[Space]
		[SerializeField] private Text playerCountStatistic;
		[SerializeField] private Text roomCountStatistic;
		[Space]
		[SerializeField] private AudioSource audioPlayer;
		[Space]
		[SerializeField] private float fadeInTime;
		[SerializeField] private float fadeOutTime;

		void Awake ()
		{
			fadeInTime = 1 / fadeInTime;
			fadeInBase = fadeInTime;
			fadeOutTime = 1 / fadeOutTime;
			fadeOutBase = fadeOutTime;
			InvokeRepeating ("RefreshStatistics", 0, 5);
		}

		public override void Enable ()
		{
			DisablePanels ();
			base.Enable ();
		}

		protected override void EnableStep (float t)
		{
			Util.UI.SetAlpha (group, t);
		}

		protected override void DisableStep (float t)
		{
			Util.UI.SetAlpha (group, t);
		}

		public override void Disable ()
		{
			if (!active)
				return;
			base.Disable ();
		}

		public void SwitchPanels (GameObject panel)
		{
			audioPlayer.PlayOneShot (Library.ClickUI);
			randomPanel.SetActive (false);
			createPanel.SetActive (false);
			listPanel.SetActive (false);
			filterPanel.SetActive (false);
			panel.SetActive (true);
		}

		private void DisablePanels ()
		{
			randomPanel.SetActive (false);
			createPanel.SetActive (false);
			listPanel.SetActive (false);
			filterPanel.SetActive (false);
		}

		public void RefreshStatistics ()
		{
			if (!PhotonNetwork.connected)
				return;
			playerCountStatistic.text = "Players: " + PhotonNetwork.countOfPlayers;
			roomCountStatistic.text = "Rooms: " + PhotonNetwork.countOfRooms;
		}
	}
}
