
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public float speed = 1f; //define a velocidade do zumbi
    public int vida = 1; // define a quantidade de tiros necessários para derrotá-lo
    public float knockback = 5f; // define a força do empurrão horizontal
    public float knockbackUp = 3f; // define a força do empurrão vertical

    private Rigidbody2D rb; // guarda o rigidbody2D do zumbi
    private Transform player; // guarda a posição do jogador
    private bool morto = false; // impede que um zumbi derrotado continue agindo

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>(); // obtém o rigidbody2D do zumbi

        GameObject p = GameObject.FindGameObjectWithTag("Player"); // procura o jogador pela tag player

        if (p != null) //ver se encontrou o jogador
            player = p.transform; //guarda a posição do jogador
    }

    void FixedUpdate() 
    {
        if (player == null || morto) return; //interrompe se não houver jogador ou se o zumbi morreu

        float dir = Mathf.Sign(player.position.x - transform.position.x); //descobre para qual lado o zumbi deve andar

        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y); // move o zumbi na direção do jogador
    }

    public void TakeHit(float dir) // executa quando o zumbi recebe um tiro
    {
        if (morto) return; // n deixa o zumbi morto receber outro impacto

        vida--; // diminui um ponto de vida

        Debug.Log("Vida do zumbi: " + vida); //mostra a vida restante no console

        if (vida <= 0) // verifica se o zumbi foi derrotado
        {
            morto = true; //marca o zumbi como morto

            Debug.Log("ZUMBI DERROTADO!"); //avisa que o zumbi foi derrotado

            MenuManage gerente = MenuManage.Instance; // obtem o gerenciador do jogo

            if (gerente == null) gerente = FindFirstObjectByType<MenuManage>(); // procura o gerenciador caso a instância não esteja disponível

            if (gerente != null) gerente.RegistrarAbate(); // registra a derrota na contagem
            else Debug.LogError("MenuManage não encontrado!"); // avisa se o gerenciador não foi encontrado

            Player jogador = FindFirstObjectByType<Player>(); // procura o jogador na cena

            if (jogador != null) // ve se encontrou o jogador
            {
                int raiosGanhos = Random.Range(0, 6); // sorteia uma quantidade de raio entre 0 e 5 raios

                jogador.GanharRaios(raiosGanhos); // da os raios ao jogador

                Debug.Log("Raios ganhos: " + raiosGanhos); // mostra a recompensa no console.
            }

            ZombieSpawner spawner = FindFirstObjectByType<ZombieSpawner>(); // procura o sistema de surgimento de zumbis

            Destroy(gameObject); // Remove o zumbi derrotado.

            if (spawner != null) spawner.Invoke("SpawnWave", 0.1f); // solicita uma nova onda de zumbis após 0,1 segundo
        }
        else // executa se o zumbi ainda estiver vivo
        {
            rb.linearVelocity = Vector2.zero; // interrompe o movimento atual

            rb.AddForce(new Vector2(dir * knockback, knockbackUp), ForceMode2D.Impulse); // aplica o empurrao
        }
    }
}
