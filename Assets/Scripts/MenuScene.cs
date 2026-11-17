
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public GameObject painelPause;
    public GameObject painelGameOver;

    private bool pausado = false;
    private bool jogoAcabou = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (painelPause != null)
            painelPause.SetActive(false);

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Continuar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        if (jogoAcabou || painelPause == null)
            return;

        pausado = true;
        painelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        pausado = false;

        if (painelPause != null)
            painelPause.SetActive(false);

        Time.timeScale = 1f;
    }

    public void MostrarGameOver()
    {
        jogoAcabou = true;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
