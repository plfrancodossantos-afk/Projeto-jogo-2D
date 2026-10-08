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

        // O raio não deve ser destruído imediatamente
        // ao nascer dentro de outro collider.
        Collider2D meuCollider =
            GetComponent<Collider2D>();

        if (meuCollider != null)
        {
            meuCollider.isTrigger = true;
        }
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        rb.linearVelocity = new Vector2(
            direction * speed,
            0f
        );

        // O raio só desaparece depois de 5 segundos
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
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
    }
}