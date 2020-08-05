using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenuButton : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public TextMeshProUGUI Title;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToOptions() 
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        Title.gameObject.SetActive(false);
    }

    public void GoToMenu() 
    {
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        Title.gameObject.SetActive(true);
    }

    public void GoToCredits()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
        Title.gameObject.SetActive(false);
    }
}
