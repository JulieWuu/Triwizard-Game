using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float totalBreath;
    [SerializeField] private Slider breathSlider;
    [SerializeField] private float maxTimeAllowed;
    [SerializeField] private float surfaceYCoordinate;

    private Rigidbody2D body;
    private Animator anim;
    private float currentBreath;
    private float breathingSpeed;

    private void Awake()
    {
        breathSlider.maxValue = totalBreath;
        currentBreath = totalBreath;
        breathingSpeed = totalBreath / maxTimeAllowed;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (body.position.y >= surfaceYCoordinate) return;
        if (currentBreath > 0 && body.position.y < surfaceYCoordinate)
        {
            currentBreath -= breathingSpeed * Time.deltaTime;
            breathSlider.value = currentBreath;
        }
        else
        {
            anim.SetTrigger("die");
            body.gravityScale = 1.5f;
            body.linearDamping = 2;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

}
