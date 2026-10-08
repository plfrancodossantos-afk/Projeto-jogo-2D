using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;

    private float direction = 1f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.freezeRotation = true;

        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        rb.linearVelocity =
            new Vector2(
                direction * speed,
                0f
            );

        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity =
            new Vector2(
                direction * speed,
                0f
            );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy =
                other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeHit(direction);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}