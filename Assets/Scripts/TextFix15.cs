using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFix15 : MonoBehaviour
{
    public GameObject player;
    public GameObject text;

    void Update()
    {
        if (player.GetComponent<Movement>().GameIsPaused)
            text.SetActive(false);
        else text.SetActive(true);
    }
}
