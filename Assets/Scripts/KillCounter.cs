using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for changing the text of a canvas element
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour {

    private int killCount = 0; // Counts how many slimes have been killed
    public int killCountSpriteNum = 0; // Number representing the image that will be used for mana
    private bool berserkState = false; // Variable that says whether we're in the berserk state currently
    private bool readyToberserk = true; // Variable that only lets the berserk timer start once
    private bool berserkStarted = false;
    private float berserkCount = 0; // Amount of time left in the current berserk state
    private bool fifteenthKill = false; // Tells if this is a super slime
    public int score = 0; // Score int if we want to use it later.
    public int currentScore; // the actual in game score
    public bool surpassedHighScore;
    public bool newHighScoreAlert;
    private float alertTimer = 0.0f;
    public GameObject inGameScoreText;
    public GameObject newHighScoreAlertText;
    //public GameObject killCountText; // the actual text being displayed on the canvas                          
    public GameObject highScoreText; //Canvas text field for high score.

    public float timer = 0.0f; // actual time
    public int seconds = 0; // time in seconds
    public int minutes = 0; // time in minutes
    public GameObject timeText; // actual text of the time being displayed on the canvas
    private string niceTime = "";

    public GameObject superShotImage;// A reference to the supershot image

    public Sprite noChargeSprite; // Sprite to show the user that they have no charge for their supershot
    public Sprite oneChargeSprite; // Sprite to show the user that they have 1 charge for their supershot
    public Sprite twoChargeSprite; // Sprite to show the user that they have 2 charge for their supershot
    public Sprite threeChargeSprite; // Sprite to show the user that they have 3 charge for their supershot
    public Sprite fourChargeSprite; // Sprite to show the user that they have 4 charge for their supershot
    public Sprite fullChargeSprite; // Sprite to show the user that they have full charge for their supershot
    public Sprite[] superShotChargeSpriteHolder = new Sprite[6]; // The array that holds the supershot sprites

    // audio variables
    private AudioSource source;
    public AudioClip manaCharge;
    public AudioClip superShotReadySound;

    private static KillCounter _instance;

    public static KillCounter instance
    {
        get {

            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<KillCounter>();

                DontDestroyOnLoad(_instance.gameObject);

                _instance.killCount = 0;
                _instance.fifteenthKill = false;
                _instance.score = 0;
                _instance.currentScore = 0;
                _instance.surpassedHighScore = false;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>(); // set the reference to the audio source component
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Resets the reference to the supershot UI
    public void ResetSupershotImages()
    {
        superShotImage = GameObject.FindGameObjectWithTag("SuperShotUI");
        superShotChargeSpriteHolder[0] = noChargeSprite;
        superShotChargeSpriteHolder[1] = oneChargeSprite;
        superShotChargeSpriteHolder[2] = twoChargeSprite;
        superShotChargeSpriteHolder[3] = threeChargeSprite;
        superShotChargeSpriteHolder[4] = fourChargeSprite;
        superShotChargeSpriteHolder[5] = fullChargeSprite;
    }

    private void Start()
    {
        // Setup the supershot UI
        ResetSupershotImages();

        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we are in the main game scene
        if (SceneCheck() == 1)
        {
            // Count down the berserk state
            if (berserkState)
            {
                berserkCount -= Time.deltaTime;

                if (berserkCount <= 0)
                {
                    ResetberserkStateVariables();
                }
            }

            // increment the timer each frame
            timer += Time.deltaTime;

            // convert the timer to minutes and seconds
            minutes = Mathf.FloorToInt(timer / 60F);
            seconds = Mathf.FloorToInt(timer - minutes * 60);

            // nice formatting for timer
            niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            // update the canvas text for timer and score
            timeText.GetComponent<Text>().text = "Time: " + niceTime;

            // keep checking to see if the high score has been passed
            if(!PlayerPrefs.HasKey("highScore") || currentScore > PlayerPrefs.GetInt("highScore"))
            {
                surpassedHighScore = true;
                newHighScoreAlert = true;
            }

            // if the player has surpassed the current high score
            if (surpassedHighScore)
            {
                inGameScoreText.GetComponent<Text>().color = Color.yellow;
            }
            if (newHighScoreAlert)
            {
                alertTimer += Time.deltaTime;
                newHighScoreAlertText.GetComponent<Text>().enabled = true;
                if(alertTimer >= 3.0f)
                {
                    newHighScoreAlertText.GetComponent<Text>().enabled = false;
                    newHighScoreAlert = false;
                }
            }
            inGameScoreText.GetComponent<Text>().text = "Score: " + currentScore;
        }    
    }

    // Resets the berserk state variables when it runs out
    public void ResetberserkStateVariables()
    {
        berserkState = false;
        readyToberserk = true;
        killCount = 0;
        killCountSpriteNum = 0;
        superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[0];     // Resets the sprite for supershot count back to its original state
    }

    // Tells the game to start the berserk state
    public void BeginBerserkState()
    {
        // Check if this is the first click
        if (readyToberserk == true)
        {
            berserkState = true;
            berserkCount = 5; // Makes the berserk state last 5 seconds
            readyToberserk = false;
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[5];
        }
    }

    // Getter for the killCount variable
    public int CheckKillCount()
    {
        return killCount;
    }

    // Adds to the kill count.
    public void AddKillToCount(bool correctType)
    {
        if(correctType) //Only adds to the Super Shot count if the Slime is killed with the correct element type
        {
            // if gaining a mana charge
            if(killCountSpriteNum <= 4)
            {
                // play a mana gaining sound effect
                source.PlayOneShot(manaCharge);
            }
            // otherwise the supershot is ready
            else if(killCountSpriteNum == 5)
            {
                // play the ready sound effect
                source.PlayOneShot(superShotReadySound,2.0f);
            }
        }

        // Don't add to berserkState mana while you're in berserkState
        if(!berserkState)
        {
            // Add to the killCount
            killCount++;
        }
        //Debug.Log(killCount);
        score++;

        // Replace the supershot charge asset on the circle with a more filled asset
        // Only change the sprite every 3 kills
        if (killCount % 3 == 0 && killCount > 0)
        {
            killCountSpriteNum++;
        }
        if(killCountSpriteNum < 5)
        {
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[killCountSpriteNum];
        }


        // update the text
       // killCountText.GetComponent<Text>().text = "Slimes Killed: " + score;

        // update the score (100 on kill so far)
        currentScore += 100;
    }
    
    // Sets the score
    public void SetScore(int newScore)
    {
        score = newScore;
    }

    // Add to the score
    public void AddToScore()
    {
        score++;

        // update the text
       // killCountText.GetComponent<Text>().text = "Slimes Killed: " + score;
    }

    // Sets the KillCount
    public void SetKillCount(int newKillCount)
    {
        killCount = newKillCount;
        killCountSpriteNum = 0;
    }

    public void SetTimer(float newTimer)
    {
        timer = newTimer;
    }

    // Gets the score
    public int GetScore()
    {
        return score;
    }

    // Gets the kill count.
    public int GetKillCount()
    {
        return killCount;
    }

    public void SetCurrentScore(int newScore)
    {
        currentScore = newScore;
    }

    // Loading and setting the player's high score in the text bubble
    public void LoadHighScore()
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

    //Saving the high score if there is a new high score.
    public void SaveHighScore(int score)
    {
        if (!PlayerPrefs.HasKey("highScore") || score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("highScore");
        }
    }

    // Getter for berserkState variable
    public bool IsBerserkState()
    {
        return berserkState;
    }

    public int SceneCheck()
    {
        //If we are in the main game scene
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (inGameScoreText == null)
            {
                inGameScoreText = GameObject.Find("Score");
            }
            if (timeText == null)
            {
                timeText = GameObject.Find("TimeText");
            }
            if (superShotImage == null)
            {
                superShotImage = GameObject.Find("SuperShotUI");
            }
            return 1;
        }
        else
        {
            currentScore = 0;
            return 2;
        }
    }

    public void Reset()
    {
        SetScore(0);
        SetTimer(0.0f);
        SetKillCount(0);
        ResetberserkStateVariables();
        SaveHighScore(currentScore);
        LoadHighScore();
        SetCurrentScore(0);
        ResetSupershotImages();
        highScoreText = GameObject.Find("HighScoreText");
        newHighScoreAlertText = GameObject.Find("NewHighScoreAlert");
        surpassedHighScore = false;
    }
}
