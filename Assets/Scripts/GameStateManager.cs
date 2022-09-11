using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager gsm;

    public GameObject cursorPrefab;
    public GameObject cursor;
    public string state;

    private void Awake()
    {
        if (gsm == null)
        {
            gsm = this;
        }
        else if (gsm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

        void Start()
    {
        //Cursor.visible = false;
        //cursor = Instantiate(cursorPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        state = "playing";
    }

    void Update()
    {
        //Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pz.z = 0;
        //cursor.transform.position = pz;

    }
}
