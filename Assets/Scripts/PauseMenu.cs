using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f); 
    }

    public void OpenPauseMenu()
    {
        AudioManager.PlaySound("ButtonPressed");
        GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
        player.GetComponent<Movement>().GameIsPaused = true;
    }

    public void ClosePauseMenu()
    {
        AudioManager.PlaySound("ButtonPressed");
        GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        player.GetComponent<Movement>().GameIsPaused = false;
    }

    public void GoToMainMenu()
    {
        AudioManager.PlaySound("ButtonPressed");
        SceneManager.LoadScene(0);
    }
}
