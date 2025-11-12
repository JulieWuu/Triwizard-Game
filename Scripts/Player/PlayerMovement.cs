using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;

    private Rigidbody2D body;
    private Animator animator;
    private Vector2 movement;
    private bool isEnabled = true;
    public Vector3 pos;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        if (isEnabled) { Move(); Rotation(); }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY);
        movement.Normalize();

        body.linearVelocity = movement * Speed;

        animator.SetBool("run", !(moveX == 0 && moveY == 0));
    }
    
    private void Rotation()
    {
        if (movement != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, movement);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            body.MoveRotation(rotation);
        }
    }
}
