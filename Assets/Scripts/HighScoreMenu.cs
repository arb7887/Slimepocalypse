using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour {


    // Main Menu text field for high scores
    public Text highScoreText;

    // Use this to make sure we don't repeat ourselves when adding a new score to the high score list
    private bool haveNotReorganizedYet = true;

    // Use this for initialization
    void Start () {
        CreateHighScorePositionDictionaries();
    }
	
    // Create all 10 high score position dictionaries
    private void CreateHighScorePositionDictionaries()
    {
        for (int i = 1; i < 11; ++i)
        {
            if (!PlayerPrefs.HasKey("highScore" + i))
            {
                PlayerPrefs.SetInt("highScore" + i, 0);
                highScoreText.text += "\n" + "High Score: 0";
            }
            else
            {
                highScoreText.text += "\n" + "High Score: " + PlayerPrefs.GetInt("highScore" + i);
            }
        }
    }

    // Checks if this method is being called too many times, and prevents repeats when this is the case
    public void CheckAndUpdateHighScoreList(int newScore)
    {

        bool repeat = false;

        for (int i = 1; i < 11; ++i)
        {
            if (newScore == PlayerPrefs.GetInt("highScore" + i))
            {
                repeat = true;
            }
        }

        if(repeat == false)
        {
            UpdateHighScoreList(newScore);
        }


    }

    // Update the high score list in the main menu, if we have a score that beats a previous score.
    private void UpdateHighScoreList(int newScore)
    {


            highScoreText.text = "";


        // A for loop to replace the first beaten score with this new score
        for (int i = 1; i < 11; ++i)
        {
            // Check if the new score is greater than the current 
            if (newScore > PlayerPrefs.GetInt("highScore" + i) && haveNotReorganizedYet)
            {
                AddNewHighScore(i, newScore);
            }
        }

        // A for loop to replace the first beaten score with this new score
        for (int i = 1; i < 11; ++i)
        {
            highScoreText.text += "\n" + "High Score: " + PlayerPrefs.GetInt("highScore" + i);
        }

        // Reset its value when its done
        haveNotReorganizedYet = true;
    }

    // Add a new high score to the high score menu list
    public void AddNewHighScore(int index, int score)
    {


        // Order matters here, make sure to lower all of the lower scores first before updating the score in this index.
        ReplaceNextValue(index);

        PlayerPrefs.SetInt("highScore" + index, score);

        // Assign this to false so we don't repeatedly change all lower scores to the same score
        haveNotReorganizedYet = false;
    }

    // Replace the value of the next index with the value of the current index
    private void ReplaceNextValue(int startingIndex)
    {

        if(PlayerPrefs.HasKey("highScore" + (startingIndex + 1)))
        {
            int highScoreHolder = PlayerPrefs.GetInt("highScore" + (startingIndex + 1));

            PlayerPrefs.SetInt("highScore" + (startingIndex + 1), PlayerPrefs.GetInt("highScore" + startingIndex));

            ReorganizeHighScoreList(startingIndex + 1, highScoreHolder);
        }
    }

    // Reorganize the high score list with the new score at the correct position
    private void ReorganizeHighScoreList(int startingIndex, int currentHighScoreHolder)
    {

        if (PlayerPrefs.HasKey("highScore" + (startingIndex + 1)))
        {
            int nextHighScoreHolder = PlayerPrefs.GetInt("highScore" + (startingIndex + 1));

            PlayerPrefs.SetInt("highScore" + (startingIndex + 1), currentHighScoreHolder);


            ReorganizeHighScoreList(startingIndex + 1, nextHighScoreHolder);
        }
    }

}


