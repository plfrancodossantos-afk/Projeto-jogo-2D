
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float speed = 12f; //a velocidade do raio

    private float direction = 1f; //guard a direção do disparo
    private Rigidbody2D rb; //guarda o Rigidbody2D do raio
    private bool jaAtingiu = false; //impede que o raio acerte mais de um inimigo

    void Awake() // executa quando o raio é criado
    {
        rb = GetComponent<Rigidbody2D>(); // obtém o rigidbody2D do raio

        rb.gravityScale = 0f; // desativa a gravidade
        rb.linearDamping = 0f; // remove a resistência ao movimento
        rb.angularDamping = 0f; // remove a resistência à rotação
        rb.freezeRotation = true; // impede que o raio gire

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //melhora a detecção de colisões

        Collider2D col = GetComponent<Collider2D>(); //obtem o Collider do raio

        if (col != null) // verifica se o collider existe
            col.isTrigger = true; //permite detectar o contato com inimigos
    }

    public void SetDirection(float dir) // recebe a direção do disparo
    {
        direction = dir; //guarda a direção recebida

        rb.linearVelocity = new Vector2(direction * speed, 0f); // faz o raio se movimentar horizontalmente

        Destroy(gameObject, 5f); // destrói o raio após 5 segundos
    }

    void OnTriggerEnter2D(Collider2D other) // detecta quando o raio toca outro objeto
    {
        if (jaAtingiu) return; // impede que o raio acerte novamente

        Enemy enemy = other.GetComponentInParent<Enemy>(); // procura o script enemy no objeto atingido 

        if (enemy != null) // erifica se o objeto atingido é um zumbi
        {
            jaAtingiu = true; // marca que o raio já atingiu um inimigo

            enemy.TakeHit(direction); //aplica o impacto na direção correta

            Destroy(gameObject); // destrói o raio após atingir o zumbi
        }
    }
}
