using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    public float BaseSpeed = 100.0F;
    public float JumpForce = 10.0F;
    public float CharacterMass = 10.0F;

    private const float GravityStrength = 9.81F;
    private Vector3 force = new Vector3(0.0F, 0.0F, 0.0F);
    private Vector3 position;
    private Vector3 lastPosition;

    private bool leftButtonDown = false;

    private Animator animator;
    private Quaternion lastRotation;
    public new Camera camera;
    private new Collider collider;
    private CharacterController charController;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        lastRotation = camera.transform.rotation;
        collider = gameObject.GetComponent<Collider>();
        charController = GetComponentInChildren<CharacterController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Obrót postaci
        var CharacterRotation = camera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        CharacterRotation.y = (4 * lastRotation.y + CharacterRotation.y)/5.0F;
        gameObject.transform.rotation = CharacterRotation;
        lastRotation = CharacterRotation;

        // Ruch postaci
        float speed = BaseSpeed;
        bool wasMoved = false;
        var movement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            speed *= 3;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            wasMoved = true;
            movement += gameObject.transform.rotation * new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            wasMoved = true;
            movement += gameObject.transform.rotation * new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            wasMoved = true;
            movement += gameObject.transform.rotation * new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            wasMoved = true;
            movement += gameObject.transform.rotation * new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && transform.position.y == lastPosition.y)
            force.y += JumpForce;

        force.y -= GravityStrength * CharacterMass * Time.deltaTime;
        movement += force * Time.deltaTime;
        lastPosition = gameObject.transform.position;

        charController.Move(movement);

        position = gameObject.transform.position;
        animator.SetBool("Idling", !wasMoved);

        // Cios bronią
        if (Input.GetMouseButton(0))// are we using the right button?
        {
            if (leftButtonDown != true)// was it previously down? if so we are already using "USE" bailout (we don't want to repeat attacks 800 times a second...just once per press please
            {
                    animator.SetTrigger("Use");//tell mecanim to do the attack animation(trigger)

                    animator.SetBool("Idling", true);//stop moving
                    leftButtonDown = true;//right button was not down before, mark it as down so we don't attack 800 frames a second 
            }
        }

        if (Input.GetMouseButtonUp(0))
            leftButtonDown = false;
    }

    void LateUpdate()
    {
        gameObject.transform.position = position;
    }

    void OnCollisionEnter()
    {
        Debug.Log("Collision");
    }
}