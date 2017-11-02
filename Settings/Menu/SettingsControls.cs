using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu.Settings
{
	public class SettingsControls : MonoBehaviour
	{
		public static float sensitivity;

		[SerializeField]private Text sensitivityValue;
		[SerializeField]private Slider sensitivitySlider;

		void OnEnable ()
		{
			LoadPrefs ();
		}


		public void DefaultPrefs ()
		{
			PlayerPrefs.SetFloat ("sensitivity", 50);
			LoadPrefs ();
		}

		public void LoadPrefs ()
		{
			setSensitivity (PlayerPrefs.GetFloat ("sensitivity"));
		}

		public void SavePrefs ()
		{
			PlayerPrefs.SetFloat ("sensitivity", sensitivity);
		}

		public void setSensitivity (float value)
		{
			sensitivity = value;
			sensitivitySlider.value = value;
			sensitivityValue.text = value.ToString ("F0");
		}
	}
}
