using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Movement Type")]
    public bool still;
    public bool slow;
    public bool medium;
    public bool fast;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

   
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        MyInput();
        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
        
    }

   

    private void MyInput()
    {
        Vector2 leftStick = Gamepad.current.leftStick.ReadValue();
        horizontalInput = leftStick.x;
        verticalInput = leftStick.y;

        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (moveDirection.magnitude > 0.6f)
        {
            fast = true;
        }
        else fast = false;

        if (moveDirection.magnitude <= 0.6f && moveDirection.magnitude >0.3f)
        {
            medium = true;
        }
        else medium = false;

        if (moveDirection.magnitude >0 && moveDirection.magnitude<0.3f)
        {
            slow = true;
        }
        else slow = false;

        if (slow == false && medium == false && fast == false)
            still = true;
        else still = false;


            //if (moveDirection.magnitude <= 0.6f) //(moveDirection.magnitude > 0.3f && moveDirection.magnitude <= 0.6f)
            //{
            //    rb.AddForce(moveDirection.normalized * 4.666f * 10f, ForceMode.Force);
            //    //rb.AddForce(moveDirection * (moveSpeed * 0.33f) * 10f, ForceMode.Force);
            //}
            //else if (moveDirection.magnitude > 0f && moveDirection.magnitude <= 0.3f)
            //{
            //    rb.AddForce(moveDirection.normalized * (moveSpeed*0.33f) * 10f, ForceMode.Force); // slow speed
            //}
            //else Debug.Log("check for moveDirection issue");

            Debug.Log("normal move: " + moveDirection.normalized);
        Debug.Log("basic move: " + moveDirection);
        Debug.Log("move: " + moveDirection.magnitude);
        Debug.Log("speed: " + rb.linearVelocity.magnitude);

    }

    private void SpeedControl()
    {
        if (fast == true)
            moveSpeed = 7f;
        else if (medium == true)
            moveSpeed = 4.5f;
        else if (slow == true)
            moveSpeed = 2.3f;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
