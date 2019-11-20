using System.Collections;
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

    private Vector3 previousPosition;
    private float depth;
    private float velocity;
    private float targetFOV;

    void Start()
    {
        targetFOV = cam.fieldOfView;
        depth = cam.transform.position.z;
        velocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        
        // Set the direction depending on whether the player is clicking or not.
        Vector3 direction = Input.GetMouseButton(0) ?
            (previousPosition - cam.ScreenToViewportPoint(Input.mousePosition)) * 180 :
            new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);

        // Rotate the camera around the planet.
        cam.transform.position = target.position;
        cam.transform.Rotate(Vector3.right, direction.y);
        cam.transform.Rotate(Vector3.up, -direction.x, Space.World);
        cam.transform.Translate(Vector3.forward * depth);
        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        

        /*
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;
            cam.transform.Rotate(Vector3.right, direction.y * 180);
            cam.transform.Rotate(Vector3.up, -direction.x * 180, Space.World);
            cam.transform.Translate(Vector3.forward * depth);
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        */


        // Allows for scrolling to zoom in and our on the planet.
        targetFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
    }

    void LateUpdate()
    {
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref velocity, zoomTime);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
