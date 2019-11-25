using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera planetCamera = null;
    [SerializeField] private Camera playerCamera = null;
    [SerializeField] private Transform planet = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float speed = 5f;

    [SerializeField] private float minFOV = 30f;
    [SerializeField] private float maxFOV = 90f;
    [SerializeField] private float zoomSensitivity = 30f;
    [SerializeField] private float zoomTime = 0.2f;

    public float mouseSensitivity;

    private float velocityFOV;
    private float targetFOV;

    private bool playerView;
    private float vertical;

    void Start()
    {
        targetFOV = planetCamera.fieldOfView;
        playerView = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleCamera();
        MovePlanetCamera();
        MovePlayerCamera();
    }

    void LateUpdate()
    {
        planetCamera.fieldOfView = Mathf.SmoothDamp(planetCamera.fieldOfView, targetFOV, ref velocityFOV, zoomTime);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    private void ToggleCamera()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerView = !playerView;

            if (playerView)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                playerCamera.enabled = true;
                planetCamera.enabled = false;
            }

            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                playerCamera.enabled = false;
                planetCamera.enabled = true;
            }
        }
    }

    private void MovePlanetCamera()
    {
        if (!playerView)
        {
            if (Input.GetMouseButton(1))
            {
                planetCamera.transform.RotateAround(planet.position, planetCamera.transform.up, Input.GetAxis("Mouse X") * speed);
                planetCamera.transform.RotateAround(planet.position, planetCamera.transform.right, Input.GetAxis("Mouse Y") * -speed);
            }


            targetFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        }
    }

    private void MovePlayerCamera()
    {
        if (playerView)
        {
            player.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
            vertical += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            vertical = Mathf.Clamp(vertical, -60, 60);
            playerCamera.transform.localEulerAngles = Vector3.left * vertical;
        }
    }
}
