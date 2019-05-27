using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseRotationCamera : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float ScrollSpeed = 5.0f;
    float minFov  = 15f;
    float maxFov  = 90f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private new Camera camera;
    private GameObject player;
	private PlayerController playerScript;
    public bool isActive = true;

    void Start()
    {
        camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerController>();
        PauseEvent.Handler += OnPauseEvent;
    }

    void LateUpdate()
    {
        if (!isActive)
            return;

        var isOverGUI = EventSystem.current.IsPointerOverGameObject();
        if (!isOverGUI)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		if (!playerScript.IsDead)
			player.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        float fov = camera.fieldOfView;
        if (!isOverGUI)
            fov += Input.GetAxis("Mouse ScrollWheel") * (-ScrollSpeed);
        fov = Mathf.Clamp(fov, minFov, maxFov);
        camera.fieldOfView = fov;

        if (Input.GetKey(KeyCode.Tab))
        {
            camera.transform.eulerAngles = camera.transform.eulerAngles + 180f * Vector3.up;
            camera.transform.position = player.transform.position + player.transform.rotation * new Vector3(0, 20, 10);
        }
        else
            camera.transform.position = player.transform.position + player.transform.rotation * new Vector3(0, 20, -10);

    }

    private void OnPauseEvent(bool pause)
    {
        if (pause)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }
}