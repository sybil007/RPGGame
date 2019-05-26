using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraveyardSoundController : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;
	AudioSource rootAudioSource;
	public bool AreZombiesAlive { get; set; }

	IEnumerable<ZombieController> zombies;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		rootAudioSource = GameObject.FindGameObjectWithTag("Global").GetComponent<AudioSource>();

		zombies = GameObject.FindGameObjectsWithTag("Zombie").Select(x => x.GetComponent<ZombieController>()).ToList();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		foreach (var z in zombies)
			z.PlayerInRange = true;

		if (!audioSource.isPlaying)
		{
			audioSource.time = 0;
			rootAudioSource.Pause();
			audioSource.Play();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag != "Player")
			return;

		foreach (var z in zombies)
			z.PlayerInRange = false;

		audioSource.Stop();
		rootAudioSource.Play();
	}
}
