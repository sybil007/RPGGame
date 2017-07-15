﻿using UnityEngine;
using System.Collections;

public class MouseRotationCamera : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    float cameraDistanceMax = 10f;
    float cameraDistanceMin = 1f;
    float scrollSpeed = 0.5f;
    float lastScrollPos = 0f;

    void Start()
    {
        lastScrollPos = Input.GetAxis("Mouse ScrollWheel");
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        var cameraDistance = (Input.GetAxis("Mouse ScrollWheel") - lastScrollPos) * scrollSpeed;
        //cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);

        gameObject.transform.position += new Vector3(0f, 0f,-cameraDistance);
    }
}