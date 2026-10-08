using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 1f;

    [Header("Vida")]
    public int vida = 3;

    [Header("Empurrão")]
    public float knockback = 5f;
    public float knockbackUp = 3f;

    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject p =
            GameObject.FindGameObjectWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        float dir =
            Mathf.Sign(
                player.position.x -
                transform.position.x
            );

        rb.linearVelocity = new Vector2(
            dir * speed,
            rb.linearVelocity.y
        );
    }

    public void TakeHit(float dir)
    {
        vida--;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            new Vector2(
                dir * knockback,
                knockbackUp
            ),
            ForceMode2D.Impulse
        );

        if (vida <= 0)
        {
            Player jogador =
                FindFirstObjectByType<Player>();

            if (jogador != null)
            {
                // Sorteia de 0 até 5 raios
                int raiosGanhos =
                    Random.Range(0, 6);

                jogador.GanharRaios(
                    raiosGanhos
                );

                Debug.Log(
                    "Zumbi morto! Você ganhou " +
                    raiosGanhos +
                    " raios."
                );
            }

            ZombieSpawner spawner =
                FindFirstObjectByType<ZombieSpawner>();

            Destroy(gameObject);

            if (spawner != null)
            {
                spawner.Invoke(
                    "SpawnWave",
                    0.1f
                );
            }
        }
    }
}