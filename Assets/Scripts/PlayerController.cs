﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Transform cam;
    public float jumpForce;
    public GameObject tree;
    public FauxGravityAttractor attractor;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 moveTarget;

    public LayerMask groundMask;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = true;
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
        ConstructTree();
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveTarget = Vector3.SmoothDamp(moveTarget, moveDirection * moveSpeed, ref velocity, 0.1f);
    }

    private void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(rb.transform.up * jumpForce);
        }

        onGround = false;

        Ray ray = new Ray(rb.transform.position, -rb.transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, .3f, groundMask))
        {
            onGround = true;
        }
    }

    private void ConstructTree()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject obj = Instantiate(tree, rb.position + rb.transform.forward * 1f, rb.rotation);
            obj.GetComponent<FauxGravityBody>().attractor = attractor;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveTarget) * Time.fixedDeltaTime);
    }
}
