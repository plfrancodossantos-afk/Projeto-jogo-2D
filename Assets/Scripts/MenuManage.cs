
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public static MenuManage Instance;

    public static int inimigosDerrotados = 0;
    public static int faseAtual = 1;

    public GameObject telaProximaFase;
    public GameObject guitarra;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        EncontrarObjetos();
    }

    void Start()
    {
        EncontrarObjetos();
    }

    void EncontrarObjetos()
    {
        telaProximaFase = null;
        guitarra = null;

        string cena = SceneManager.GetActiveScene().name;

        if (cena == "Fase 1")
        {
            GameObject canvas = GameObject.Find("Canvas");

            if (canvas != null)
            {
                Transform painel = canvas.transform.Find("PanelProxFase");

                if (painel != null)
                    telaProximaFase = painel.gameObject;
            }

            if (telaProximaFase == null)
                Debug.LogError("PanelProxFase não encontrado!");
        }

        if (cena == "Fase 2")
        {
            guitarra = GameObject.Find("Guitarra");

            if (guitarra == null)
                Debug.LogError("Guitarra não encontrada!");
            else
                guitarra.SetActive(false);
        }

        Debug.Log("Cena verificada: " + cena);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        inimigosDerrotados = 0;
        faseAtual = 1;

        SceneManager.LoadScene("Fase 1");
    }

    public void ProximaFase()
    {
        Time.timeScale = 1f;
        inimigosDerrotados = 0;
        faseAtual = 2;

        SceneManager.LoadScene("Fase 2");
    }

    public void RegistrarAbate()
    {
        inimigosDerrotados++;

        Debug.Log("Zumbis derrotados: " + inimigosDerrotados);

        if (faseAtual == 1 && inimigosDerrotados >= 10)
        {
            inimigosDerrotados = 10;

            if (telaProximaFase != null)
            {
                telaProximaFase.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("FASE 1 CONCLUÍDA!");
            }
        }

        if (faseAtual == 2 && inimigosDerrotados >= 20)
        {
            inimigosDerrotados = 20;

            Debug.Log("FASE 2 CONCLUÍDA!");

            if (guitarra != null)
                guitarra.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
