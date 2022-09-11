using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private Player player;

    private float angle = 0;
    private float speed = 5;

    private Vector2 center;
    private float radius;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        center = player.transform.position;
        radius = 2;
    }

    private void LateUpdate()
    {
        angle += (speed * Time.deltaTime);
        Vector3 targetPos = new Vector3(
            Mathf.Cos(angle) * radius,
            Mathf.Sin(angle) * radius,
            0
         );

        transform.position = player.transform.position + targetPos;
    }

    public void SetAngle(float a)
    {
        angle = a;
    }
}
