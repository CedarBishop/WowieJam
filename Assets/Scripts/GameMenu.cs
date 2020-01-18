using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance = null;
    public Text attackCountText;
    public Text stepCountText;
    public Animator transistionImage;
    private Player player;



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
        UpdateUI();
    }

    public void UpdateUI()
    {
        attackCountText.text = player.attackCount.ToString();
        stepCountText.text = player.stepCount.ToString();
    }
}
