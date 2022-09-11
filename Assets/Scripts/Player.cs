using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Stats")]
    public float movementSpeed;

    [Header("Combat Prefabs")]
    public GameObject bulletPrefab;
    public GameObject scythePrefab;
    private List<GameObject> bullets;
    private float fireDelay;
    private bool canShoot;
    private float bulletSpeed;
    private int numBullets;
    private List<GameObject> scythes;
    private Vector2 fireDirection;


    [Header("Background Object")]
    public GameObject ground;

    private Timer timer;
    private SpriteFlash flash;
    private SlimeSpawner slimeSpawner;
    private UpgradeManager upgradeManager;

    private Camera camera;
    private float cameraSize;

    private int hp;
    private int maxHp;
    private int exp;
    private int maxExp;
    private int slime;
    private int maxSlime;

    private float magnetRange;

    private float size;
    private int level;

    private int books = 0;
    private int juices = 0;
    private int orbs = 0;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        timer.StartTimer();

        flash = GetComponent<SpriteFlash>();
        slimeSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SlimeSpawner>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeManager>();
        camera = GetComponentInChildren<Camera>();

        SetDefaultValues();

        SoundManager.sm.PlayGameMusic();
    }

    void Update()
    {
        if (GameStateManager.gsm.state == "paused") return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(movementSpeed * horizontal, movementSpeed * vertical);
        movement *= Time.deltaTime;

        transform.Translate(movement);
        ground.transform.Translate(movement);

        if(movement.magnitude > 0)
        {
            fireDirection = new Vector2(horizontal, vertical);
            fireDirection.Normalize();
        }

        LevelUp();

        FireBullet(fireDirection);

        DebugCommands();

        Grow();
    }

    //private void LateUpdate()
    //{
    //    positionOffset.Set(
    //        Mathf.Cos(angle) * CircleRadius,
    //        ElevationOffset,
    //        Mathf.Sin(angle) * CircleRadius
    //    );
    //    transform.position = Target.position + positionOffset;
    //    angle += Time.deltaTime * RotationSpeed;
    //}

    void SetDefaultValues()
    {
        hp = 100;
        maxHp = 100;
        exp = 0;
        maxExp = 100;
        slime = 0;
        maxSlime = 10;
        level = 1;
        size = 1f;

        movementSpeed = 3;

        magnetRange = 1;

        bullets = new List<GameObject>();
        scythes = new List<GameObject>();
        canShoot = true;
        bulletSpeed = 5f;
        fireDelay = 0.1f;
        numBullets = 2;

        cameraSize = camera.orthographicSize;
    }

    void DebugCommands()
    {
        if (Input.GetKeyDown("t"))
        {
            size+=0.1f;
            transform.localScale = new Vector3(size, size, 1);
            cameraSize += 0.1f;
            camera.orthographicSize = cameraSize;
        }
        if (Input.GetKeyDown("u"))
        {
            addSythe();
        }
    }

    public void SpawnScythes()
    {
        foreach(GameObject scythe in scythes)
        {
            Destroy(scythe);
        }
        scythes.Clear();

        float angle = 0;
        float increment = (Mathf.PI * 2) / books;
        for(int i = 0; i < books; i++)
        {
            InstantiateScythe(angle);
            angle += increment;
        }
    }

    void Grow()
    {
        if(slime >= maxSlime)
        {
            size += 0.5f;
            transform.localScale = new Vector3(size, size, 1);
            cameraSize += 0.1f;
            camera.orthographicSize = cameraSize;
            slime -= maxSlime;
            maxSlime += 5;
            maxHp += 20;
            hp = maxHp;
        }
    }

    void FireBullet(Vector2 dir)
    {
        if (canShoot)
        {
            StartCoroutine(FireRate(dir));
        }
        
        //if (Input.GetButton("Fire1") && canShoot)
        //{

        //}
    }

    IEnumerator FireRate(Vector2 dir)
    {
        canShoot = false;
        for (int i = 0; i < numBullets + juices; i++)
        {
            InstantiateProjectile(dir);
        }  
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    void InstantiateScythe(float angle)
    {
        GameObject scythe = Instantiate(
            scythePrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity) as GameObject;
        scythe.GetComponent<Scythe>().SetAngle(angle);
        scythes.Add(scythe);
    }

    void InstantiateProjectile(Vector2 bulletVelocity)
    {
        //Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 bulletVelocity = new Vector2(pz.x - transform.position.x, pz.y - transform.position.y);
        //bulletVelocity.Normalize();

        GameObject bullet = Instantiate(
            bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity) as GameObject;
        bullets.Add(bullet);
        
        float angle = Mathf.Atan2(bulletVelocity.y, bulletVelocity.x) * Mathf.Rad2Deg;
        angle += Random.Range(-15f, 15f);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.transform.localScale = new Vector3(size, size, 0);
        //bullet.GetComponent<Bullet>().SetVelocity(bulletVelocity, bulletSpeed);
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        bullet.GetComponent<Bullet>().SetVelocity(dir, bulletSpeed);
    }

    void LevelUp()
    {
        if(exp > maxExp)
        {
            level++;
            exp -= maxExp;
            maxExp = maxExp + (int)(maxExp * 1.2);
            GameStateManager.gsm.state = "paused";
            upgradeManager.Upgrade();
        }
    }

    public void TakeDamage(int d)
    {
        hp -= d;
        flash.Flash();
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GameOver");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exp")
        {
            exp += 20;
            Destroy(other.gameObject);
            SoundManager.sm.PlayGemSound();
        }
        if(other.gameObject.tag == "Slime")
        {
            slime += 1;
            Destroy(other.gameObject);
            slimeSpawner.SpawnSingleSlime();
        }
        
    }

    public int getHp() { return hp; }
    public int getMaxHp() { return maxHp; }
    public int getExp() { return exp; }
    public int getMaxExp() { return maxExp; }
    public int getSlime() { return slime; }
    public int getMaxSlime() { return maxSlime; }
    public void addSythe() {
        books++;
        SpawnScythes();
    }
    public void addJuice() { juices++; }
    public void addOrb() { orbs++; }
    public float getMagnetRange() { return magnetRange + orbs; }
}
