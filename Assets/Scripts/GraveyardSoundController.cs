using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraveyardSoundController : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;
	AudioSource rootAudioSource;
	PlayerController playerScript;
	public bool AreZombiesAlive { get; set; }

	public AudioClip PlayerOnEnteringAudio;

	IEnumerable<ZombieController> zombies;
	private bool firstEntrance = true;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		rootAudioSource = GameObject.FindGameObjectWithTag("Global").GetComponent<AudioSource>();
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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

		if (!audioSource.isPlaying && zombies.Any(x => x.IsAlive))
		{
			audioSource.time = 0;
			rootAudioSource.Pause();
			audioSource.Play();
		}

		if (firstEntrance)
		{
			firstEntrance = false;
			playerScript.AudioSource.Stop();
			playerScript.AudioSource.clip = PlayerOnEnteringAudio;
			playerScript.AudioSource.Play();
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
