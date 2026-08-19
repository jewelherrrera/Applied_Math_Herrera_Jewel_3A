using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyToSpawn;
    public float interval;
    private float timer;
    private int currentEnemyCount;

    [SerializeField] private List<Transform> spawnPoints;

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Playing)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= interval && currentEnemyCount < maxEnemyToSpawn )
        {
            GameObject enemy = Instantiate(enemyPrefab,Vector3.zero,Quaternion.identity);
            enemy.transform.position = GetSpawnPoint().position;
            Enemy currentEnemy = enemy.gameObject.GetComponent<Enemy>();
            currentEnemy.isMoving = true;
            timer = 0;
            currentEnemyCount++;
        }
    }

    private Transform GetSpawnPoint()
    {
        int index = Random.Range(0,spawnPoints.Count);
        Transform selectedSpawnPoint = spawnPoints[index];
        return selectedSpawnPoint;
    }

}
