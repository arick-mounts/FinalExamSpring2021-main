using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static string playerName = "Default";
    public static int startLives = 4;
    public static float startTime = 60;
    public static int points = 0;
    public int lives;

    public float time;

    public Text playerText;
    public Text livesText;
    public Text pointsText;
    public Text timeText;



    // Start is called before the first frame update
    void Start()
    {
        time = startTime;
        lives = startLives;

        if (playerText != null)
        {
            playerText.text = "Currently playing: " + playerName;
            livesText.text = lives.ToString();
            pointsText.text = "0";
            timeText.text = time.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0.01)
        {
            nextScene();
        }
        if(timeText != null)
            timeText.text =  time.ToString("F2");
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void returnToMenu()
    {
        playerName = "Default";
        startLives = 4;
        startTime = 60;
        points = 0;

        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Debug.Log("Game over man!");
        Application.Quit();
    }

    public void setName(string n)
    {
        playerName = n;
    }

    public void setLives(int l)
    {
        startLives = l + 1;
    }

    public void setTime(float t)
    {
        startTime = t;
    }

    public void decreasePoints()
    {
        if (points >= 1)
        {
            points--;
            updateText();
        }
    }
    public void increasePoints()
    {
        points++;
        updateText();
    }


    public void decreaseLives()
    {
        lives--;
        if(lives < 1)
        {
            nextScene();
        }
        updateText();
        
    }

    public void increaseLives()
    {
        lives++;
        updateText();
    }

    public void updateText()
    {

        livesText.text = lives.ToString();

        pointsText.text = points.ToString();

        playerText.text = "Currently playing: " + playerName;
    }

}
