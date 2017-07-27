using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public List<Vector3> Route;
    public float Speed = 10;
    public float RotationSpeed = 5;

    private int currentPoint = 0;
    private CharacterController controller;
    private Quaternion lookRotation;
    private Animator anim;

    private Vector3 position;
    private Quaternion rotation;
    // Use this for initialization

    private bool isActive = true;

	void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (Route.Count > 1)
        {
            anim.SetInteger(AnimatorHashes.Speed, 1);
            anim.SetInteger(AnimatorHashes.Direction, (int)Direction.Forward);
        }

        PauseEvent.Handler += OnPauseEvent;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isActive)
            return;

        if (Route.Count > 1)
        {
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
            if (!IsEqual(lookRotation.eulerAngles, transform.rotation.eulerAngles, 0.3f))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            }
            else
            {
                controller.Move(Vector3.MoveTowards(transform.position, Route[currentPoint], step) - transform.position);
            }

            rotation = transform.rotation;
            position = transform.position;
        }
    }

    private void LateUpdate()
    {
        transform.position = position;
        transform.rotation = rotation;
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

    private void OnPauseEvent(bool pause)
    {
        if (pause)
        {
            isActive = false;
            anim.speed = 0;
        }
        else
        {
            isActive = true;
            anim.speed = 1;
        }
    }
}
