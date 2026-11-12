using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Fase 1");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }




}
