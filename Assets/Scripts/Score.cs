using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameManager manager;
    Movement movement;
    PlayerStats playerStats;
    public Text scoreText, highScoreText;
    float score;

    public float multiplier;


    bool multiplierActive;
    float counter;
    public GameObject scoreBoostPanel;
    public Image scoreBoostImage;
    public Text scoreBoostMultiplierText;

    bool highScorePassed;

    void Start()
    {
        manager = GetComponent<GameManager>();
        movement = manager.ball.GetComponent<Movement>();
        playerStats = GetComponent<PlayerStats>();

        ResetVariables();
    }

    void Update()
    {
        if(!highScorePassed)
        {
            if(score > playerStats.highScore)
            {
                highScorePassed = true;
                scoreText.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
            }
        }
    }


    void FixedUpdate()
    {
        if (!manager.gameStarted) return;

        score += Time.fixedDeltaTime * movement.speed * multiplier;
        RefreshScoreText();

        if (multiplierActive)
        {
            if(counter > 0)
            {
                counter -= Time.fixedDeltaTime;
                UpdateScoreBoostImage();
            }
            else
            {
                multiplierActive = false;
                ResetMultiplier();
            }
        }
            
    }

    public void RefreshScoreText()
    {
        int parsedScore = (int)score;
        scoreText.text = parsedScore.ToString();
    }

    public string GetScoreString()
    {
        return ((int)score).ToString();
    }

    public int GetScoreInt()
    {
        return (int)score;
    }

    public void AddExtraScore(int amount)
    {
        score += amount * multiplier;
    }

    public int NextMultiplier()
    {
        if (multiplier * 2 >= 16) return 16;
        else return (int)multiplier * 2;
    }

    public void ChangeMultiplier(int amount)
    {
        if (multiplier * amount >= 16) multiplier = 16;
        else multiplier *= amount;
        StartMultiplierCount(10, multiplier);
    }

    void StartMultiplierCount(int counterTime, float newMultiplier)
    {
        counter = counterTime;
        scoreBoostPanel.SetActive(true);
        scoreBoostMultiplierText.text = ((int)newMultiplier).ToString() + "x";
        multiplierActive = true;   
    }

    public void ResetMultiplier()
    {
        multiplierActive = false;
        scoreBoostPanel.SetActive(false);
        multiplier = 1;
    }

    void UpdateScoreBoostImage()
    {
        scoreBoostImage.transform.localScale = new Vector3(1, counter / 10, 1);
    }

    public void ResetVariables()
    {
        highScorePassed = false;
        scoreText.color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
        score = 0;
        counter = 0;
        ResetMultiplier();
        RefreshScoreText();
        highScoreText.text = "High Score : " + playerStats.GetHighScore().ToString();
    }
}
