using UnityEngine;
using System.Collections;
using Menu;
using Tin.Resources;

public class SettingsManager : MonoBehaviour
{
	public static SettingsManager instance;

	[SerializeField] private SettingsGeneral general;
	[SerializeField] private SettingsControls controls;
	[SerializeField] private SettingsAudio audio;
	[SerializeField] private SettingsGraphics graphics;

	void Awake ()
	{
		if (instance != this && instance != null)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		instance = this;
	}

	public void LoadUserSettings ()
	{
		if (PlayerPreferences.NewUser) {
			general.DefaultPrefs ();
			controls.DefaultPrefs ();
			audio.DefaultPrefs ();
			graphics.DefaultPrefs ();
			Debug.Log ("First time loading in");
		} else
			Debug.Log ("User settings found");

		graphics.ApplyPrefs ();
	}


}