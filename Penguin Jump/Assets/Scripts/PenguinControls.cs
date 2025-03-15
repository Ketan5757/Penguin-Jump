using UnityEngine;
using UnityEngine.InputSystem;

public class PenguinControls : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 2f;

    [SerializeField] private Rigidbody2D rigidBody;

    private Vector2 moveDirection;

    public InputActionReference move;
    
    
    private void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * movingSpeed, moveDirection.y * movingSpeed);
    }
}
