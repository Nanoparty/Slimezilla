using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button play;
    public Button credits;

    void Start()
    {
        SoundManager.sm.PlayMenuMusic();
        play.onClick.AddListener(PlayListener);
        credits.onClick.AddListener(CreditsListener);
    }

    void PlayListener()
    {
        SoundManager.sm.PlayMenuSound();
        SoundManager.sm.PlayGameMusic();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void CreditsListener()
    {
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
