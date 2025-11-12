using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Projectile : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Solid")) { Hit(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC")) { Hit(); }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    private void Hit()
    {
        anim.SetTrigger("Hit");
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
}
