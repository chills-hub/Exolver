using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool IsPlayerLoading = false;

    public void StartTheGame() 
    {
        SceneManager.LoadScene("Main_Hub");
    }

    public void StartTheGameWithLoad() 
    {
        IsPlayerLoading = true;
        SceneManager.LoadScene("Main_Hub");
    }
}
