using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement
    private CharacterController controller;
    private Animator animator;
    private float verticalVelocity = 0f;

    public AudioClip footstepAudio;

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
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Apply fake gravity to ensure the player stays grounded
        if (controller.isGrounded)
        {
            verticalVelocity = 0f; // Reset vertical velocity
        }
        else
        {
            // Apply downward force when not grounded
            verticalVelocity -= 9.81f * Time.deltaTime; // Adjust gravity as needed
        }

        // Combine horizontal movement with vertical velocity
        Vector3 movement = moveDirection * moveSpeed + Vector3.up * verticalVelocity;

        // Set isMoving parameter in the Animator controller
        animator.SetBool("isMoving", moveDirection.magnitude > 0);

        // Move the player based on the combined movement vector
        controller.Move(movement * Time.deltaTime);

        // Rotate the player based on movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    public void Footstep()
    {
        StartCoroutine(FootstepRoutine());
    }

    private IEnumerator FootstepRoutine()
    {
        AudioSource footstepSource = gameObject.AddComponent<AudioSource>();

        footstepSource.pitch = Random.Range(0.55f, 1f);
        footstepSource.clip = footstepAudio;
        footstepSource.Play();

        yield return new WaitForSeconds(footstepAudio.length);

        Destroy(footstepSource);
    }
}
