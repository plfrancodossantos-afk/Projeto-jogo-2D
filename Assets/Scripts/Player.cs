using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Tiro")]
    public GameObject bulletPrefab;

    public int raios = 15;
    public int maxRaios = 20;

    [Header("Contador de Raios")]
    public TextMeshProUGUI textoRaios;

    [Header("Vida")]
    public int vidas = 4;

    public GameObject coracao1;
    public GameObject coracao2;
    public GameObject coracao3;
    public GameObject coracao4;

    [Header("Dano")]
    public float puloAoLevarDano = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isGrounded = false;
    private float direction = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        vidas = 4;
        raios = 15;

        AtualizarCoracoes();
        AtualizarRaios();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            move * speed,
            rb.linearVelocity.y
        );

        if (move > 0)
        {
            sr.flipX = false;
            direction = 1f;
        }
        else if (move < 0)
        {
            sr.flipX = true;
            direction = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                0f
            );

            rb.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        if (raios <= 0)
        {
            Debug.Log("Você não tem mais raios!");
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning(
                "Bullet Prefab não foi colocado no Player!"
            );

            return;
        }

        Vector2 spawnPosition =
            (Vector2)transform.position +
            new Vector2(
                direction * 0.7f,
                0f
            );

        GameObject bala = Instantiate(
            bulletPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Bullet bulletScript =
            bala.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }

        raios--;

        AtualizarRaios();

        Debug.Log(
            "Raios restantes: " + raios
        );
    }

    public void GanharRaios(int quantidade)
    {
        raios += quantidade;

        if (raios > maxRaios)
        {
            raios = maxRaios;
        }

        AtualizarRaios();

        Debug.Log(
            "Raios: " + raios
        );
    }

    void AtualizarRaios()
    {
        if (textoRaios != null)
        {
            textoRaios.text =
                "Raios: " + raios;
        }
    }

    public void TomarDano()
    {
        if (vidas <= 0)
            return;

        vidas--;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            0f
        );

        rb.AddForce(
            Vector2.up * puloAoLevarDano,
            ForceMode2D.Impulse
        );

        AtualizarCoracoes();

        if (vidas <= 0)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }

    void AtualizarCoracoes()
    {
        if (coracao1 != null)
            coracao1.SetActive(vidas >= 1);

        if (coracao2 != null)
            coracao2.SetActive(vidas >= 2);

        if (coracao3 != null)
            coracao3.SetActive(vidas >= 3);

        if (coracao4 != null)
            coracao4.SetActive(vidas >= 4);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TomarDano();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}