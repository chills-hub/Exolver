using UnityEngine;
public class DistanceLogging : MonoBehaviour
{
    [HideInInspector]
    public float TotalDistance = 0;
    private PlayerController Player;
    static DistanceLogging distanceLoggingInstance;

    private void Awake()
    {
        if (distanceLoggingInstance == null)
        {
            //First run, set the instance
            distanceLoggingInstance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (distanceLoggingInstance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(distanceLoggingInstance.gameObject);
            distanceLoggingInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TotalDistance = Vector2.Distance(transform.position, Player.transform.position);
        Debug.Log(TotalDistance);
    }
}
