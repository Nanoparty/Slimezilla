using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Player player;
    private TMP_Text exp;
    private TMP_Text shadow;
    private Slider slider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        exp = transform.GetChild(3).GetComponent<TMP_Text>();
        shadow = transform.GetChild(2).GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        int percentage = (int)(((float)player.getExp() / (float)player.getMaxExp()) * 100f);
        exp.text = "EXP: " + percentage.ToString() + "%";
        shadow.text = "EXP: " + percentage.ToString() + "%";
        slider.value = percentage;

    }
}
