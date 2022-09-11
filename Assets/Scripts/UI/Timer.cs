using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text timer;
    private TMP_Text shadow;
    private bool startTimer;
    private float startTime;
    private float totalTime;

    private int minutes;
    private int seconds;

    private EnemySpawner enemySpawner;

    void Start()
    {
        timer = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        shadow = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        minutes = 0;
        seconds = 0;

        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        if (startTimer)
        {
            int currentTime = (int)(totalTime - startTime);
            minutes = currentTime / 60;
            seconds = currentTime % 60;
            string minText = minutes > 9 ? minutes.ToString() : "0" + minutes;
            string secText = seconds > 9 ? seconds.ToString() : "0" + seconds;
            timer.text = "Time: " + minText + ":" + secText;
            shadow.text = "Time: " + minText + ":" + secText;
            enemySpawner.setEnemyMax(currentTime);
        }
    }

    public void StartTimer()
    {
        startTimer = true;
        startTime = totalTime;
    }
}
