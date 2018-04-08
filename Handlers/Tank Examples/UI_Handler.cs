using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace TinForge.TankController
{
	public class UI_Handler : TankComponent
	{
		//Components
		[SerializeField] private Text tankName;
		[SerializeField] private CanvasGroup aliveCanvas;
		[SerializeField] private CanvasGroup deadCanvas;
		[SerializeField] private Image engineFill;
		[SerializeField] private Image reloadFill;
		[SerializeField] private Image healthFill;
		[SerializeField] private Image hitImage;
		[SerializeField] private Image deadImage;
		[SerializeField] private Image aimPoint;

		void Start()
		{
			health.OnHit += Hit;
			health.OnHitDamage += DebugDamage;
			health.OnDead += Dead;

			StartCoroutine(FadeCoroutine(aliveCanvas, 1));
		}

		void OnDisable()
		{
			health.OnHit -= Hit;
			health.OnHitDamage -= DebugDamage;
			health.OnDead -= Dead;
		}

		//---

		void Update()
		{
			engineFill.fillAmount = Mathf.Clamp(Mathf.Abs(hull_traverse.lastVertical * hull_traverse.lastVertical) + Mathf.Abs(hull_traverse.lastHorizontal * hull_traverse.lastHorizontal), 0, 1);
			reloadFill.fillAmount = gun_control.CoolDown;
			healthFill.fillAmount = (float) health.HP / Health.MaxHealth;
			aimPoint.rectTransform.position = Vector3.Lerp(aimPoint.rectTransform.position, gun_control.AimPoint, 0.5f);
		}

		//---

		void Hit()
		{
			StartCoroutine(HitThread());
		}

		private IEnumerator HitThread()
		{
			float t;
			t = 0;
			Color color = hitImage.color;
			while (t <= 1)
			{
				color.a = t * t;
				hitImage.color = color;
				t += Time.deltaTime * 10;
				yield return null;
			}
			t = 0;
			while (t <= 1)
			{
				color.a = 1 - t;
				hitImage.color = color;
				t += Time.deltaTime * 2;
				yield return null;
			}
		}

		//---
		void DebugDamage(int damage)
		{
			Debug.Log(damage + " Damage");
		}

		private void Dead()
		{
			deadImage.enabled = true;
			StartCoroutine(FadeCoroutine(aliveCanvas, 1));
			StartCoroutine(FadeCoroutine(deadCanvas, 1));
		}

		private IEnumerator FadeCoroutine(CanvasGroup cg, float seconds)
		{
			float start = Mathf.Round(cg.alpha);
			float end = start == 0 ? 1 : 0;
			float t = 0;
			while (t <= 1)
			{
				Util.UI.SetAlpha(cg, Mathf.Lerp(start, end, t));
				t += Time.deltaTime * (1 / seconds);
				yield return null;
			}
			cg.alpha = end;
		}
	}
}