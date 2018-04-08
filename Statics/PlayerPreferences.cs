using UnityEngine;

/// Encapsulates Unity's player prefs as properties
/// Values treated as static properties

public class PlayerPreferences : MonoBehaviour
{
	#region Ball Data

	public static int MaxForce = 100;
	public static int Frequency = 10;
	public static float Dampening = 1f;
	public static float Gravity = -16.81f;
	public static float Drag = 1;
	public static float Handicap = 0;

	#endregion

	#region PlayerPrefs

	private const string FirstTimeKEY = "firsttime";
	private const string HighScoreKEY = "highscore";
	private const string BallKEY = "ball";
	private const string MusicKEY = "music";
	private const string SoundKEY = "sound";

	void Awake ()
	{
		if (!PlayerPrefs.HasKey (FirstTimeKEY)) {
			FirstTime = 1;
			HighScore = 0;
			Ball = "Soccer";
			Music = true;
			Sound = true;
		}
	}

	public static int FirstTime {
		get { return PlayerPrefs.GetInt (FirstTimeKEY); }
		set { PlayerPrefs.SetInt (FirstTimeKEY, value); }
	}

	public static int HighScore {
		get { return PlayerPrefs.GetInt (HighScoreKEY); }
		set { PlayerPrefs.SetInt (HighScoreKEY, value); }
	}

	public static string Ball {
		get { return PlayerPrefs.GetString (BallKEY); }
		set { PlayerPrefs.SetString (BallKEY, value); }
	}

	public static bool Music {
		get { return PlayerPrefs.GetInt (MusicKEY) > 0 ? true : false; }
		set { PlayerPrefs.SetInt (MusicKEY, value == true ? 1 : -1); }
	}

	public static bool Sound {
		get { return PlayerPrefs.GetInt (SoundKEY) > 0 ? true : false; }
		set { PlayerPrefs.SetInt (SoundKEY, value == true ? 1 : -1); }
	}

	#endregion

}
