using UnityEngine;
using TMPro;   // برای TextMeshPro

public class ScoreManager : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;           // یخ
    public TextMeshProUGUI scoreText;  // متن اسکور روی UI

    private float score = 0f;
    private bool stopped = false;      // وقتی true بشه، اسکور دیگه آپدیت نمیشه

    void Update()
    {
        if (stopped) return;                      // اگر بازی تموم شده، دیگه اسکور رو تغییر نده
        if (player == null || scoreText == null)  // اگر Referenceها ست نشده باشن، هیچی نکن
            return;

        float playerX = player.position.x;
        score = Mathf.Max(score, playerX);

        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public float GetScore()
    {
        return score;
    }

    public void StopScore()
    {
        stopped = true;
    }
}
