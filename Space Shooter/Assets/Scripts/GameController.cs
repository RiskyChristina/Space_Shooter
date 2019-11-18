using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    public Text WinText;
    private bool win;

    void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        WinText.text = "";
        StartCoroutine(SpawnWaves());
        score = 0;
        UpdateScore();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("Space Shooter"); ;
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        { 
            for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
            yield return new WaitForSeconds (waveWait);

            if (gameOver || win)
            {
                RestartText.text = "Press 'S' to Start Over";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            WinText.text = "You win! Game created by Christina Leskowyak";
            win = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        if (win == true)
        {
            GameOverText.text = "";
            gameOver = true;
        }
        else
        {
            GameOverText.text = "Game Over!";
            gameOver = true;
        }
    }
}
