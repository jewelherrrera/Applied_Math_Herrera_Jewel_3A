using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int health;
    public int score;
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

        score += givenScore;

    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
    }
    public void LoadHighScore()
    {
       score = PlayerPrefs.GetInt("HighScore");
       uimanager.SetScoreLabel();
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
