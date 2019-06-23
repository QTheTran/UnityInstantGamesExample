using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

    public GameObject menuGroup, endButton, scoreButtonGroup;

    public int incrementAmount = 1;
    public int score;
    public Text scoreText, hiScoreText, updateScoreButtonText, adsButtonText;
    public static int hiScore;

	// Use this for initialization
	void Start () {
        score = 0;
        Application.ExternalCall("LeaderboardRetrieveScore");
	}

    public void LeaderboardScoreRetrieved(int score)
    {
        hiScore = score;
        hiScoreText.text = score.ToString();
    }

    public void StartGame()
    {
        endButton.SetActive(true);
        scoreButtonGroup.SetActive(true);
        menuGroup.SetActive(false);
    }

    public void UpdateIncrement( Text textScore )
    {
        incrementAmount = int.Parse(textScore.text);
        updateScoreButtonText.text = "+" + textScore.text;
    }

    public void UpdateScore()
    {
        score += incrementAmount;
        scoreText.text = score.ToString();
    }

    public void EndGame()
    {
        Application.ExternalCall("LeaderboardAddNewScore", score);
    }

    public void LeaderboardScoreAdded()
    {
        SceneManager.LoadScene(0);
    }

    public void AdButtonPressed()
    {
        if (adsButtonText.text == "Load Ad")
        {
            Application.ExternalCall("ReqRwd", "404917146766694_412746215983787");
        }
        else
        {
            Application.ExternalCall("ShowRwd", "404917146766694_412746215983787");
        }
    }

    public void RWDLoaded()
    {
        adsButtonText.text = "Show Ad";
    }

    public void RWDshown()
    {
        adsButtonText.text = "Load Ad";
    }
}
