using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private GameObject player;


    private bool playerIsAlive;
    private bool isGameActive = true;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if (!player.GetComponent<PlayerController>().isAlive)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }
}
