using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject paddle;
    public GameObject gameOverScreen;
    public GameObject brickPrefab;
    public Material[] brickMaterials;
    public TMP_Text livesText;
    public TMP_Text pointsText;

    public float lives = 3f;

    public float score = 0f;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = lives.ToString();
        pointsText.text = score.ToString();

        InitGame();
    }

    // Update is called once per frame
    void Update()
    {


        if (lives <= 0)
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    public void InitGame()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // Erstelle einen neuen Brick
                GameObject newBrick = Instantiate(brickPrefab, new Vector3(j - 4.5f, 17f - i, -0.5f), Quaternion.identity);
            }
        }
    }

    public void LoseLife()
    {
        Debug.Log("Lost a life");
        lives--;
        livesText.text = lives.ToString();
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        lives = 3f;
        score = 0f;
        livesText.text = lives.ToString();
        pointsText.text = score.ToString();

        Time.timeScale = 1f; // Spielgeschwindigkeit zurücksetzen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Neustarten
    }


    // Methode, um Punkte hinzuzufügen
    public void AddScore(int points)
    {
        score += points;
        pointsText.text = score.ToString();
        Debug.Log("Score: " + score);  // Zeige die aktuelle Punktzahl im Log an (zum Testen)
    }
}
