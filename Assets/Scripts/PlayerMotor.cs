using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 7f;
    public float gravity = -9.81f;  
    public float jumpHeight = 1.6f;
    private bool isGrounded;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    public void MovProcess(Vector2 input, bool isSprinting)
{
    float moveSpeed = isSprinting ? speed * 1.5f : speed;

    // Create movement vector
    Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
    Vector3 velocity = moveDirection * moveSpeed;

    // Apply gravity
    playerVelocity.y += gravity * Time.deltaTime;

    if (isGrounded && playerVelocity.y < 0)
    {
        playerVelocity.y = -1.5f;
    }

    // Combine horizontal and vertical movement
    velocity.y = playerVelocity.y;

    // Move player
    controller.Move(velocity * Time.deltaTime);
}
    public void Jump()
    {
        if(isGrounded)

        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}

