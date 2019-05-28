using System;
using System.Linq;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	private AudioSource audioSource;
	private Animator animator;
	private GameObject player;
	private PlayerController playerScript;
	private CharacterController controller;

	public AudioClip deathClip;
	public AudioClip killClip;
	public AudioClip allZombiesDeadClip;

	public float RotationSpeed = 2;
	public float Speed = 10;
	public float GraveyardEntranceLine = 1780;
	public float AttackForce = 30;

	public bool IsAlive = true;
	public bool PlayerInRange
	{
		get { return _playerInRange; }
		set
		{
			_playerInRange = value;
			animator.SetBool(AnimatorHashes.PlayerInRange, value);
		}
	}
	private bool _playerInRange;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (IsAlive)
		{
			if (_playerInRange)
			{
				if (playerScript.IsDead)
				{
					OnPlayerDead();
					return;
				}
				//find the vector pointing from our position to the target
				var direction = (player.transform.position - transform.position).normalized;
				//create the rotation we need to be in to look at the target
				var lookRotation = Quaternion.LookRotation(direction);

				// Speed and time 
				float step = Speed * Time.deltaTime;
				//rotate us over time according to speed until we are in the required rotation
				if (!IsEqualRotation(lookRotation.eulerAngles, transform.rotation.eulerAngles, 0.3f))
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
					transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
				}

				if (!IsEqualPosition(transform.position, player.transform.position, 0.1F))
					controller.Move(Vector3.MoveTowards(transform.position, player.transform.position, step) - transform.position);
			}
		}
	}

	private void LateUpdate()
	{
		transform.position = new Vector3(transform.position.x, -0, Math.Max(transform.position.z, GraveyardEntranceLine));
	}

	private bool IsEqualRotation(Vector3 first, Vector3 second, float epsilon)
	{
		if (Math.Abs(first.y - second.y) > epsilon)
			return false;

		return true;
	}

	private bool IsEqualPosition(Vector3 first, Vector3 second, float epsilon)
	{
		if (Math.Abs(first.x - second.x) > epsilon)
			return false;

		if (Math.Abs(first.z - second.z) > epsilon)
			return false;

		return true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player" || !IsAlive)
			return;

		audioSource.time = 0;
		audioSource.Play();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag != "Player")
			return;

		audioSource.Stop();
	}

	private void OnCollisionEnter(Collision collision)
	{
		DetermineCollisionWithPlayer(collision.collider);
		if (!IsAlive || collision.collider.tag != "PlayerWeapon")
			return;

		var playerScript = player.GetComponent<PlayerController>();
		if (!playerScript.IsAttacking)
			return;

		IsAlive = false;
		audioSource.Stop();
		audioSource.clip = deathClip;
		audioSource.loop = false;
		audioSource.Play();

		animator.SetBool(AnimatorHashes.Death, true);

		if (GameObject.FindGameObjectsWithTag("Zombie").All(x => !x.GetComponent<ZombieController>().IsAlive))
		{
			playerScript.AudioSource.Stop();
			playerScript.AudioSource.clip = allZombiesDeadClip;
			playerScript.AudioSource.Play();
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		DetermineCollisionWithPlayer(collision.collider);
	}

	private void DetermineCollisionWithPlayer(Collider col)
	{
		if (col.tag != "Player" || !IsAlive)
			return;

		playerScript.Health -= Time.deltaTime * AttackForce;
	}

	private void OnPlayerDead()
	{
		_playerInRange = false;
		animator.SetBool(AnimatorHashes.PlayerInRange, false);
		audioSource.Stop();

		audioSource.clip = killClip;
		audioSource.loop = false;
		audioSource.Play();
	}
}
