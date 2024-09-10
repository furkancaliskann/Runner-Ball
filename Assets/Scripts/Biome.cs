using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    PlatformGenerator generator;
    GameManager manager;
    Score score;
    Color currentBiomeColor;



    void Start()
    {
        generator = GetComponent<PlatformGenerator>();
        manager = GetComponent<GameManager>();
        score = GetComponent<Score>(); 

        ResetVariables();
    }

    void ChangePlatformColor()
    {
        currentBiomeColor = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f);

        generator.ChangeColorOfAllPlatforms(currentBiomeColor);
    }

    public Color GetCurrentBiomeColor()
    {
        return currentBiomeColor;
    }

    public void StopChangePlatformColor()
    {
        CancelInvoke(nameof(ChangePlatformColor));
    }

    public void ResetVariables()
    {
        ChangePlatformColor();
        InvokeRepeating(nameof(ChangePlatformColor), 10f, 10f);
    }
}
