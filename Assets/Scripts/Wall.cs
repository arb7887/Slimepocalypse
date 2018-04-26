using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Wall : MonoBehaviour {
    // wall health
    private int health = 3;

    // reference to the manager object to get access to the screenshake script
    public GameObject manager;

    // audio for the walls
    private AudioSource source;
    public AudioClip crash;
    public float deathTimer;
    public bool isDead = false;

    private void Awake()
    {
        // assign references
        manager = GameObject.Find("Manager");
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
        // runs the sound effect after the wall is dead
        if (isDead)
        {
            // create the illusion of death/destruction
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // update the timer
            deathTimer += Time.deltaTime;

            // if the timer is greater than 7 seconds
            if(deathTimer > 7.0f)
            {
                Destroy(gameObject);
            }
        }
	}

    // collision detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if a slime collides with wall
        if (collision.gameObject.tag == "Slime")
        {
            // decrement health
            health--;

            // play the sound effect
            source.PlayOneShot(crash);

            //collision.GetComponent<Slime>().manager.GetComponent<SlimeManager>().slimeList.Remove(collision.gameObject);
            SlimeManagerSingleton.Instance.slimeList.Remove(collision.gameObject);

            // Send analytics event of what type hit the wall.
            GameAnalytics.NewDesignEvent(collision.gameObject.GetComponent<Slime>().specialType, 1.0f);
            UnityEngine.Analytics.Analytics.CustomEvent(collision.gameObject.GetComponent<Slime>().specialType + " slime hit wall");

            // destroy slime
            Destroy(collision.gameObject);

            // set the duration of the screen shake to activate it
            manager.GetComponent<ScreenShake>().shakeDuration = 0.1f;
        }

        // call the method to update the visual
        updateVisual();

        // if the health of the wall reaches or falls below 0, destroy the wall
        if(health <=0)
        {
            isDead = true; // delays death for sound effect playing purposes
            //Destroy(gameObject);
        }
    }

    // method to change the sprite based on health
    private void updateVisual()
    {
        if (health == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Manager").GetComponent<WallManager>().wallImage2.GetComponent<SpriteRenderer>().sprite;
            // create a new hitbox for the new visual
            var boxCollider = GetComponent<BoxCollider2D>() as BoxCollider2D;
            boxCollider.size = new Vector2(1.28f,0.5f);
        }
        if (health == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Manager").GetComponent<WallManager>().wallImage3.GetComponent<SpriteRenderer>().sprite;
            // create a new hitbox for the new visual
            var boxCollider = GetComponent<BoxCollider2D>() as BoxCollider2D;
            boxCollider.size = new Vector2(1.28f, 0.2f);
        }
    }
}
