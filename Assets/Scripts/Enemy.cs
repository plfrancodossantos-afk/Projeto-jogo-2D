
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 1f;

    [Header("Vida")]
    public int vida = 1;

    [Header("Empurrão")]
    public float knockback = 5f;
    public float knockbackUp = 3f;

    private Rigidbody2D rb;
    private Transform player;
    private bool morto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject p =
            GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null || morto)
            return;

        float dir = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            dir * speed,
            rb.linearVelocity.y
        );
    }

    public void TakeHit(float dir)
    {
        if (morto)
            return;

        vida--;

        Debug.Log("Zumbi atingido! Vida: " + vida);

        if (vida <= 0)
        {
            morto = true;

            Debug.Log("ZUMBI DERROTADO!");

            MenuManage gerente = MenuManage.Instance;

            if (gerente == null)
                gerente = FindFirstObjectByType<MenuManage>();

            if (gerente != null)
                gerente.RegistrarAbate();
            else
                Debug.LogError("MenuManage não encontrado!");

            Player jogador =
                FindFirstObjectByType<Player>();

            if (jogador != null)
            {
                int raiosGanhos = Random.Range(0, 6);

                jogador.GanharRaios(raiosGanhos);

                Debug.Log("Raios ganhos: " + raiosGanhos);
            }

            ZombieSpawner spawner =
                FindFirstObjectByType<ZombieSpawner>();

            Destroy(gameObject);

            if (spawner != null)
                spawner.Invoke("SpawnWave", 0.1f);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;

            rb.AddForce(
                new Vector2(dir * knockback, knockbackUp),
                ForceMode2D.Impulse
            );
        }
    }
}
