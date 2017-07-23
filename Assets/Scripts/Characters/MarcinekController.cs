using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcinekController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public bool IsAngry { get; set; }

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Type", (int)IdlingType.Angry);
        audioSource = GetComponent<AudioSource>();
        IsAngry = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (IsAngry && !audioSource.isPlaying)
        {
            audioSource.time = 14.2f;
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
