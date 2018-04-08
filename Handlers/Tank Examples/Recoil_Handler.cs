using System.Collections;
using UnityEngine;
namespace TinForge.TankController
{
	//Simulate recoil effects by adjusting transform rotations on hull
	public class Recoil_Handler : TankComponent
	{
		//Values
		[SerializeField] private float driveRecoilAmount = 8;
		[SerializeField] private float driveRecoilRate = 1;
		[SerializeField, Space] private float shootRecoilAmount = 8;
		[SerializeField] private float shootRecoilRateTo = 5;
		[SerializeField] private float shootRecoilRateFrom = 3;
		[SerializeField, Space] private float hitRecoilAmount = 20;
		[SerializeField] private float hitRecoilRateTo = 5;
		[SerializeField] private float hitRecoilRateFrom = 2;

		//Private
		private float lerpRate = 0.5f;
		private Quaternion driveRecoil = Quaternion.identity;
		private Quaternion shootRecoil = Quaternion.identity;
		private Quaternion hitRecoil = Quaternion.identity;

		//---

		void Start()
		{
			gun_control.OnShoot += Shoot;
			hull_traverse.OnHullTraverse += Drive;
			health.OnHitDirection += Hit;
		}

		void OnDisable()
		{
			gun_control.OnShoot -= Shoot;
			hull_traverse.OnHullTraverse -= Drive;
			health.OnHitDirection -= Hit;
		}

		//---

		void Update()
		{
			hull.localRotation = Quaternion.Lerp(driveRecoil, shootRecoil, lerpRate);
			hull.localRotation = Quaternion.Lerp(hull.localRotation, hitRecoil, lerpRate);
		}

		//---

		public void Drive(float inputVertical, float inputHorizontal)
		{
			driveRecoil = Quaternion.Euler(inputVertical * (Mathf.PingPong(Time.time * driveRecoilRate, driveRecoilAmount) - driveRecoilAmount), 0, inputHorizontal * (Mathf.PingPong(Time.time * driveRecoilRate, driveRecoilAmount) - driveRecoilAmount));
		}

		//---

		public void Shoot()
		{
			StartCoroutine(ShootThread());
		}

		public IEnumerator ShootThread()
		{
			float t;
			t = 0;
			shootRecoil = Quaternion.identity;
			while (t <= 1)
			{
				shootRecoil = Quaternion.AngleAxis(Mathf.Sin(t * Mathf.PI * 0.5f) * shootRecoilAmount, hull.InverseTransformDirection(-turret.right));
				t += Time.deltaTime * shootRecoilRateTo;
				yield return null;
			}
			t = 0;
			while (t <= 1)
			{
				shootRecoil = Quaternion.AngleAxis(shootRecoilAmount - (t * t * shootRecoilAmount), hull.InverseTransformDirection(-turret.right));
				t += Time.deltaTime * shootRecoilRateFrom;
				yield return null;
			}
			shootRecoil = Quaternion.identity;
		}

		//---

		public void Hit(Vector3 direction)
		{
			StartCoroutine(HitThread(direction));
		}

		public IEnumerator HitThread(Vector3 direction)
		{
			float t;
			t = 0;
			hitRecoil = Quaternion.identity;
			while (t <= 1)
			{
				hitRecoil = Quaternion.AngleAxis(Mathf.Sin(t * Mathf.PI * 0.5f) * hitRecoilAmount, hull.InverseTransformDirection(direction));
				t += Time.deltaTime * hitRecoilRateTo;
				yield return null;
			}
			t = 0;
			while (t <= 1)
			{
				hitRecoil = Quaternion.AngleAxis(hitRecoilAmount - (Mathf.Sin(t * Mathf.PI * 0.5f) * hitRecoilAmount), hull.InverseTransformDirection(direction));
				t += Time.deltaTime * hitRecoilRateFrom;
				yield return null;
			}
			hitRecoil = Quaternion.identity;
		}
	}
}
