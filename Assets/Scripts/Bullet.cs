
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;

    private float direction = 1f;
    private Rigidbody2D rb;
    private bool jaAtingiu = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.freezeRotation = true;

        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;

        Collider2D meuCollider =
            GetComponent<Collider2D>();

        if (meuCollider != null)
            meuCollider.isTrigger = true;
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        rb.linearVelocity = new Vector2(
            direction * speed,
            0f
        );

        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(
                direction * speed,
                0f
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (jaAtingiu)
            return;

        if (other.CompareTag("Enemy"))
        {
            jaAtingiu = true;

            Enemy enemy =
                other.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                // Um único tiro elimina o zumbi.
                enemy.TakeHit(1000f);
            }

            Destroy(gameObject);
        }
    }
}
