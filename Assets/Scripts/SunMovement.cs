using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    [SerializeField] private Transform planet = null;
    [SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(planet);
        transform.RotateAround(planet.position, planet.up, speed * Time.deltaTime);
    }
}
