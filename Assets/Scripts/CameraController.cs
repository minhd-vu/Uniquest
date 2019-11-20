using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private Vector3 previousPosition;

    // Update is called once per frame
    void Update()
    {
        //Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime
        //transform.RotateAround(center.position, Vector3.up, Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
        //transform.RotateAround(center.position, Vector3.left, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;
            cam.transform.Rotate(Vector3.right, direction.y * 180);
            cam.transform.Rotate(Vector3.up, -direction.x * 180, Space.World);
            cam.transform.Translate(Vector3.back * 10);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
