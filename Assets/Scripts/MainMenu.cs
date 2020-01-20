using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName = "Level1";

  //  public Animator transistionAnimation;
    public AudioSource musicAudioSource;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
      //  transistionAnimation.SetTrigger("Start");
    }

    public void PlayGame()
    {
       // transistionAnimation.SetTrigger("End");
        StartCoroutine("DelayPlayGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayPlayGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(firstLevelName);
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume",musicSlider.value);
        musicAudioSource.volume = musicSlider.value;
    }

    public void SetSFXVolume ()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
