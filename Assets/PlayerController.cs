using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Set the move direction based on input
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Set isMoving parameter in the Animator controller
        animator.SetBool("isMoving", moveDirection.magnitude > 0);

        if (!controller.isGrounded)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        // Move the player based on the move direction
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player based on movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
