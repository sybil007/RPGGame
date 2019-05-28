using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkerController : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		if (!audioSource.isPlaying)
		{
			audioSource.time = 43.0f;
			audioSource.SetScheduledEndTime(66);
			audioSource.Play();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag != "Player")
			return;

		audioSource.Stop();
	}
}
