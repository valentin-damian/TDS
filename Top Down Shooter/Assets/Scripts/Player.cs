using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundery;
    [SerializeField] int playerHealth = 5;
    [SerializeField] float invisibleTime = 5f;

    [SerializeField] float moveSpeed = 7;
    [SerializeField] float bulletSpeed = 9;
    [SerializeField] float rotationSpeed = 700;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;

    float targetAngle;

    bool invisible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundery = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D playerBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundery.x, screenBoundery.x), 
                                         Mathf.Clamp(transform.position.y, -screenBoundery.y, screenBoundery.y));
    }

    private void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);

    }

    void ResetInvincibility()
    {
        invisible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies") && !invisible)
        {
            if (playerHealth <= 1)
            {
                Destroy(gameObject);

            }
            else
            {
                playerHealth -= 1;
                invisible = true;
                Invoke("ResetInvincibility", invisibleTime);
                Debug.Log("Player's HP: " + playerHealth);
            }
        }
    }
}
