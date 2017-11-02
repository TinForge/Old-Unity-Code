using UnityEngine;
using HashTable = ExitGames.Client.Photon.Hashtable;
using Tin.Resources;

namespace Tin.Networking
{
	///Loads from player preferences
	///Exposes properties to gameobjects that access it
	public class Customization : Photon.PunBehaviour
	{
		private PhotonPlayer player;
		private HashTable properties;

		private const string KEY_Name = "name";
		private const string KEY_Color = "color";

		#region Lobby

		void Awake ()
		{
			properties = new HashTable ();
			player = PhotonNetwork.player;//Sets player to manipulate properties
		}

		public override void OnLeftLobby ()
		{
			LoadFromPrefs ();
		}

		public void LoadFromPrefs ()
		{
			PlayerName = PlayerPreferences.PlayerName;
			PlayerColorAsString = PlayerPreferences.PlayerColor;
		}

		#endregion

		#region Game

		public override void OnPhotonInstantiate (PhotonMessageInfo info) //Finds the PhotonPlayer that owns this object in game
		{
			properties = new HashTable ();
			player = photonView.owner;
			if (player == null)
				Debug.LogError ("PlayerProperties could not find PhotonPlayer component");
		}

		#endregion

		public string PlayerName {
			get {
				return (string)player.CustomProperties [KEY_Name];
			}
			set {
				properties = player.CustomProperties;
				if (properties.ContainsKey (KEY_Name))
					properties [KEY_Name] = value;
				else
					properties.Add (KEY_Name, value);
				player.SetCustomProperties (properties);
			}
		}

		private string PlayerColorAsString {
			get {
				return (string)player.CustomProperties [KEY_Color];
			}
			set {
				properties = player.CustomProperties;			
				if (properties.ContainsKey (KEY_Color))
					properties [KEY_Color] = value;
				else
					properties.Add (KEY_Color, value);
				player.SetCustomProperties (properties);
			}
		}

		public Color PlayerColor {
			get {
				Color c;
				ColorUtility.TryParseHtmlString (PlayerColorAsString, out c);
				return c;
			}
		}
	}
}
