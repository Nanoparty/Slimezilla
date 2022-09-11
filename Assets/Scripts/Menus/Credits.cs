using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Button back;

    void Start()
    {
        back.onClick.AddListener(BackListener);   
    }

    void BackListener()
    {
        SoundManager.sm.PlayMenuSound();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
