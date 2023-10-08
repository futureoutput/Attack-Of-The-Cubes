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

    public GameObject[] enemyPrefabs;

    private int waveNumber;
    public float waveDelay = 10f;
    private int maxEnemyIndexLength;

    public GameObject spawner;
    public float spawnVariance = 10f;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.isGameActive = true;
        maxEnemyIndexLength = enemyPrefabs.Length;
        waveNumber = 0;
        StartCoroutine(WaveTimer());  
    }

    IEnumerator WaveTimer()
    {
        waveNumber++;
        int numberOfEnemys = 1 + waveNumber / 4;
        int enemyDiff = waveNumber / 4;
        SpawnEnemyWave(numberOfEnemys, enemyDiff);
        yield return new WaitForSeconds(waveDelay);
        if (isGameActive)
        {
            StartCoroutine(WaveTimer());
        }
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

    // ABSTRACTION
    void GameOver()
    {
        isGameActive = false;
        DataManager.Instance.isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        if (DataManager.Instance.currentScore> DataManager.Instance.highScore)
        {
            DataManager.Instance.highScore = DataManager.Instance.currentScore;
            DataManager.Instance.highScorePlayerName = DataManager.Instance.currentPlayerName;
            DataManager.Instance.SaveHighScore();
        }
        
    }

    // ABSTRACTION
    void SpawnEnemyWave(int enemyCount, int difficulty) {
        int i = 0;
        int enemyIndex;

        //ensure prefab array stays within bounds
        int maxIndex = difficulty;
        if (maxEnemyIndexLength < difficulty)
        {
            maxIndex = maxEnemyIndexLength;
        }

        while (i < enemyCount)
        {
            enemyIndex = Random.Range(0, maxIndex);
            i = i + enemyIndex + 1;
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), Random.rotation);
        }
        
    }

    // ABSTRACTION
    Vector3 GenerateSpawnPosition()
    {
        Vector3 position = spawner.transform.position;
        position.x = position.x + Random.Range(-spawnVariance, spawnVariance);
        //position.y = position.y + Random.Range(-spawnVariance, spawnVariance);
        position.z = position.z + Random.Range(-spawnVariance, spawnVariance);
        return position;
    }
}
