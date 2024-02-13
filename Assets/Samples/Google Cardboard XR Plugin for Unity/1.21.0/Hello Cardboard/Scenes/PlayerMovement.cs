using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get the input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction based on camera orientation
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Move the player
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Check for jumps
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Jump forward
            Vector3 jumpVector = cameraForward * jumpForce;
            controller.Move(jumpVector * Time.deltaTime);
        }
    }
}
