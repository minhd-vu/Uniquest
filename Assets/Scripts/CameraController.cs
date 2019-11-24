using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera planetCamera;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform planet;
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomTime;

    public float mouseSensitivity;

    private float velocityFOV;
    private float targetFOV;

    private Vector3 velocityCamera;
    private Transform targetCamera;

    private bool playerView;
    private float vertical;

    void Start()
    {
        targetFOV = planetCamera.fieldOfView;
        targetCamera = planetCamera.transform;
        velocityCamera = Vector3.zero;
        playerView = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerView = !playerView;

            if (playerView)
            {
                playerCamera.enabled = true;
                planetCamera.enabled = false;
            }

            else
            {
                playerCamera.enabled = false;
                planetCamera.enabled = true;
            }
        }

        if (!playerView)
        {
            if (Input.GetMouseButton(1))
            {
                targetCamera.RotateAround(planet.position, targetCamera.transform.up, Input.GetAxis("Mouse X") * speed);
                targetCamera.RotateAround(planet.position, targetCamera.transform.right, Input.GetAxis("Mouse Y") * -speed);

                //planetCamera.transform.RotateAround(planet.position, planetCamera.transform.up, Input.GetAxis("Mouse X") * speed);
                //planetCamera.transform.RotateAround(planet.position, planetCamera.transform.right, Input.GetAxis("Mouse Y") * -speed);
            }


            targetFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        }

        else
        {
            player.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
            vertical += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            vertical = Mathf.Clamp(vertical, -60, 60);
            playerCamera.transform.localEulerAngles = Vector3.left * vertical;
        }
    }

    void LateUpdate()
    {
        planetCamera.fieldOfView = Mathf.SmoothDamp(planetCamera.fieldOfView, targetFOV, ref velocityFOV, zoomTime);
        planetCamera.transform.position = Vector3.SmoothDamp(planetCamera.transform.position, targetCamera.position, ref velocityCamera, 1.15f);
        planetCamera.transform.rotation = Quaternion.Slerp(planetCamera.transform.rotation, targetCamera.rotation, 02.5f);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
