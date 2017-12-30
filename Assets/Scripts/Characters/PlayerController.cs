using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private const float GravityStrength = 9.81F;

    #region MovementAndForces

    private Vector3 force = new Vector3(0.0F, 0.0F, 0.0F);
    private Vector3 lastPosition;
    private Vector3 position;
    private int lastSpeed = 0;
    private Quaternion lastRotation;
    private Direction lastDirection;

    #endregion

    #region CharacterObjectSettings

    public float BasicSpeed = 50.0F;
    public float JumpForce = 10.0F;
    public float CharacterMass = 10.0F;
    public GameObject CurrentWeapon;
    private Animator animator;
    private new Camera camera;
    private CharacterController charController;
    private bool isActive = true;

    #endregion

    private bool isGrounded { get { return Math.Abs(lastPosition.y - transform.position.y) < 0.1F; } }

    public bool IsAttacking
    {
        get
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        lastRotation = camera.transform.rotation;
        charController = GetComponentInChildren<CharacterController>();
        lastPosition = transform.position;
        PauseEvent.Handler += OnPauseEvent;
    }

    void FixedUpdate()
    {
        if (!isActive)
            return;
        var isOverGUI = EventSystem.current.IsPointerOverGameObject();

        // Obrót postaci jest w kontrolerze obrotu kamery

        // Ruch postaci
        Direction dir = Direction.None;
        int AnimationSpeed = 0;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            AnimationSpeed = 2;
            dir += (int)Direction.Forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            AnimationSpeed = 2;
            dir += (int)Direction.Backward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            AnimationSpeed = 2;
            dir += (int)Direction.Left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            AnimationSpeed = 2;
            dir += (int)Direction.Right;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && AnimationSpeed != 0)
            AnimationSpeed = 3;

        if (isGrounded)
            force.y = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && force.y <= 0)
        {
            animator.SetTrigger(AnimatorHashes.Jump);
        }

        if (lastDirection != dir)
        {
            animator.SetInteger(AnimatorHashes.Direction, (int)dir);
            lastDirection = dir;
        }

        if (lastSpeed != AnimationSpeed)
        {
            lastSpeed = AnimationSpeed;
            animator.SetInteger(AnimatorHashes.Speed, AnimationSpeed);
        }

        // Siła grawitacji
        lastPosition = transform.position;
        force.y -= GravityStrength * CharacterMass * Time.deltaTime;
        charController.Move(force * Time.deltaTime);

        // Cios bronią
        if (!isOverGUI && Input.GetMouseButtonDown(0))
            animator.SetTrigger(AnimatorHashes.Attack);
    }

    private void OnPauseEvent(bool pause)
    {
        if (pause)
        {
            isActive = false;
            animator.speed = 0;
        }
        else
        {
            isActive = true;
            animator.speed = 1;
        }
    }
}