using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Animation settings")]
    public Ease easeType;
    public float duration;
    public RectTransform gameoverRect;

    public TextMeshProUGUI gameOverLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highScoreLabel;
    public TextMeshProUGUI healthLabel;

    public Button restartBtn;



    private void Start()
    {

        SetHealthLabel();

        restartBtn.onClick.AddListener(GameManager.Instance.RestartGame);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowGameOverUI();
        }

        SetScoreLabel();
    
    }

  public void SetHealthLabel()
    {
        healthLabel.text = $"{GameManager.Instance.health}";
    }

    public void SetScoreLabel()
    {
        scoreLabel.text = $"{GameManager.Instance.score}";
    }

    public void SetHighScoreLabel()
    {
        highScoreLabel.text = $"{GameManager.Instance.highScore}";
    }

  

    public void ShowGameOverUI(bool isGameOver = true)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(gameoverRect.DOScale(Vector3.one,duration).SetEase(easeType));
        gameOverLabel.text = isGameOver ? "GAME OVER" : "GAME WON";
     
        SetHighScoreLabel();

    }

    public void HideGameOverUI()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(gameoverRect.DOScale(Vector3.zero, duration).SetEase(easeType));
    }
}
