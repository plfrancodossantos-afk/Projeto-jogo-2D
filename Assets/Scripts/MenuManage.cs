
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour 
{
    public static MenuManage Instance; // permite que outros scripts encontrem o gerenciador

    public static int inimigosDerrotados = 0; // guarda a quantidade de zumbis derrotados
    public static int faseAtual = 1; // guarda o número da fase atual
    public static bool fase2Concluida = false; // impede que a vitória seja carregada mais de uma vez

    public GameObject telaProximaFase; // guarda o painel de conclusão da primeira fase

    void Awake() // executa quando o objeto é carregado
    {
        Instance = this; // define este objeto como o gerenciador atual
    }

    void Start() // executa quando o jogo começa nesta cena
    {
        EncontrarObjetos(); // procura os objetos necessários da cena
    }

    void EncontrarObjetos() //procura o painel da próxima fase
    {
        telaProximaFase = null; //limpa a referência anterior do painel

        string cena = SceneManager.GetActiveScene().name; // descobre o nome da cena atual

        if (cena == "Fase 1") // ve se o jogador está na primeira fase
        {
            GameObject canvas = GameObject.Find("Canvas"); // procura o canvas da cena

            if (canvas != null) // ve se encontrou o canvas
            {
                Transform painel = canvas.transform.Find("PanelProxFase"); // procura o painel dentro do canvas

                if (painel != null) // ve se encontrou o painel
                    telaProximaFase = painel.gameObject; // Guarda o painel encontrado
            }

            if (telaProximaFase == null) // ve se o painel não foi encontrado
                Debug.LogError("PanelProxFase não encontrado!"); // mostra um erro no console
        }

        Debug.Log("Cena verificada: " + cena); // mostra o nome da cena no console
    }

    public void StartGame() // executa quando o jogador aperta "começar"
    {
        Time.timeScale = 1f; // garante que o jogo não esteja pausado

        inimigosDerrotados = 0; // reinicia a contagem de inimigos
        faseAtual = 1; // define a primeira fase como atual
        fase2Concluida = false; // libera uma nova conclusão da segunda fase

        SceneManager.LoadScene("Fase 1"); // carrega a primeira fase
    }

    public void ProximaFase() // executa quando o jogador aperta "continuar"
    {
        Time.timeScale = 1f; // despausa o jogo

        inimigosDerrotados = 0; // reinicia a contagem para a segunda fase
        faseAtual = 2; // define a segunda fase como atual

        SceneManager.LoadScene("Fase 2"); // carrega a segunda fase
    }

    public void RegistrarAbate() // registra quando um zumbi é derrotado
    {
        if (fase2Concluida) // ve se a segunda fase já foi concluída
            return; // impede novos registros após a vitória

        inimigosDerrotados++; // aumenta a quantidade de zumbis derrotados

        Debug.Log("Zumbis derrotados: " + inimigosDerrotados); //mostra a contagem no console

        if (faseAtual == 1 && inimigosDerrotados >= 10) // ve se 10 zumbis foram derrotados na primeira fase
        {
            inimigosDerrotados = 10; // bota o limite de conatgem ate 10

            if (telaProximaFase != null) // ve se o painel foi encontrado
            {
                telaProximaFase.SetActive(true); // mostra o painel da próxima fase
                Time.timeScale = 0f; // pausa o jogo

                Debug.Log("FASE 1 CONCLUÍDA!"); // informa que a primeira fase terminou
            }
            else // executa se o painel não foi encontrado
            {
                Debug.LogError("Painel da próxima fase não encontrado!"); // mostra um erro no console.
            }
        }

        if (faseAtual == 2 && inimigosDerrotados >= 20) // ve se 20 zumbis foram derrotados na segunda fase
        {
            inimigosDerrotados = 20; // limita a contagem a 20
            fase2Concluida = true; // marca a segunda fase como concluída

            Debug.Log("FASE 2 CONCLUÍDA!"); // informa que a segunda fase terminou

            Time.timeScale = 1f; // garante que o jogo esteja despausado
            SceneManager.LoadScene("Vitoria"); // carrega a cena de vitória
        }
    }

    public void VoltarMenu() // executa quando o jogador aperta o botão Menu
    {
        Time.timeScale = 1f; // despausa o jogo
        SceneManager.LoadScene("Menu"); // retorna para o menu
    }

    public void QuitGame() // executa quando o jogador aperta "sair"
    {
        Time.timeScale = 1f; // despausa o jogo
        Application.Quit(); // fecha o jogo quando estiver executando
    }
}
