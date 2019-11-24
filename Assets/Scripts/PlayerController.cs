using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Transform cam;
    public float jumpForce;

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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveTarget) * Time.fixedDeltaTime);
    }
}
