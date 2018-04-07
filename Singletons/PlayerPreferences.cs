using UnityEngine;

namespace Tin.Resources
{
	///Custom interface to interact with Player Preferences
	/// Has the master string for preference/property keys
	public static class PlayerPreferences
	{
		//Strings are for accessing respective properties
		private const string KEY_Name = "name";
		private const string KEY_Color = "color";

		private const string KEY_MasterVolume = "mastervolume";
		private const string KEY_GeneralVolume = "generalvolume";
		private const string KEY_EffectsVolume = "effectsvolume";
		private const string KEY_MusicVolume = "musicvolume";

		private const string KEY_NewUser = "newuser2";



		#region Player Properties

		public static string PlayerName {
			get {
				if (!PlayerPrefs.HasKey (KEY_Name))
					PlayerPrefs.SetString (KEY_Name, "");

				string name = PlayerPrefs.GetString (KEY_Name);
				PhotonNetwork.playerName = name;
				return name;
			}
			set {
				PlayerPrefs.SetString (KEY_Name, value);
			}
		}

		public static string PlayerColor {
			get {
				if (!PlayerPrefs.HasKey (KEY_Color))
					PlayerPrefs.SetString (KEY_Color, "#000000");
				return PlayerPrefs.GetString (KEY_Color);
			}
			set {
				PlayerPrefs.SetString (KEY_Color, "#" + value);
			}
		}

		#endregion

		#region Volumes

		public static float MasterVolume {
			get {
				if (!PlayerPrefs.HasKey (KEY_MasterVolume))
					PlayerPrefs.SetFloat (KEY_MasterVolume, 50);
				return PlayerPrefs.GetFloat (KEY_MasterVolume);
			}
			set {
				PlayerPrefs.SetFloat (KEY_MasterVolume, value);
			}
		}

		public static float GeneralVolume {
			get {
				if (!PlayerPrefs.HasKey (KEY_GeneralVolume))
					PlayerPrefs.SetFloat (KEY_GeneralVolume, 50);
				return PlayerPrefs.GetFloat (KEY_GeneralVolume);
			}
			set {
				PlayerPrefs.SetFloat (KEY_GeneralVolume, value);
			}
		}

		public static float EffectsVolume {
			get {
				if (!PlayerPrefs.HasKey (KEY_EffectsVolume))
					PlayerPrefs.SetFloat (KEY_EffectsVolume, 50);
				return PlayerPrefs.GetFloat (KEY_EffectsVolume);
			}
			set {
				PlayerPrefs.SetFloat (KEY_EffectsVolume, value);
			}
		}

		public static float MusicVolume {
			get {
				if (!PlayerPrefs.HasKey (KEY_MusicVolume))
					PlayerPrefs.SetFloat (KEY_MusicVolume, 50);
				return PlayerPrefs.GetFloat (KEY_MusicVolume);
			}
			set {
				PlayerPrefs.SetFloat (KEY_MusicVolume, value);
			}
		}

		#endregion

		public static bool NewUser {
			get {
				if (!PlayerPrefs.HasKey (KEY_NewUser)) {
					PlayerPrefs.SetString (KEY_NewUser, "Nope");
					return true;
				} else {
					return false;
				}
			}
		}
	}
}

