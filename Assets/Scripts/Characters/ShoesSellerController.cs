using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesSellerController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		animator.SetInteger("Type", (int)IdlingType.Afraid);
	}
}
