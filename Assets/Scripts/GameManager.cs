using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Score score;
    Movement movement;
    PlatformGenerator generator;
    Biome biome;
    PlayerStats playerStats;
    AudioPlayer audioPlayer;
    CameraScript cameraScript;

    public GameObject startCanvas, gameCanvas, endCanvas;
    public GameObject ball;
    
    public bool gameStarted;
    bool isFirstGame;

    public Text endScoreText;


    void Start()
    {
        score = GetComponent<Score>();
        movement = ball.GetComponent<Movement>();
        generator = GetComponent<PlatformGenerator>();
        biome = GetComponent<Biome>();
        playerStats = GetComponent<PlayerStats>();
        audioPlayer = GetComponent<AudioPlayer>();
        cameraScript = Camera.main.GetComponent<CameraScript>();

        Application.targetFrameRate = 120;

        isFirstGame = true;
        OpenCanvas(startCanvas);
    }

    
    void Update()
    {
        if(!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
            
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        if (EventSystem.current.currentSelectedGameObject.tag != "Button")
                        {
                            StartGame();
                        }
                    }
                    else
                    {
                        StartGame();
                    }
                    
                }

                   
            }
        }

        if(ball.transform.position.y < -2 && gameStarted)
        {
            FinishGame();
        }
        
    }

    void OpenCanvas(GameObject canvas)
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        endCanvas.SetActive(false);

        canvas.SetActive(true);
    }

    void StartGame()
    {
        OpenCanvas(gameCanvas);

        score.ResetVariables();

        if(isFirstGame)
        {
            isFirstGame = false;
        }
        else
        {
            generator.ResetVariables();
            biome.ResetVariables();
        }

        cameraScript.ResetVariables();

        movement.ResetVariables(); // sonda olmalý

        gameStarted = true;

    }

    void FinishGame()
    {
        audioPlayer.PlayGameOverClip();
        gameStarted = false;
        biome.StopChangePlatformColor();

        ball.SetActive(false);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        int finishScore = score.GetScoreInt();

        playerStats.SendScoreToGooglePlay(finishScore);

        if(playerStats.IsNewHighScore(finishScore))
        {
            endScoreText.text = "New High Score! \n - " + score.GetScoreString() + " -";
        }
        else
        {
            endScoreText.text = "Score : " + score.GetScoreString();
        }

        
        OpenCanvas(endCanvas);
    }
}
