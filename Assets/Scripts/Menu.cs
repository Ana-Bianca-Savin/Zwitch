using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject levels;
    public GameObject options;
    public GameObject help;

    public void Start()
    {
        main.SetActive(true);
        levels.SetActive(true);
        options.SetActive(true);
        help.SetActive(true);
        main.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
        levels.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        options.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        help.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
    }

    public void Play()
    {
        AudioManager.PlaySound("ButtonPressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        AudioManager.PlaySound("ButtonPressed");
        Application.Quit();
    }

    public void LoadLevel()
    {
        AudioManager.PlaySound("ButtonPressed");
        SceneManager.LoadScene(int.Parse(EventSystem.current.currentSelectedGameObject.name));
    }
    
    public void MainToLevel()
    {
        AudioManager.PlaySound("ButtonPressed");
        main.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        levels.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
    }

    public void LevelToMain()
    {
        AudioManager.PlaySound("ButtonPressed");
        main.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
        levels.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
    }

    public void MainToOptions()
    {
        AudioManager.PlaySound("ButtonPressed");
        main.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        options.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OptionsToMain()
    {
        AudioManager.PlaySound("ButtonPressed");
        main.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
        options.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
    }

    public void OptionsToHelp()
    {
        AudioManager.PlaySound("ButtonPressed");
        help.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
        options.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
    }

    public void HelpToOptions()
    {
        AudioManager.PlaySound("ButtonPressed");
        help.GetComponent<RectTransform>().transform.localPosition = new Vector3(1000, 1000, 0);
        options.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
    }
}
