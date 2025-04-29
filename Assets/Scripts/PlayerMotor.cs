using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 7f;
    public float gravity = -9.81f;  
    public float jumpHeight = 1.6f;

    private bool isGrounded;

    private StaminaSystem stamina;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina = GetComponent<StaminaSystem>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void MovProcess(Vector2 input, bool isSprinting)
    {
        bool canSprint = isSprinting && stamina.CanSprint();
        float moveSpeed = canSprint ? speed * 1.5f : speed;

        // Optional: penalty for being out of stamina
        if (stamina.currentStamina <= 0f)
            moveSpeed *= 0.6f;

        if (canSprint)
        {
            stamina.UseStamina();
        }

        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
        Vector3 velocity = moveDirection * moveSpeed;

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1.5f;
        }

        velocity.y = playerVelocity.y;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
