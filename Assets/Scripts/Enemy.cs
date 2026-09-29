using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int vida = 3;
    public float knockback = 5f;

    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    public void TakeHit(float dir)
    {
        vida--;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(dir * knockback, 3f), ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}