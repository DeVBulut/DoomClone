using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;       // Force applied for the jump
    public float jumpCooldown;    // Cooldown between jumps
    public float airMultiplier;   // Movement speed multiplier when in the air
    bool readyToJump = true;      // To check if the player can jump

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.3f, whatIsGround);
        DetectInput();

        // Adjust drag based on grounded status
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = groundDrag / 2;
        }

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            Jump();
        }

    }

    private void DetectInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = oritentation.forward * verticalInput + oritentation.right * horizontalInput;

        // Different speed multiplier for grounded vs air movement
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
        }
    }

    private void Jump()
    {
        readyToJump = false;

        // Reset vertical velocity before applying jump force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Apply jump force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Start jump cooldown
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Staircase")
        {
            moveSpeed = 1.6f;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.tag == "Staircase")
        {
            moveSpeed = 0.85f;
        }
    }
}
