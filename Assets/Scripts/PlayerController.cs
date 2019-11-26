using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject planet = null;
    [SerializeField] private GameObject selected;
    [SerializeField] private float minFOV = 30f;
    [SerializeField] private float maxFOV = 90f;
    [SerializeField] private float zoomSensitivity = 30f;
    [SerializeField] private float zoomTime = 0.2f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float dropHeight = 1f;

    private float velocityFOV;
    private float targetFOV;

    void Start()
    {
        targetFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        BuildSelected();
    }

    void LateUpdate()
    {
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref velocityFOV, zoomTime);
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButton(1))
        {
            cam.transform.RotateAround(planet.transform.position, cam.transform.up, Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
            cam.transform.RotateAround(planet.transform.position, cam.transform.right, Input.GetAxis("Mouse Y") * -mouseSensitivity * Time.deltaTime);
        }

        targetFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
    }

    private void BuildSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = Instantiate(selected, hit.point, Quaternion.identity);
                obj.transform.Translate(obj.transform.position + obj.transform.up * dropHeight);
                obj.transform.Rotate(Vector3.forward * Random.Range(0f, 360f));
                obj.GetComponent<FauxGravityBody>().attractor = planet.GetComponent<FauxGravityAttractor>();
            }
        }
    }
}
