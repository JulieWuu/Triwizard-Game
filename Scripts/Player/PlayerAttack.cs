using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float spellSpeed;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;

    private Animator animator;
    private float cooldownTimer = Mathf.Infinity;
    private bool armFacingRight = true;
    private Vector3 lookDirection;
    private bool canFire = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { canFire = true; }
        if (canFire && cooldownTimer > attackCooldown)
        {
            canFire = false;
            Cast();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Cast()
    {
        animator.SetTrigger("cast");
        cooldownTimer = 0;
        
        FindAngle();

        GameObject spellClone = Instantiate(bullet, firePoint.position, firePoint.rotation);
        spellClone.GetComponent<Rigidbody2D>().gravityScale = 0;

        spellClone.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(lookDirection.x, lookDirection.y).normalized * spellSpeed;
    }

    private void FindAngle()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = (mousePos - transform.position).normalized;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if ((armFacingRight && (lookAngle > 90 || lookAngle <= -90)) || (!armFacingRight && lookAngle <= 90 && lookAngle > -90))
        {
            armFacingRight = !armFacingRight;
            firePoint.localScale = new Vector3(firePoint.localScale.x, -firePoint.localScale.y, firePoint.localScale.z);
        }
    }
}
