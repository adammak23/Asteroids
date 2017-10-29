using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public Text ScoreText2;
    public Text HighScoreText;
    public Text LivesText;
    public int BigAsteroidPoints = 20;
    public int MediumAsteroidPoints = 50;
    public int SmallAsteroidPoints = 100;
    public GameObject Player;
    public GameObject GameOverPanel;
    public GameObject Spawner;
    public int RespawnTime;

    int score;
    public int lives;

    void Start()
    {
        score = 0;
        ScoreText.text = "Score: " + score;

        LivesText.text = "Lives: ";
            for(int i = 0; i < lives; i++) { LivesText.text += "♥ "; }
    }

    void Update()
    {
        if (lives <= 0)
        {
            //gameover
            GameOverPanel.SetActive(true);
            ScoreText.text = "";
            LivesText.text = "";
            ScoreText2.text = "Your Score: " + score;
            int HighScore = PlayerPrefs.GetInt("highscore");
            if (score > HighScore) PlayerPrefs.SetInt("highscore", score);
            HighScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("highscore");
        }

    }

    void ScorePoints(int size)
    {
        if (size == 3) score += BigAsteroidPoints;
        if (size == 2) score += MediumAsteroidPoints;
        if (size == 1) score += SmallAsteroidPoints;

        ScoreText.text = "Score: " + score;

        Spawner.SendMessage("TryGoToNextLvl");
    }
    void Death()
    {
        lives --;
        LivesText.text = "Lives: ";
        for (int i = 0; i < lives; i++) { LivesText.text += "♥ "; }
        if (lives > 0) Player.SendMessage("Respawn",RespawnTime);
    }
}
