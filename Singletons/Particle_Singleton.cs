using UnityEngine;

/// MonoBehaviour initializes static values on Awake
/// Call from anywhere to play something
/// Intended for one static source
public class Particle_Singleton : MonoBehaviour
{
	[SerializeField]private ParticleSystem stars;
	public static ParticleSystem Stars;
	[SerializeField]private ParticleSystem confetti;
	public static ParticleSystem Confetti;
	//public ParticleSystem stars;

	void Awake ()
	{
		Stars = stars;
		Confetti = confetti;
	}

	public static void Play (ParticleSystem particle)
	{
		if (!particle.isPlaying)
			particle.Play ();
	}

}
