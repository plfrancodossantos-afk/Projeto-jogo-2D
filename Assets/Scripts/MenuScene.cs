
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class MenuScene : MonoBehaviour 
{
    public GameObject painelPause; //guarda o painel de pausa
    public GameObject painelGameOver; // guarda o painel de derrota

    private bool pausado = false; // avisa se o jogo está pausado
    private bool jogoAcabou = false; // avisa se o jogador perdeu

    void Start()
    {
        Time.timeScale = 1f; // garante que o jogo comece sem estar pausado

        if (painelPause != null) // verifica se o painel de pausa foi associado
            painelPause.SetActive(false); // esconde o painel de pausa.

        if (painelGameOver != null) // verifica se o painel de Game Over foi associado
            painelGameOver.SetActive(false); // esconde o painel de derrota
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //ve se o jogador apertou ESC
        {
            if (pausado) // ve se o jogo já está pausado
                Continuar(); // retoma o jogo
            else // executa se o jogo não estiver pausado
                Pausar(); // abre o menu de pausa
        }
    }

    public void Pausar() // funçao chamada para pausar o jogo
    {
        if (jogoAcabou || painelPause == null) //verifica se o jogo acabou ou se não existe painel
            return; // interrompe a função

        pausado = true; // marca o jogo como pausado
        painelPause.SetActive(true); // mostra o painel de pausa
        Time.timeScale = 0f; // deixa o jogo travado
    }

    public void Continuar() // funçao chamada para continuar jogando
    {
        pausado = false; // marca o jogo como não pausado

        if (painelPause != null) // ve se o painel existe
            painelPause.SetActive(false); //esconde o painel de pausa

        Time.timeScale = 1f; // retoma o tempo normal do jogo
    }

    public void MostrarGameOver() // funçao chamada quando o jogador perde
    {
        jogoAcabou = true; // fala que o jogo terminou

        if (painelGameOver != null) // ve se o painel existe
            painelGameOver.SetActive(true); // mostra a tela de game over

        Time.timeScale = 0f; // pausa o jogo
    }

    public void Reiniciar() // funçao chamada pelo botão de reiniciar
    {
        Time.timeScale = 1f; // despausa o jogo.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recarrega a cena atual
    }

    public void VoltarMenu() // funçao chamada pelo botão de voltar ao menu
    {
        Time.timeScale = 1f; //despausa o jogo
        SceneManager.LoadScene("Menu"); // carrega a cena Menu
    }
}
