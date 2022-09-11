using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    Player player;
    float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speed = 5;
    }

    void Update()
    {
        if(DistanceFromPlayer() <= player.getMagnetRange())
        {
            Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            dir.Normalize();
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    float DistanceFromPlayer()
    {
        return Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - transform.position.x, 2)
            + Mathf.Pow(player.transform.position.y - transform.position.y, 2));
    }
}
