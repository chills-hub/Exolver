using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] LevelBlocks;
    public GameObject StartingPoint;
    private Vector3 LastPosition;
    private Vector3 NextPosition;
    private const float Player_Distance_Spawn_Blocks = 100f;
    private int StartingSpawnBlocks = 1;
    private PlayerController Player;

    private void Awake()
    {
        //randomise block from array, delete when certain distance behind player or when fog covers it
        Player = FindObjectOfType<PlayerController>();
        LastPosition = StartingPoint.transform.position;
        Instantiate(LevelBlocks[0], LastPosition, Quaternion.identity);
        SpawnBlockAhead(LastPosition);

        for (int i = 0; i < StartingSpawnBlocks; i++)
        {
            SpawnBlockAhead(LastPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, LastPosition) < Player_Distance_Spawn_Blocks) 
        {
            SpawnBlockAhead(LastPosition);
        }
    }

    private void SpawnBlockAhead(Vector3 position) 
    {
        NextPosition = position + new Vector3(40, 0, 0);
        Instantiate(LevelBlocks[0], NextPosition, Quaternion.identity);
        LastPosition = NextPosition;
    }
}
