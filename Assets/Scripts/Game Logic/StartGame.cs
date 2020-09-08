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
        StartCoroutine(LoadSceneAsync(2));
       // Scene sceneToLoad = SceneManager.GetSceneByName("Gameplay");
       // SceneManager.SetActiveScene(sceneToLoad);
       
        //SceneManager.UnloadSceneAsync("Main_Hub");
    }

    private IEnumerator LoadSceneAsync(int sceneIndex) 
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(sceneIndex);
        while (!loadLevel.isDone) 
        {
            Debug.Log(loadLevel.progress);
            yield return null;

        }
    }
}
