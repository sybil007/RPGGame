﻿using UnityEngine;
using System.Collections;

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

    void Start()
    {
        camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        float fov = camera.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * (-ScrollSpeed);
        fov = Mathf.Clamp(fov, minFov, maxFov);
        camera.fieldOfView = fov;

        camera.transform.position = player.transform.position + player.transform.rotation * new Vector3(0,20,-10);

    }
}