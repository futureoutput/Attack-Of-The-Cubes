using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UITitleManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerNameField;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = "High Score: " + DataManager.Instance.highScorePlayerName + ": " + DataManager.Instance.highScore;
        playerNameField.text = DataManager.Instance.currentPlayerName;


    }
    private void StartNew()
    {
        DataManager.Instance.currentPlayerName = playerNameField.text;
        SceneManager.LoadScene(1);
    }
    private void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
