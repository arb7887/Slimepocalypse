using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary for changing the text of a canvas element

public class KillCounter : MonoBehaviour {

    private int killCount = 0; // Counts how many slimes have been killed
    private bool fifthShot = false; // Tells if this is a super slime
    private int score = 0; // Score int if we want to use it later.
    public GameObject killCountText; // the actual text being displayed on the canvas

    public enum GameState { Menu, Game, End};
    public GameState gameState = GameState.Menu;
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

    private static KillCounter _instance;

    public static KillCounter instance
    {
        get {

            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<KillCounter>();

                DontDestroyOnLoad(_instance.gameObject);

                _instance.killCount = 0;
                _instance.fifthShot = false;
                _instance.score = 0;
            }

            return _instance;
        }
    }

    private void Awake()
    {
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
    public void resetSupershotImages()
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
        superShotImage = GameObject.FindGameObjectWithTag("SuperShotUI");
        superShotChargeSpriteHolder[0] = noChargeSprite;
        superShotChargeSpriteHolder[1] = oneChargeSprite;
        superShotChargeSpriteHolder[2] = twoChargeSprite;
        superShotChargeSpriteHolder[3] = threeChargeSprite;
        superShotChargeSpriteHolder[4] = fourChargeSprite;
        superShotChargeSpriteHolder[5] = fullChargeSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // only while the game is running
        if(gameState == GameState.Game)
        {
            // increment the timer each frame
            timer += Time.deltaTime;

            // convert the timer to minutes and seconds
            minutes = Mathf.FloorToInt(timer / 60F);
            seconds = Mathf.FloorToInt(timer - minutes * 60);

            // nice formatting for timer
            niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            // update the canvas text
            timeText.GetComponent<Text>().text = "Time: " + niceTime;
        }
    }

    // Adds to the kill count.
    public void AddKillToCount()
    {
        // Add to the killCount
        killCount++;
        //Debug.Log(killCount);
        score++;

        // Replace the supershot charge asset on the circle with a more filled asset
        if(killCount > 5)
        {
            // If killCount is for whatever reason above 5
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[5];

        } else
        {
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[killCount];
        }

        // update the text
        killCountText.GetComponent<Text>().text = "Slimes Killed: " + score;
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
    public bool IsSuperShot()
    {
        if (killCount >= 5)
        {
            superShotImage.GetComponent<Image>().sprite = superShotChargeSpriteHolder[0];     // Resets the sprite for supershot count back to its original state
            fifthShot = true;
            killCount = 0;
        }
        else fifthShot = false;

        return fifthShot;
    }

}
