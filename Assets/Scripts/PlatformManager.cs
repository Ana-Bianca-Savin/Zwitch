using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject pink;
    public GameObject blue;
    public GameObject pinkOutline;
    public GameObject blueOutline;

    public bool pressedZ;
    public GameObject player;

    private void Awake()
    {
        pink.SetActive(false);
        blueOutline.SetActive(false);
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !player.GetComponent<Movement>().GameIsPaused)
            pressedZ = true;
    }

    void FixedUpdate()
    {
        if (pressedZ)
        {
            AudioManager.PlaySound("ZPressed");
            pink.SetActive(!pink.activeSelf);
            blue.SetActive(!blue.activeSelf);

            pinkOutline.SetActive(!pink.activeSelf);
            blueOutline.SetActive(!blue.activeSelf);
            pressedZ = false;
        }
    }
}
