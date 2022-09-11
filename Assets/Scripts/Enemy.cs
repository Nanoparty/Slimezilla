using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ExpPrefab;
    public GameObject BloodSplat;

    private SpriteFlash flash;
    private EnemySpawner enemySpawner;

    private float speed;
    private float damage;
    private float health;
    private float attackRange;
    private float attackDelay;
    private bool canAttack;

    private Player player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        flash = GetComponent<SpriteFlash>();
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();

        SetDefaults();
    }

    void Update()
    {
        if (GameStateManager.gsm.state == "paused") return;

        if (DistanceFromPlayer() > attackRange)
        {
            FollowPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void SetDefaults()
    {
        speed = 1f;
        damage = 1f;
        health = 5f;
        attackRange = 0.8f;
        attackDelay = 0.5f;
        canAttack = true;
    }

    float DistanceFromPlayer()
    {
        return Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - transform.position.x, 2)
            + Mathf.Pow(player.transform.position.y - transform.position.y,2));
    }

    void FollowPlayer()
    {
        Vector2 direction = new Vector2(
            player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);
        direction.Normalize();
        float newX = transform.position.x + direction.x * speed * Time.deltaTime;
        float newY = transform.position.y + direction.y * speed * Time.deltaTime;
        transform.position = new Vector2(newX, newY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Scythe")
        {
            TakeDamage(1);
        }

    }

    void AttackPlayer()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        player.TakeDamage((int)damage);
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    public void TakeDamage(float d)
    {
        SoundManager.sm.PlayHitSound();
        health -= d;
        flash.Flash();
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject bullet = Instantiate(
            ExpPrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity) as GameObject;
        GameObject blood = Instantiate(
            BloodSplat, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity) as GameObject;
        enemySpawner.removeEnemy();
        Destroy(this.gameObject);
    }
}
