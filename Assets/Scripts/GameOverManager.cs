using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;   // پنل Game Over

    [Header("Score System")]
    public ScoreManager scoreManager;  // امتیازدهی بازی
    public TextMeshProUGUI gameOverScoreText;  // نمایش امتیاز نهایی

    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // توقف اسکور
        if (scoreManager != null)
            scoreManager.StopScore();

        // مقدار واقعی امتیاز
        float finalScore = scoreManager.GetScore();

        // نمایش داخل متن Game Over
        if (gameOverScoreText != null)
            gameOverScoreText.text = "Score: " + Mathf.FloorToInt(finalScore);

        // نمایش پنل
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // توقف بازی
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
