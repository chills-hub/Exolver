using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static SaveGui Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }
}
