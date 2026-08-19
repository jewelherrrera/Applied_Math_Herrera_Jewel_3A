using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int health;
    public int score;
    //best score is tracked
    public int highScore;

    public int killedEnemyCount;

    public GameState currentGameState;
    public UIManager uimanager;
    public EnemySpawner enemySpawner;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (health <= 0)
        {
            uimanager.ShowGameOverUI();
        }

        if (killedEnemyCount >= enemySpawner.maxEnemyToSpawn && health > 0)
        {
            uimanager.ShowGameOverUI(false);
        }
    }

    void Start()
    {
        SetGameState(GameState.Playing);
        LoadHighScore();
    }

    public void AddScore(int givenScore)
    {
        //correct amount of points is added
        score += givenScore;

        //current score is compared against high score
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            
            if (uimanager != null)
            {
                uimanager.SetHighScoreLabel();
            }
        }
    }

    public void SaveHighScore()
    {
        //high score is saved using playerprefs
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        //saved high score is loaded when the game starts
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        if (uimanager != null)
        {
            uimanager.SetHighScoreLabel();
        }


        score = 0;
        
        if (uimanager != null)
        {
            uimanager.SetScoreLabel();
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
    }
}

public enum GameState
{
    Start,
    Playing,
    GameOver
}