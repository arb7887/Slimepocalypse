using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for changing the text of a canvas element

public class HighScore : MonoBehaviour {

    //Canvas text field for high score.
    public GameObject highScoreText;
    
	// Use this for initialization
	void Start () {
        LoadHighScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("highScore");
        }
        else
        {
            highScoreText.GetComponent<Text>().text = "High Score: 0";
        }
    }

    public void SaveHighScore(int score)
    {
        if (!PlayerPrefs.HasKey("highScore") || score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("highScore");

        }
    }
}
