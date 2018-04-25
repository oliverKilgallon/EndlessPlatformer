using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour {

    public Text playerScore;
    public Text highScore;

    void OnEnable()
    {
        playerScore.text = "You scored: " + GameManager.instance.player.GetComponent<PlayerStats>().currentScore.ToString() + "m";
        highScore.text = "Highest scoring: " + PlayerPrefs.GetInt("highScore", 0).ToString() + "m";
    }
}
