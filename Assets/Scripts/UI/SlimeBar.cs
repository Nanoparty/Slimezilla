using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBar : MonoBehaviour
{
    private Player player;
    private TMP_Text slime;
    private TMP_Text shadow;
    private Slider slider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        slime = transform.GetChild(3).GetComponent<TMP_Text>();
        shadow = transform.GetChild(2).GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        int percentage = (int)(((float)player.getSlime() / (float)player.getMaxSlime()) * 100f);
        slime.text = "SLIME: " + percentage.ToString() + "%";
        shadow.text = "SLIME: " + percentage.ToString() + "%";
        slider.value = percentage;
    }
}
