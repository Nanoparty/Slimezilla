using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Player player;
    private TMP_Text hp;
    private TMP_Text shadow;
    private Slider slider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hp = transform.GetChild(3).GetComponent<TMP_Text>();
        shadow = transform.GetChild(2).GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        int percentage = (int)(((float)player.getHp() / (float)player.getMaxHp()) * 100f); 
        hp.text = "HP: " + percentage.ToString() + "%";
        shadow.text = "HP: " + percentage.ToString() + "%";
        slider.value = percentage;
    }
}
