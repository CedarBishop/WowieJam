using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public GameObject Menu;
    private bool isOn = false;

    private void Start()
    {
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private void Update()
    {
        if (!isOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0.0001f;
                Menu.SetActive(true);
                isOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                Menu.SetActive(false);
                isOn = false;
            }
        }
    }

}
