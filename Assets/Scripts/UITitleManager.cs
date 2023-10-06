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
    public TMP_InputField playerNameField;


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
