using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject ChestPopup;
    public GameObject ChestAnimation;
    public GameObject ChestButton;

    public GameObject ItemsPopup;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;

    private int item1Count = 0;
    private int item2Count = 0;
    private int item3Count = 0;

    private Animator chestAnimator;

    private float transitionDelay = 0.5f;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ChestButton.GetComponent<Button>().onClick.AddListener(OpenListener);

        chestAnimator = ChestAnimation.GetComponent<Animator>();

        ChestPopup.SetActive(false);
        ItemsPopup.SetActive(false);

        Item1.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(Item1Listener);
        Item2.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(Item2Listener);
        Item3.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(Item3Listener);
    }

    public void Upgrade()
    {
        ChestPopup.SetActive(true);
        Item1.transform.GetChild(1).GetComponent<TMP_Text>().text = "Slimoire +" + (item1Count + 1);
        Item2.transform.GetChild(1).GetComponent<TMP_Text>().text = "Royal Jelly +" + (item2Count + 1);
        Item3.transform.GetChild(1).GetComponent<TMP_Text>().text = "Crystal Ball +" + (item3Count + 1);
    }

    void OpenListener()
    {
        chestAnimator.Play("Open");
        StartCoroutine(OpenItems());
        SoundManager.sm.PlayChestSound();
    }

    IEnumerator OpenItems()
    {
        yield return new WaitForSeconds(transitionDelay);
        ChestPopup.SetActive(false);
        ItemsPopup.SetActive(true);
    }

    void Item1Listener()
    {
        item1Count++;
        player.addSythe();
        CloseUpgrade();
        SoundManager.sm.PlayBookSound();
    }

    void Item2Listener()
    {
        item2Count++;
        player.addJuice();
        CloseUpgrade();
        SoundManager.sm.PlayPotionSound();
    }

    void Item3Listener()
    {
        item3Count++;
        player.addOrb();
        CloseUpgrade();
        
    }

    void CloseUpgrade()
    {
        ItemsPopup.SetActive(false);
        GameStateManager.gsm.state = "playing";
    }
}
