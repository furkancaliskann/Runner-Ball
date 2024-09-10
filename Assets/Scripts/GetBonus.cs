using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBonus : MonoBehaviour
{
    Score score;
    AudioPlayer audioPlayer;

    public GameObject bonusTextPrefab;
    public GameObject bonusCanvas;


    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Manager").GetComponent<Score>();
        audioPlayer = score.GetComponent<AudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {
            audioPlayer.PlayBonusClip();
            GameObject bonus = Instantiate(bonusTextPrefab, other.transform.position, Quaternion.Euler(0,45,0), bonusCanvas.transform);
            bonus.GetComponent<Text>().text = "+" + (score.multiplier * 10);
            Destroy(bonus, 1f);
            Destroy(other.gameObject);
            score.AddExtraScore(10);
        }

        if(other.tag == "2x")
        {
            audioPlayer.PlayBonusClip();
            GameObject bonus = Instantiate(bonusTextPrefab, other.transform.position, Quaternion.Euler(0, 45, 0), bonusCanvas.transform);
            Text text = bonus.GetComponent<Text>();
            text.fontSize = 10;
            text.text = score.NextMultiplier() + "x";
            Destroy(bonus, 1f);
            Destroy(other.gameObject);
            score.ChangeMultiplier(2);
        }
    }
}
