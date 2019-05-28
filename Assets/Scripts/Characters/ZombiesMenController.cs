using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombiesMenController : MonoBehaviour {

	AudioSource audioSource;
	Animator animator;
	public AudioClip ScaredAudio;
	public AudioClip OkAudio;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		animator.SetTrigger(AnimatorHashes.Hello);
		if (!audioSource.isPlaying)
		{
			if (GameObject.FindGameObjectsWithTag("Zombie").Any(x => x.GetComponent<ZombieController>().IsAlive))
				audioSource.clip = ScaredAudio;
			else
				audioSource.clip = OkAudio;

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
