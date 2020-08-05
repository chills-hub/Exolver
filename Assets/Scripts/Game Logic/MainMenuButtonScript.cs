using TMPro;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour
{
    public void ChangeColorRed() 
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    }

    public void ChangeColorWhite()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
}
