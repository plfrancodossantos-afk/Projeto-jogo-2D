
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject painelPause;
    public GameObject painelGameOver;

    private bool pausado = false;
    private bool jogoAcabou = false;

    void Start()
    {
        Time.timeScale = 1f;

        pausado = false;
        jogoAcabou = false;

        if (painelPause != null)
            painelPause.SetActive(false);

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoAcabou)
                return;

            if (pausado)
                Continuar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        if (jogoAcabou)
            return;

        pausado = true;

        if (painelPause != null)
            painelPause.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        if (jogoAcabou)
            return;

        pausado = false;

        if (painelPause != null)
            painelPause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void MostrarGameOver()
    {
        if (jogoAcabou)
            return;

        jogoAcabou = true;
        pausado = false;

        if (painelPause != null)
            painelPause.SetActive(false);

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
