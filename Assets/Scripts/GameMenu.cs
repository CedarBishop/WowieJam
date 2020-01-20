using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance = null;
    public Text attackCountText;
    public Text stepCountText;
    public Animator transistionImage;
    private Player player;
    public TextMeshProUGUI textLevel;
    public Text restartText;
    public Slider musicSlider;
    public Slider sfxSlider;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        restartText.gameObject.SetActive(false);
        
    }

    public void UpdateUI()
    {
        int attackCount = player.attackCount;
        int stepCount = player.stepCount;
        attackCountText.text = attackCount.ToString();
        stepCountText.text = stepCount.ToString();
        if (attackCount <= 0 && stepCount <= 0)
        {
            restartText.gameObject.SetActive(true);
        }
    }

    public void MainMenuButtinClick()
    {
        LevelManager.instance.LoadMainMenu();
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        SoundManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        SoundManager.instance.SetSFXVolume(sfxSlider.value);
    }
}
