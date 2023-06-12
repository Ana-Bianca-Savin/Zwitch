using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip ZPressed, jump, buttonPressed, door, fallThrough;
    static AudioSource audioS;

    void Start()
    {
        ZPressed = Resources.Load<AudioClip>("ZPressed");
        jump = Resources.Load<AudioClip>("jump");
        buttonPressed = Resources.Load<AudioClip>("ButtonPressed");
        door = Resources.Load<AudioClip>("Door");
        fallThrough = Resources.Load<AudioClip>("FallThrough");
        audioS = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "ZPressed":
                audioS.PlayOneShot(ZPressed);
                break;
            case "jump":
                audioS.PlayOneShot(jump);
                break;
            case "ButtonPressed":
                audioS.PlayOneShot(buttonPressed);
                break;
            case "Door":
                audioS.PlayOneShot(door);
                break;
            case "FallThrough":
                audioS.PlayOneShot(fallThrough);
                break;
        }
    }
}
