using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject startButton;
    public PlayerMovement player;
    public Text gameOverCountdown;

    public GameObject finishGoal;

    public Text Point;
    public Image healthBar;

    public static int point = 0;

    public float countTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCountdown.gameObject.SetActive(false);
        Time.timeScale = 1;
        healthBar.gameObject.SetActive(false);
        Destroy(finishGoal);
    }
    private void Update()
    {
        if( player.isDead )
        {
            gameOverCountdown.gameObject.SetActive(true);
            countTimer -= Time.unscaledDeltaTime;
            Stop();
        }
        gameOverCountdown.text = "Restarting " + (countTimer).ToString("0");
        if(countTimer < 0)
        {
            RestartGame();
        }
        if (Time.timeScale != 0)
        {
            healthBar.gameObject.SetActive(true);
        }
        Point.text = "Point : " + point; 
    }
    public void StartGame()
    {
        startButton.SetActive(false);
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("Level1");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.UnloadSceneAsync("FinishMenu");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    private void Stop(){
        Time.timeScale = 0f;
    }
}
