using System.Threading;
using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [Range(0, 1f)] 
    [SerializeField] private float yDamping = 0.5f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform targetObject;
    [SerializeField] private float aimingDistance;

    private Rigidbody2D rb;
    private float dirX = 1f;
    private Vector3 selfScale;
    private bool aiming = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        selfScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        Flip();
        Damp();
    }

    private void Update()
    {
        if (aiming)
        {
            selfScale.x = 1;
            LookAt(targetObject.position);
            if (Vector3.Distance(transform.position, targetObject.position) >= aimingDistance)
            {
                aiming = false;
            }
        }
        else
        {
            selfScale.x = dirX;
            float velAngle = Mathf.Atan2(rb.linearVelocity.y, dirX * rb.linearVelocity.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, velAngle);
            transform.rotation = rotation; // for smooth see void LookAt()
        }
        
        transform.localScale = selfScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            dirX *= -1;
            speed *= -1;
        }

        if (collision.gameObject.CompareTag("Spell")) { aiming = false; }
        if (collision.gameObject.name == "Player") { aiming = true; }
    }
    
    private void Flip()
    {
        if ((dirX > 0 && selfScale.x < 0) || (dirX < 0 && selfScale.x > 0)) selfScale.x *= -1;
        transform.localScale = selfScale;
    }

    private void Damp()
    {
        if (rb.linearVelocity.y != 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * (1 - yDamping));
        }
    }

    private void LookAt(Vector3 pos)
    {
        Vector3 lookDir = (pos - transform.position).normalized;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        
        if (lookAngle > 90 || lookAngle <= -90) 
        {
            selfScale.x = -1;
            lookAngle = lookAngle - 180;
        }
        else{ selfScale.x = 1; }

        transform.localScale = selfScale;
        Quaternion rotation = Quaternion.Euler(0, 0, lookAngle);
        transform.rotation = rotation;
        // for smooth use: Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
    }
}
