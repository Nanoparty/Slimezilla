using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyParent;

    private Player player;

    int maxEnemies = 20;
    int enemyCount;

    float minDistance = 10;
    float maxDistance = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyCount = 0;

        SpawnAllEnemies();
    }

    void Update()
    {
        if(enemyCount < maxEnemies)
        {
            SpawnSingleEnemy();
        }
        
    }

    private void SpawnAllEnemies()
    {
        for(int i = 0; i < maxEnemies; i++)
        {
            SpawnSingleEnemy();
        }
    }

    public void SpawnSingleEnemy()
    {
        Vector2 spawnPoint = player.transform.position;
        float dis = DistanceFromPlayer(spawnPoint);
        while (dis < minDistance || dis > maxDistance)
        {
            spawnPoint = Random.insideUnitCircle * maxDistance;
            spawnPoint.x += player.transform.position.x;
            spawnPoint.y += player.transform.position.y;
            dis = DistanceFromPlayer(spawnPoint);
        }
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, enemyParent.transform) as GameObject;
        enemyCount++;
    }

    float DistanceFromPlayer(Vector2 pos)
    {
        return Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - pos.x, 2)
            + Mathf.Pow(player.transform.position.y - pos.y, 2));
    }

    public void removeEnemy() { enemyCount--; }
    public void addEnemyCount() { maxEnemies++; }
    public void setEnemyMax(int i) { maxEnemies = i; }
}
