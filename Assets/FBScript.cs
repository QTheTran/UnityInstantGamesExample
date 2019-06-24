using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class FBScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void FBInteropInit(string unityInterop, string leaderboardName);

    [DllImport("__Internal")]
    private static extern void FBInteropLeaderboardRetrieveScore();

    [DllImport("__Internal")]
    private static extern void FBInteropLeaderboardAddNewScore(int score);

    [DllImport("__Internal")]
    private static extern void FBInteropRequestReward(string idd);

    [DllImport("__Internal")]
    private static extern void FBInteropShowReward(string idd);

    public GameObject menuGroup, endButton, scoreButtonGroup;

    public int incrementAmount = 1;
    public int score;
    public Text scoreText, hiScoreText, updateScoreButtonText, adsButtonText;
    public static int hiScore;

    void Awake()
    {
        print("Awake");
        FBInteropInit(this.gameObject.name, "TopScores");
    }

    // Use this for initialization
    void Start () {
        print("Start");
        score = 0;
        FBInteropLeaderboardRetrieveScore();
        //Application.ExternalCall("LeaderboardRetrieveScore");
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
        //Application.ExternalCall("LeaderboardAddNewScore", score);
        FBInteropLeaderboardAddNewScore(score);
    }

    public void AdButtonPressed()
    {
        if (adsButtonText.text == "Load Ad")
        {
            //Application.ExternalCall("ReqRwd", "404917146766694_412746215983787");
            FBInteropRequestReward("404917146766694_412746215983787");
        }
        else
        {
            //Application.ExternalCall("ShowRwd", "404917146766694_412746215983787");
            FBInteropShowReward("404917146766694_412746215983787");
        }
    }

    public void UnityInteropLeaderboardScoreRetrieved(int score)
    {
        hiScore = score;
        hiScoreText.text = score.ToString();
    }

    public void UnityInteropLeaderboardScoreAdded()
    {
        SceneManager.LoadScene(0);
    }

    public void UnityInteropRewardLoaded()
    {
        adsButtonText.text = "Show Ad";
    }

    public void UnityInteropRewardShown()
    {
        adsButtonText.text = "Load Ad";
    }
}
