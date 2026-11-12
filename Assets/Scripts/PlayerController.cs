using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Moves the player on the XZ plane with WASD using a Rigidbody.
/// Reads input with the Input System package (the project has the legacy Input Manager disabled).
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody body;
    private Vector3 moveDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        moveDirection = ReadMoveDirection();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = body.linearVelocity.y;
        body.linearVelocity = velocity;
    }

    private static Vector3 ReadMoveDirection()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return Vector3.zero;
        }

        float horizontal = (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f);
        float vertical = (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f);

        return new Vector3(horizontal, 0f, vertical).normalized;
    }
}
