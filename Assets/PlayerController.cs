using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private Animator animator;

    private bool isMoving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Set the move direction based on input
        moveDirection = new Vector3(horizontalInput, 0f, 0f).normalized;

        animator.SetBool("isMoving", moveDirection.magnitude > 0);
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
