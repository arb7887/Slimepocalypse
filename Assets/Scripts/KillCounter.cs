using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for changing the text of a canvas element

public class KillCounter : MonoBehaviour {

    private int killCount = 0; // Counts how many slimes have been killed
    private int killCountSpriteNum = 0; // Number representing the image that will be used for mana
    private bool beserkState = false; // Variable that says whether we're in the beserk state currently
    private bool readyToBeserk = true; // Variable that only lets the beserk timer start once
    private float beserkCount = 0; // Amount of time left in the current beserk state
    private bool fifteenthKill = false; // Tells if this is a super slime
    public int score = 0; // Score int if we want to use it later.
    public int currentScore; // the actual in game score
    public GameObject inGameScoreText;
    public GameObject killCountText; // the actual text being displayed on the canvas

    public float timer = 0.0f; // actual time
    public int seconds = 0; // time in seconds
    public int minutes = 0; // time in minutes
    public GameObject timeText; // actual text of the time being displayed on the canvas
    private string niceTime = "";

    private GameObject superShotImage;// A reference to the supershot image

    public Sprite noChargeSprite; // Sprite to show the user that they have no charge for their supershot
    public Sprite oneChargeSprite; // Sprite to show the user that they have 1 charge for their supershot
    public Sprite twoChargeSprite; // Sprite to show the user that they have 2 charge for their supershot
    public Sprite threeChargeSprite; // Sprite to show the user that they have 3 charge for their supershot
    public Sprite fourChargeSprite; // Sprite to show the user that they have 4 charge for their supershot
    public Sprite fullChargeSprite; // Sprite to show the user that they have full charge for their supershot
    private Sprite[] superShotChargeSpriteHolder = new Sprite[6]; // The array that holds the supershot sprites

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
    }

    // Update is called once per frame
    void Update()
    {
        // Count down the beserk state
        if (beserkState)
        {
            beserkCount -= Time.deltaTime;

            if (beserkCount <= 0)
            {
                ResetBeserkStateVariables();
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
        inGameScoreText.GetComponent<Text>().text = "Score: " + currentScore;
        
    }

    // Resets the beserk state variables when it runs out
    public void ResetBeserkStateVariables()
    {
        beserkState = false;
        readyToBeserk = true;
        killCount = 0;
        killCountSpriteNum = 0;
        superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[0];     // Resets the sprite for supershot count back to its original state
    }

    // Tells the game to start the beserk state
    public void BeginBeserkState()
    {
        // Check if this is the first click
        if (readyToBeserk == true)
        {
            beserkState = true;
            beserkCount = 5; // Makes the beserk state last 5 seconds
            readyToBeserk = false;
        }
    }

    // Tells the Flickshot class to start using normal shots again
    public bool CheckBeserkState()
    {
        return beserkState;
    }

    // Adds to the kill count.
    public void AddKillToCount(bool correctType)
    {
        if(correctType) //Only adds to the Super Shot count if the Slime is killed with the correct element type
        {
            // if gaining a mana charge
            if(killCount <= 3)
            {
                // play a mana gaining sound effect
                source.PlayOneShot(manaCharge);
            }
            // otherwise the supershot is ready
            else if(killCount >= 4)
            {
                // play the ready sound effect
                source.PlayOneShot(superShotReadySound,2.0f);
            }
        }

        // Don't add to beserkState mana while you're in beserkState
        if(!beserkState)
        {
            // Add to the killCount
            killCount++;
        }
        //Debug.Log(killCount);
        score++;

        // Replace the supershot charge asset on the circle with a more filled asset
        if (killCount > 15)
        {
            // If killCount is for whatever reason above 5
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[5];

        } else
        {
            // Only change the sprite every 3 kills
            if (killCount % 3 == 0 && killCount > 0)
            {
                killCountSpriteNum++;
            }
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[killCountSpriteNum];
        }


        // update the text
        killCountText.GetComponent<Text>().text = "Slimes Killed: " + score;

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
        killCountText.GetComponent<Text>().text = "Slimes Killed: " + score;
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

    // If the kill count is greater than 5, the next shot will be a supershot.
    public bool IsBeserkState()
    {
        if (killCount >= 15)
        {
            fifteenthKill = true;
            killCount = 0;
            killCountSpriteNum = 0;
        }
        else fifteenthKill = false;

        return fifteenthKill;
    }

}
