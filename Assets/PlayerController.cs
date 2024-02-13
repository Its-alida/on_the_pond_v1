using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 movementInput;

    [Header("Movement Settings")]
    public float movementSpeed = 5.0f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2.0f;

    private float verticalRotation = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        Vector3 move = transform.TransformDirection(moveDirection) * movementSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    private void HandleMouseLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
