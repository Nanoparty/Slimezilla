using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public GameObject slimeParent;

    private Player player;

    int maxSlimes = 20;
    int slimeCount;

    float minDistance = 10;
    float maxDistance = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        SpawnSlimes();
    }

    void SpawnSlimes()
    {
        for (int i = 0; i < maxSlimes; i++)
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
            GameObject slime = Instantiate(slimePrefab, spawnPoint, Quaternion.identity) as GameObject;
            slimeCount++;
        }
    }

    public void SpawnSingleSlime()
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
        GameObject slime = Instantiate(slimePrefab, spawnPoint, Quaternion.identity, slimeParent.transform) as GameObject;
        slimeCount++;
    }

    float DistanceFromPlayer(Vector2 pos)
    {
        return Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - pos.x, 2)
            + Mathf.Pow(player.transform.position.y - pos.y, 2));
    }

    public int getSlimeCount() { return slimeCount; }
    public void removeSlime() { slimeCount--; }


}
