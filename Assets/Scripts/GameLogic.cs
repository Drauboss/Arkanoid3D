using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject paddle;
    public GameObject gameOverScreen;
    public GameObject wonScreen;
    public GameObject brickPrefab;
    public Material[] brickMaterials;
    public TMP_Text livesText;
    public TMP_Text pointsText;

    public float lives = 3f;

    public float score = 0f;

    private float bricksLeft = 0;

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

        if (bricksLeft <= 0)
        {
            Debug.Log("You win!");
            Won();
        }

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    public void InitGame()
    {
        bricksLeft = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // Erstelle einen neuen Brick
                GameObject newBrick = Instantiate(brickPrefab, new Vector3(j - 4.5f, 17f - i, -0.5f), Quaternion.identity);
                bricksLeft++;
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
    private void Won()
    {
        wonScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        wonScreen.SetActive(false);
        lives = 3f;
        score = 0f;
        livesText.text = lives.ToString();
        pointsText.text = score.ToString();

        InitGame();

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

    public void BrickDestroyed()
    {
        Debug.Log("Brick destroyed");
        Debug.Log("Bricks left: " + bricksLeft);
        bricksLeft--;
    }

    //Region PowerUps
    public void MakePaddeBigger()
    {
        paddle.transform.localScale = new Vector3(4f, 0.5f, 1f);
        StartCoroutine(ResetPaddleSize());
    }

    public void MakePaddeSmaller()
    {
        paddle.transform.localScale = new Vector3(2f, 0.5f, 1f);
        StartCoroutine(ResetPaddleSize());
    }

    public void MakeGameSlower()
    {
        Time.timeScale = 0.5f;
        StartCoroutine(ResetGameSpeed());
    }

    public void HealthUp()
    {
        lives++;
        livesText.text = lives.ToString();
    }
    private IEnumerator ResetPaddleSize()
    {
        yield return new WaitForSeconds(10f);
        paddle.transform.localScale = new Vector3(3.0f, 0.5f, 1f);
    }

    private IEnumerator ResetGameSpeed()
    {
        yield return new WaitForSeconds(10f);
        Time.timeScale = 1f;
    }
}
