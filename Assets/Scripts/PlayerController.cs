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
    private int lastSpeed = 0;
    public GameObject CurrentWeapon;

    private Animator animator;
    private Quaternion lastRotation;
    public new Camera camera;
    private new Collider collider;
    private CharacterController charController;

    private bool isGrounded { get { return lastPosition.y == transform.position.y; } }

    void Start()
    {
        animator = GetComponent<Animator>();
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
        int Speed = 0;
        var movement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Speed += 1;
            speed *= 3;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Speed += 2;
            movement += gameObject.transform.rotation * new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Speed += 2;
            movement += gameObject.transform.rotation * new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Speed += 2;
            movement += gameObject.transform.rotation * new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Speed += 2;
            movement += gameObject.transform.rotation * new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        }

        if (isGrounded)
            force.y = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && force.y <= 0)
        {
            animator.SetTrigger("Jump");
            force.y += JumpForce;
        }

        if (lastSpeed != Speed)
        {
            lastSpeed = Speed;
            animator.SetInteger("Speed", Speed);
        }

        force.y -= GravityStrength * CharacterMass * Time.deltaTime;
        movement += force * Time.deltaTime;

        lastPosition = gameObject.transform.position;

        charController.Move(movement);

        position = gameObject.transform.position;

        // Cios bronią
        if (Input.GetMouseButtonDown(0))// are we using the right button?
        {
            animator.SetTrigger("Attack");//tell mecanim to do the attack animation(trigger)
        }
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