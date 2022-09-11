using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameStateManager.gsm.state == "paused") return;

        float newX = transform.position.x + velocity.x * speed * Time.deltaTime;
        float newY = transform.position.y + velocity.y * speed * Time.deltaTime;
        transform.position = new Vector2(newX, newY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            other.GetComponent<Enemy>().TakeDamage(1);
        }

    }

    public void SetVelocity(Vector2 v, float s)
    {
        velocity = v;
        speed = s;
    }
}
