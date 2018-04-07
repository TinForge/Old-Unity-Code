using UnityEngine;
using UnityEngine.Audio;

/// MonoBehaviour initializes static values on Awake
/// Call from anywhere to play something
/// Intended for one static source
public class AudioPlayer : MonoBehaviour
{
	[SerializeField]private AudioClip clickUI;
	public static AudioClip ClickUI;
	[SerializeField]private AudioClip ballTouch;
	public static AudioClip BallTouch;
	[SerializeField]private AudioClip ballThrow;
	public static AudioClip BallThrow;
	[SerializeField]private AudioClip ballCollide;
	public static AudioClip BallCollide;
	[SerializeField]private AudioClip gameOver;
	public static AudioClip GameOver;
	[SerializeField]private AudioClip highscore;
	public static AudioClip Highscore;
	[SerializeField]private AudioClip zap;
	public static AudioClip Zap;


	private static AudioSource Source;

	void Awake ()
	{
		ClickUI = clickUI;
		BallTouch = ballTouch;
		BallThrow = ballThrow;
		BallCollide = ballCollide;
		GameOver = gameOver;
		Highscore = highscore;
		Zap = zap;

		Source = GetComponent<AudioSource> ();
	}

	public static void Play (AudioClip clip)
	{
		Source.PlayOneShot (clip);
	}

	public static void Mute (bool mute)
	{
		Source.mute = mute;
	}
}
