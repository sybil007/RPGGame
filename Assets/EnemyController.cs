using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public List<Vector3> Route;
    public float Speed = 50;
    public float RotationSpeed = 5;

    private int currentPoint = 0;
    private CharacterController controller;
    private Quaternion lookRotation;
    private Animator anim;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsEqual(transform.position, Route[currentPoint], 0.1f))
        {
            currentPoint = (currentPoint + 1) % Route.Count;
            //find the vector pointing from our position to the target
            var direction = (Route[currentPoint] - transform.position).normalized;
            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);
        }

        // Speed and time 
        float step = Speed * Time.deltaTime;
        //rotate us over time according to speed until we are in the required rotation
        if (!IsEqual(lookRotation.eulerAngles, transform.rotation.eulerAngles, 0.1f))
        {
            anim.SetTrigger("idleTrigger");
            anim.SetFloat("idleSelect", 0.6f);
            anim.SetFloat("walkSelect", 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }
        else
        {
            // Get the enemy moving! 
            anim.SetTrigger("idleTrigger");
            anim.SetFloat("idleSelect", 0.1f);
            anim.SetFloat("walkSelect", 0f);
            controller.Move(Vector3.MoveTowards(transform.position, Route[currentPoint], step) - transform.position);
        }
    }

    private bool IsEqual(Vector3 first, Vector3 second, float epsilon)
    {
        if (Math.Abs(first.x - second.x) > epsilon)
            return false;

        if (Math.Abs(first.y - second.y) > epsilon)
            return false;

        if (Math.Abs(first.z - second.z) > epsilon)
            return false;

        return true;
    }
}
