using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

    public GameObject startButton, endButton, scoreButtonGroup;

    public int incrementAmount = 1;
    public int score;
    public Text scoreText, hiScoreText, updateScoreButtonText;
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
        startButton.SetActive(false);
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
}
