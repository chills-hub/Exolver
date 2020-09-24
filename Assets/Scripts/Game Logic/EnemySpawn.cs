using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public void SpawnEnemy() 
    {
        int random = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemy = Instantiate(EnemyPrefabs[random], transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyAi>().Target = FindObjectOfType<PlayerController>().transform;
    }
}
