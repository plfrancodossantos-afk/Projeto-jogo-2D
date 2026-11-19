
using UnityEngine;
using TMPro; 

public class Player : MonoBehaviour 
{
    public float speed = 5f; //a velocidade de movimento
    public float jumpForce = 7f; //força do pulo

    public GameObject bulletPrefab; //guarda o prefab do raio disparado

    public int raios = 15; //a quantidade inicial de raios
    public int maxRaios = 20; //o limite máximo de raios

    public TextMeshProUGUI textoRaios; //o texto q mostra a quantidade de raios

    public int vidas = 4; //define a quantidade de vidas do jogador

    public GameObject coracao1; //primeira imagem de coração
    public GameObject coracao2; //segunda imagem de coração
    public GameObject coracao3; //terceira imagem de coração
    public GameObject coracao4; // quarta imagem de coração

    public float puloAoLevarDano = 5f; //força do pulo ao receber dano

    public MenuScene menuCena; //controlador da tela de Game Over

    private Rigidbody2D rb; // Guarda o Rigidbody2D do jogador
    private SpriteRenderer sr; // Guarda o componente responsável pela imagem do jogador

    private bool isGrounded = false; //fala se o jogador está no chão
    private float direction = 1f; // Guarda a direção para a qual o jogador está olhando
    private bool morreu = false; // player n pode fazer nada  depois q morre

    void Start() // Executa quando o jogador aparece na cena
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do jogador
        sr = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer do jogador

        vidas = 4; // Reinicia as vidas
        raios = 15; // Reinicia a quantidade de raios

        AtualizarCoracoes(); // atualiza as imagens dos corações
        AtualizarRaios(); // atualiza o contador de raios
    }

    void Update()
    {
        if (morreu) // vê se o jogador já morreu
            return; // impede que ele continue se movimentando ou atirando

        float move = Input.GetAxisRaw("Horizontal"); // lê os comandos de movimento horizontal

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y); // Move o jogador sem alterar sua velocidade vertical

        if (move > 0) // vê se o jogador está andando para a direita
        {
            sr.flipX = false; // mantém o personagem olhando para a direita
            direction = 1f; // define a direção do disparo para a direita
        }
        else if (move < 0) // verifica se o jogador está andando para a esquerda
        {
            sr.flipX = true; // vai vira o personagem para a esquerda
            direction = -1f; // faz o disparo ir para a esquerda
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // verifica se apertou Espaço estando no chão
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // zera a velocidade vertical antes do pulo

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // aplica a força do pulo
        }

        if (Input.GetKeyDown(KeyCode.E)) // verifica se apertou a tecla E
        {
            Atirar(); // executa o disparo
        }
    }

    void Atirar() // controla o disparo dos raios
    {
        if (morreu || raios <= 0) //verifica se o jogador morreu ou está sem raios
        {
            Debug.Log("Você não tem mais raios!"); // vai avisar no console quando os raios acabar
            return; // n vai deixar o player atirar mais
        }

        if (bulletPrefab == null) // verifica se o prefab do raio foi associado
        {
            Debug.LogWarning("Bullet Prefab não foi colocado no Player!"); // mostra um aviso no Console
            return; // Cancela o disparo
        }

        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(direction * 0.7f, 0f); // calcula onde o raio deve aparecer

        GameObject bala = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity); // cria o raio na posição calculada

        Bullet bulletScript = bala.GetComponent<Bullet>(); // obtém o script Bullet do raio criado

        if (bulletScript != null) // verifica se o raio possui o script
        {
            bulletScript.SetDirection(direction); //define para qual lado o raio vai
        }

        raios--; //diminui um raio do contador

        AtualizarRaios(); // atualiza o contador de raios

        Debug.Log("Raios restantes: " + raios); // mostra a quantidade restante no Console
    }

    public void GanharRaios(int quantidade) // recebe raios como recompensa
    {
        if (morreu) // verifica se o jogador morreu
            return; // impede do jogador receber os raios dps q morre

        raios += quantidade; // adiciona os raios recebidos

        if (raios > maxRaios) // verifica se ultrapassou o limite
        {
            raios = maxRaios; // limita a quantidade ao máximo permitido
        }

        AtualizarRaios(); // atualiza o contador de raios na tela

        Debug.Log("Raios: " + raios); // mostra a quantidade atual no Console
    }

    void AtualizarRaios() // atualiza o texto dos raios
    {
        if (textoRaios == null) // verifica se o texto foi associado
            return; // interrompe se não encontrar o texto

        textoRaios.gameObject.SetActive(true); // garante que o contador esteja visível
        textoRaios.text = "Raios: " + raios; // mostra a quantidade atual de raios
    }

    public void TomarDano() //executa quando o jogador recebe um ataque
    {
        if (vidas <= 0 || morreu) //verifica se já está sem vidas ou morreu
            return; //impede receber dano novamente

        vidas--; //diminui uma vida

        AtualizarCoracoes(); //atualiza as imagens dos corações

        if (vidas <= 0) //verifica se todas as vidas acabaram
        {
            morreu = true; //marca o jogador como morto

            rb.linearVelocity = Vector2.zero; //interrompe o movimento do jogador

            if (menuCena != null) //verifica se o controlador de menu foi associado
            {
                menuCena.MostrarGameOver(); //mostra a tela de derrota
            }
            else //executa se o controlador não foi encontrado
            {
                Debug.LogError("MenuCena não foi associado ao Player!"); //mostra um erro no console
            }

            return; //encerra a função.
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); //zera a velocidade vertical antes do impulso

        rb.AddForce(Vector2.up * puloAoLevarDano, ForceMode2D.Impulse); //faz o jogador pular ao receber dano
    }

    void AtualizarCoracoes() //atualiza os corações conforme as vidas restantes
    {
        if (coracao1 != null) //verifica se o primeiro coração existe
            coracao1.SetActive(vidas >= 1); //mostra o primeiro coração se houver pelo menos uma vida

        if (coracao2 != null) //verifica se o segundo coração existe
            coracao2.SetActive(vidas >= 2); //mostra o segundo coração se houver pelo menos duas vidas

        if (coracao3 != null) // verifica se o terceiro coração existe
            coracao3.SetActive(vidas >= 3); //mostra o terceiro coração se houver pelo menos três vidas

        if (coracao4 != null) //verifica se o quarto coração existe
            coracao4.SetActive(vidas >= 4); // mostra o quarto coração se houver quatro vidas
    }

    void OnCollisionStay2D(Collision2D collision) // ve rifica os objetos que continuam em contato com o jogador
    {
        if (collision.gameObject.CompareTag("Ground")) // verifica se o jogador está tocando o chão
        {
            isGrounded = true; // permite que o jogador pule
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // detecta quando o jogador encosta em outro objeto
    {
        if (collision.gameObject.CompareTag("Enemy")) // verifica se encostou em um zumbi
        {
            TomarDano(); // retira uma vida do jogador
        }
    }

    void OnCollisionExit2D(Collision2D collision) // detecta quando o jogador deixa de tocar em um objeto
    {
        if (collision.gameObject.CompareTag("Ground")) // verifica se saiu do chão
        {
            isGrounded = false; // impede que o jogador pule no ar
        }
    }
}
