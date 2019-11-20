using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float minimumZoom;
    [SerializeField] private float maximumZoom;

    private Vector3 previousPosition;
    private float currentZoom;

    // Update is called once per frame
    void Update()
    {
        //cam.transform.position = target.position;
        //cam.transform.Rotate(Vector3.right, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        //cam.transform.Rotate(Vector3.up, -Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Space.World);
        //cam.transform.Translate(Vector3.back * minimumZoom);

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }


        Vector3 direction = Input.GetMouseButton(0) ? (previousPosition - cam.ScreenToViewportPoint(Input.mousePosition)) * 180 : new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);

        cam.transform.position = target.position;
        cam.transform.Rotate(Vector3.right, direction.y);
        cam.transform.Rotate(Vector3.up, -direction.x, Space.World);
        cam.transform.Translate(Vector3.back * minimumZoom);

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}
