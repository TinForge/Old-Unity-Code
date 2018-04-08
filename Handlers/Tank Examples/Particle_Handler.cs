using System.Collections;
using UnityEngine;
namespace TinForge.TankController
{
	public class Particle_Handler : TankComponent
	{
		//Components
		[SerializeField] private ParticleSystem shootParticles;
		[SerializeField] private Light shootLight;
		[SerializeField,Space] private ParticleSystem deathParticles;

		void Start()
		{
			gun_control.OnShoot += Shoot;
			health.OnDead += Dead;
		}

		void OnDisable()
		{
			gun_control.OnShoot -= Shoot;
			health.OnDead -= Dead;
		}

		//---

		private void Shoot()
		{
			shootParticles.Play();
			StartCoroutine(Flash(0, 3, 0.1f));
		}

		private IEnumerator Flash(float start, float end, float rate)
		{
			float t;
			t = 0;
			while (t <= 1)
			{
				shootLight.intensity = Mathf.Lerp(start, end, t * t);
				t += Time.deltaTime * (1 / rate);
				yield return null;
			}
			t = 0;
			while (t <= 1)
			{
				shootLight.intensity = Mathf.Lerp(end, start, t * t);
				t += Time.deltaTime * (1 / rate);
				yield return null;
			}
			shootLight.intensity = start;
		}

		private void Dead()
		{
			deathParticles.Play();
		}

		public void PlayProperty(ParticleSystem particle)
		{
			if (!particle.isPlaying)
			{
				particle.Play();
			}
		}
	}
}