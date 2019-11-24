﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomTime;

    private float velocity;
    private float targetFOV;

    void Start()
    {
        targetFOV = cam.fieldOfView;
        velocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cam.transform.RotateAround(target.transform.position, cam.transform.up, Input.GetAxis("Mouse X") * speed);
            cam.transform.RotateAround(target.transform.position, cam.transform.right, Input.GetAxis("Mouse Y") * -speed);
        }

        targetFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
    }

    void LateUpdate()
    {
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref velocity, zoomTime);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
