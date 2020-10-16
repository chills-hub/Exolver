using UnityEngine;
public class DistanceLogging : MonoBehaviour
{
    [HideInInspector]
    public float TotalDistance = 0;
    private PlayerController Player;
 
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TotalDistance = Vector2.Distance(transform.position, Player.transform.position);
        //Debug.Log(TotalDistance);
    }
}
