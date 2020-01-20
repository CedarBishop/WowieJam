using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public static List<Enemy> enemies = new List<Enemy>();

    public bool playerIsDead;
    private int mainMenuBuildIndex = 0;

    private int targetScene;

    Animator transitionAnimator;
    public int startingSteps;
    public int startingBullets;


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
        targetScene = SceneManager.GetActiveScene().buildIndex;
        GameMenu.instance.textLevel.text = "Level " + targetScene;
        enemies.Clear();
        transitionAnimator = GameMenu.instance.transistionImage;
        transitionAnimator.SetTrigger("Start");
        Enemy[] arr = FindObjectsOfType<Enemy>();
        foreach (Enemy item in arr)
        {
            enemies.Add(item);
        }
    }

    public void LoadNextLevel()
    {
        targetScene = SceneManager.GetActiveScene().buildIndex + 1;
        print("Load next Level into level " + targetScene);
        StartCoroutine("Transistion");
    }

    public void RestartLevel ()
    {
        targetScene = SceneManager.GetActiveScene().buildIndex;
        print("restart into level " + targetScene);
        StartCoroutine("Transistion");
    }

    public void EnemyDied (Enemy enemy)
    {
        enemies.Remove(enemy);
        
        if (enemies.Count <= 0)
        {
            StartCoroutine("CheckGameWin");         
        }

    }

    public void LoadMainMenu ()
    {
        targetScene = mainMenuBuildIndex;
        StartCoroutine("Transistion");
    }

    IEnumerator Transistion()
    {
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(targetScene);
    }
    IEnumerator CheckGameWin ()
    {
        yield return new WaitForSeconds(0.15f);

        if (playerIsDead == false)
        {
            print("Level Won");
            LoadNextLevel();
        }
    }
}
