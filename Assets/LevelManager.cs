using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public static List<Enemy> enemies = new List<Enemy>();

   
    public string[] levels;
    private string mainMenuName = "MainMenu";

    private string targetScene;

    Animator transitionAnimator;

    private int currentLevel = 0;

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
        targetScene = levels[currentLevel + 1];
        StartCoroutine("Transistion");
        currentLevel++;
    }

    public void RestartLevel ()
    {
        targetScene = SceneManager.GetActiveScene().name;
        StartCoroutine("Transistion");
    }

    public void EnemyDied (Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            print("Level Won");
            LoadNextLevel();
        }

    }

    public void LoadMainMenu ()
    {
        targetScene = mainMenuName;
        StartCoroutine("Transistion");
    }

    IEnumerator Transistion()
    {
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(targetScene);
    }
}
