using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using Tin.Resources;

namespace Menu.Settings
{
	public class SettingsAudio : MonoBehaviour
	{
		public float masterVolume;
		public float generalVolume;
		public float effectsVolume;
		public float musicVolume;

		[SerializeField]private Text masterValue;
		[SerializeField]private Text generalValue;
		[SerializeField]private Text effectsValue;
		[SerializeField]private Text musicValue;
		[SerializeField]private Slider masterSlider;
		[SerializeField]private Slider generalSlider;
		[SerializeField]private Slider effectsSlider;
		[SerializeField]private Slider musicSlider;

		void OnEnable ()
		{
			LoadPrefs ();
		}

		public void DefaultPrefs ()
		{
			SetMasterVolume (50);
			SetGeneralVolume (50);
			SetEffectsVolume (50);
			SetMusicVolume (50);
		}

		public void LoadPrefs ()
		{
			SetMasterVolume (PlayerPreferences.MasterVolume);
			SetGeneralVolume (PlayerPreferences.GeneralVolume);
			SetEffectsVolume (PlayerPreferences.EffectsVolume);
			SetMusicVolume (PlayerPreferences.MusicVolume);
		}

		public void SavePrefs ()
		{
			PlayerPreferences.MasterVolume = masterVolume;
			PlayerPreferences.GeneralVolume = generalVolume;
			PlayerPreferences.EffectsVolume = effectsVolume;
			PlayerPreferences.MusicVolume = musicVolume;

			Audio.instance.SetVolume ();
		}

		public void SetMasterVolume (float value)
		{
			masterVolume = value;
			masterSlider.value = value;
			masterValue.text = value.ToString ("N0");
		}

		public void SetGeneralVolume (float value)
		{
			generalVolume = value;
			generalSlider.value = value;
			generalValue.text = value.ToString ("N0");
		}

		public void SetEffectsVolume (float value)
		{
			effectsVolume = value;
			effectsSlider.value = value;
			effectsValue.text = value.ToString ("N0");
		}

		public void SetMusicVolume (float value)
		{
			musicVolume = value;
			musicSlider.value = value;
			musicValue.text = value.ToString ("N0");
		}
	}
}
