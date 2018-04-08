using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint_Handler : MonoBehaviour {

	[SerializeField] private AudioClip breakSound;

	 void OnJointBreak()
	{
		if(breakSound!=null)
		{
			AudioSource a = gameObject.AddComponent<AudioSource>();
			a.spatialBlend = 1;
			a.PlayOneShot(breakSound);
		}
	}
}
