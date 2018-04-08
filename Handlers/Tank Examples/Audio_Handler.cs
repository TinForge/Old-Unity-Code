using UnityEngine;
namespace TinForge.TankController
{
	public class Audio_Handler : TankComponent
	{
		//Components
		[SerializeField] private AudioSource hullTraverse;
		[SerializeField] private AudioSource turretTraverse;
		[SerializeField] private AudioSource cannonTraverse;
		[SerializeField] private AudioSource shoot;
		[SerializeField] private AudioSource hit;
		[SerializeField] private AudioSource dead;

		//Private
		float engineRate;

		//---

		private void Start()
		{
			hull_traverse.OnHullTraverse += HullTraverse;
			//turret_traverse.OnTurretTraverse += TurretTraverse;
			//gun_traverse.OnCannonTraverse += GunTraverse;
			gun_control.OnShoot += Shoot;
			health.OnHit += Hit;
			health.OnDead += Dead;
		}

		private void OnDisable()
		{
			hull_traverse.OnHullTraverse -= HullTraverse;
			turret_traverse.OnTurretTraverse -= TurretTraverse;
			gun_traverse.OnCannonTraverse -= GunTraverse;
			gun_control.OnShoot -= Shoot;
			health.OnHit -= Hit;
			health.OnDead -= Dead;
		}

		//---

		private void HullTraverse(float inputVertical, float inputHorizontal)
		{
			float mag = Mathf.Abs(inputVertical * 0.75f) + Mathf.Abs(inputHorizontal * 0.75f);
			engineRate = Mathf.MoveTowards(engineRate, mag, 0.03f);
			hullTraverse.pitch = Mathf.Lerp(.9f, 1.25f, engineRate);
			hullTraverse.volume = Mathf.Lerp(.15f, .4f, engineRate);
		}
		private void TurretTraverse()
		{

		}
		private void GunTraverse()
		{

		}

		private void Shoot()
		{
			shoot.Play();
		}

		private void Hit()
		{
			hit.Play();
		}

		private void Dead()
		{
			dead.Play();
			hullTraverse.Stop();
		}
	}
}