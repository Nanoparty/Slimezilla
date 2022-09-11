using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Player player;
    float despawnDistance = 20;
    SlimeSpawner slimeSpawner;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        slimeSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SlimeSpawner>();
    }

    void Update()
    {
        if(DistanceFromPlayer(transform.position) > despawnDistance)
        {
            slimeSpawner.removeSlime();
            slimeSpawner.SpawnSingleSlime();
            Destroy(this.gameObject);
        }
    }

    float DistanceFromPlayer(Vector2 pos)
    {
        return Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - pos.x, 2)
            + Mathf.Pow(player.transform.position.y - pos.y, 2));
    }
}
