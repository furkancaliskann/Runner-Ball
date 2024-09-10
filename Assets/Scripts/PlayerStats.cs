using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayerStats : MonoBehaviour
{
    public int highScore;
    public bool connectedToGooglePlay;

    void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void Start()
    {
        LogInToGooglePlay();
        GetHighScore();
    }

    void LogInToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            connectedToGooglePlay = true;
        }
        else connectedToGooglePlay = false;
    }

    public void ShowLeaderBoard()
    {
        if (!connectedToGooglePlay) LogInToGooglePlay();
        Social.ShowLeaderboardUI();
    }

    public void SendScoreToGooglePlay(int score)
    {
        if (connectedToGooglePlay) 
        {
            Social.ReportScore(score, GPGSIds.leaderboard_runner_ball, LeaderboardUpdate);
        }
    }

    void LeaderboardUpdate(bool success)
    {
        if (success) Debug.Log("Updated Leaderboard!");
        else Debug.Log("Unable to update Leaderboard!");
    }

    public bool IsNewHighScore(int score)
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            return true;
        }
        else return false;  
    }

    public int GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        return highScore;
    }
}
