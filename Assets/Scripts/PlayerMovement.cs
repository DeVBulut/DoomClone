using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform oritentation;
    float horizontalInput; 
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.1f + 0.2f, whatIsGround);
        DetectInput();

        if(grounded)
        {
            rb.drag = groundDrag; 
        }
        else 
        {
            rb.drag = groundDrag / 2;
        }

        // if(onAStaircase)
        // {
        //     moveSpeed = 2;
        // }
        // else
        // {
        //     moveSpeed = 1;
        // }
    }

    private void DetectInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = oritentation.forward * verticalInput + oritentation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Staircase")
        {
            moveSpeed = 1.6f; 
        }    
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Staircase")
        {
            moveSpeed = 0.85f; 
        }
    }
    
}
