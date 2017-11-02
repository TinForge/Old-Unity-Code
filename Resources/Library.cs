using UnityEngine;

namespace Tin.Resources
{
	public class Library: MonoBehaviour
	{
		private static Library instance;

		[SerializeField] private AudioClip clickUI;
		[SerializeField] private AudioClip failureUI;
		[SerializeField] private AudioClip successUI;


		public static AudioClip ClickUI;

		public static AudioClip FailureUI;

		public static AudioClip SuccessUI;

		void Awake ()
		{
			if (instance != this && instance != null)
				Destroy (gameObject);

			ClickUI = clickUI;
			FailureUI = FailureUI;
			SuccessUI = successUI;
		}
	}
}