using UnityEngine;
using UnityEngine.Audio;
using Tin.Resources;

public class Audio : MonoBehaviour
{
	public static Audio instance;

	[Tooltip ("Debug Only")][SerializeField]private float masterVolume = 50;
	[Tooltip ("Debug Only")][SerializeField]private float generalVolume = 50;
	[Tooltip ("Debug Only")][SerializeField]private float effectsVolume = 50;
	[Tooltip ("Debug Only")][SerializeField]private float musicVolume = 50;

	[SerializeField]private AudioMixerGroup masterGroup;
	[SerializeField]private AudioMixerGroup generalGroup;
	[SerializeField]private AudioMixerGroup effectsGroup;
	[SerializeField]private AudioMixerGroup musicGroup;

	private void Awake ()
	{
		if (instance != this && instance != null)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		instance = this;
	}

	public void SetVolume ()
	{
		SetMasterVolume (PlayerPreferences.MasterVolume);
		SetGeneralVolume (PlayerPreferences.GeneralVolume);
		SetEffectsVolume (PlayerPreferences.EffectsVolume);
		SetMusicVolume (PlayerPreferences.MusicVolume);
	}

	public void SetMasterVolume (float value)
	{
		masterVolume = value;
		if (value == 0)
			value += 1;
		masterGroup.audioMixer.SetFloat ("masterVolume", (float)(Mathf.Log (value / 100) * 20));
	}

	public void SetGeneralVolume (float value)
	{
		generalVolume = value;
		if (value == 0)
			value += 1;
		generalGroup.audioMixer.SetFloat ("generalVolume", (float)(Mathf.Log (value / 100) * 20));
	}

	public void SetEffectsVolume (float value)
	{
		effectsVolume = value;
		if (value == 0)
			value += 1;
		effectsGroup.audioMixer.SetFloat ("effectsVolume", (float)(Mathf.Log (value / 100) * 20));
	}

	public void SetMusicVolume (float value)
	{
		musicVolume = value;
		if (value == 0)
			value += 1;
		musicGroup.audioMixer.SetFloat ("musicVolume", (float)(Mathf.Log (value / 100) * 20));
	}

}
