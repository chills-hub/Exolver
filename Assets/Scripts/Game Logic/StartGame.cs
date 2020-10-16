using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool IsPlayerLoading = false;

    //static StartGame startGameInstance;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //    if (startGameInstance == null)
    //    {
    //        //First run, set the instance
    //        startGameInstance = this;

    //    }
    //    else if (startGameInstance != this)
    //    {
    //        //Instance is not the same as the one we have, destroy old one, and reset to newest one
    //        //Destroy(startGameInstance.gameObject);
    //        startGameInstance = this;
    //    }
    //}


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

    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }
}
