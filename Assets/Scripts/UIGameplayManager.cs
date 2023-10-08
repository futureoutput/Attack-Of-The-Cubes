using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGameplayManager : MonoBehaviour
{
    private int score;

    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private void Start()
    {
        score = DataManager.Instance.currentScore;
        UpdateScore();
    }

    private void Update()
    {
        if (DataManager.Instance.currentScore != score)
        {
            UpdateScore();
        }
    }
    public void UpdateScore()
    {
        scoreText.text = "Score:" + DataManager.Instance.currentScore;
        highScoreText.text = "High Score: " + DataManager.Instance.highScorePlayerName + ": " + DataManager.Instance.highScore;
    }
    private void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
