using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 7;
    [SerializeField] float bulletSpeed = 9;
    [SerializeField] GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        GameObject playerBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = playerBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        
    }
}
