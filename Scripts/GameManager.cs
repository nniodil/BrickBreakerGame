using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
 
 public int lives;
 public int score;
 public Text livesText;
 public Text scoreText;
 public bool gameOver;
 public GameObject gameOverPanel;
 public int numberOfBricks;
 public Transform[] levels;
 public int currentLevelIndex = 0;
 public GameObject loadLevelPanel;
 public GameObject screamer;
 public GameObject endGame;
    
    void Start()
    {
        livesText.text = "Lives : " + lives;
        scoreText.text = "Score : " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }
    
    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;
       
        //Check for no lives left and trigger the end of the game
        livesText.text = "Lives : " + lives;

        if(lives <= -1)
        {
            lives = -1;
            GameOver ();
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevelIndex >= levels.Length -1)
            {
               screamer.SetActive (true);
               Destroy(screamer, 3f);
               endGame.SetActive (true);
            }
            else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke("LoadLevel",3f);
            }
            
            if(currentLevelIndex == 3)
            {
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level Final";
            }
        }
    }

    void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate (levels [currentLevelIndex],Vector2.zero,Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive (false);  
    }
    
    void GameOver ()
    {
        gameOver = true;
        gameOverPanel.SetActive (true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit ();
        Debug.Log("Game Quit");
    }     
}
