using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Ground Check")]
    private Vector3 velocity;
    private bool isGrounded;

    private CharacterController controller;

    void Start()
    {
        // Get the Character Controller component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Check if the player is touching the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // Small negative value ensures the player stays firmly snapped to the ground
            velocity.y = -2f; 
        }

        // 2. Get Input from Keyboard (WASD / Arrow Keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the character's facing direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the character based on user input
        controller.Move(move * walkSpeed * Time.deltaTime);

        // 3. Handle Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Physics formula for jumping: velocity = sqrt(height * -2 * gravity)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Apply Gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Move the controller again to apply the downward gravity/jump velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
