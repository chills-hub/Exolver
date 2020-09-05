using System.Collections;
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

    public void EnterGameplay() 
    {
        SceneManager.LoadScene("Gameplay");
        Scene sceneToLoad = SceneManager.GetSceneByName("Gameplay");
        SceneManager.SetActiveScene(sceneToLoad);
        //SceneManager.UnloadSceneAsync("Main_Hub");
       // SceneManager.MoveGameObjectToScene(FindObjectOfType<PlayerController>().gameObject, sceneToLoad);
       // SceneManager.MoveGameObjectToScene(FindObjectOfType<GameManager>().gameObject, sceneToLoad);
    }
}
