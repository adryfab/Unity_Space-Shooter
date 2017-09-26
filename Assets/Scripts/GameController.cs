using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCounts;
    public float spwanWait;
    public float startWait;
    public float waveWait;
    public TextMesh scoreText;
    public TextMesh restartText;
    public TextMesh gameOverText;

    private int score;
    private bool restart;
    private bool gameOver;

    private void Start()
    {
        score = 0;
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    private void Update()
    {
        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < hazardCounts; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spwanWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver == true)
            {
                restartText.text = "Presiona R para reiniciar";
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Puntuación: " + score.ToString();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Fin del juego!";
        gameOver = true;
    }
}
