using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    public int health = 2; // number of hits until a slime dies
    //private string slimeType = "fire"; // what element type of slime it is

    public float slimeSpeed, dashSpeed; //Speed of normal movement, and dashing movement, which will be higher than normal movement.
    public string type; //Determines collision detection for different ammo types.
    public List<Sprite> spriteList; //List of sprites for different slime types.
    public int numMoveTypes;
    public enum MoveTypes { Normal, Dashing, LaneSwap};
    public List<MoveTypes> activeMovetypes;
    public float slideTimer, laneTimer; //Timer variables for storing time lengths between dashes/lane swaping
    public float dashTimer, delayTimer; //2 random numbers to determine length of slime's dash, and delay before next dash. Randomized for sporatic movement. Used solely for vertical dashing.
    public float laneSwitchTimer; //Constant for slime switching lanes. No randomization of time between lane switching.
    public int currentLane; //Storing the current lane of the slime.
    public int moveTo; //Lane that the slime with move towards.
    public bool reachedLane; //Boolean to see if the slime has reached/exceeded the target lane.
    public bool shaking; // Boolean to see if the slime is shaking
    public AudioClip hitSound; // sound to play when damage is taken
    public AudioClip deathSound; // sound to play when slime is killed
    private float volume = 1.0f; // how loud to play the audio
    private AudioSource source; // how the audio gets played

    // runs before start
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        setInitialSize();
        slimeSpeed = 0.01f;
        dashSpeed = slimeSpeed * 3.0f;
        dashTimer = Random.Range(0.3f, 0.7f);
        delayTimer = Random.Range(1.2f, 1.5f);
        laneTimer = 0.0f;
        reachedLane = true;
        float random = Random.Range(0.0f, 1.0f);
        //Randomly set the type and sprite of the slime.
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Moving the slimes every frame
        for (int i = 0; i < activeMovetypes.Count; i++)
        {
            if (activeMovetypes[i] == MoveTypes.Normal)
            {
                MoveSlime();
            }
            else if (activeMovetypes[i] == MoveTypes.Dashing)
            {
                Dash();
            }
            else if (activeMovetypes[i] == MoveTypes.LaneSwap)
            {
                LaneSwap();
            }
        }
	}

    public void SetType(int newType)
    {
        if (newType == 0)
        {
            type = "ice";
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        }
        else
        {
            type = "fire";
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[1];
        }
    }

    // simple takedamage
    public virtual void TakeDamage()
    {
        // decrease health by 1
        health--;

        // shake the slime
        shaking = true;
        gameObject.GetComponent<SlimeShake>().shakeDuration = 0.2f;

        // check to make sure the slime is still alive
        if (health <= 0)
        {
            // play the death sound effect
            source.PlayOneShot(deathSound, volume);

            // destroy the slime when healt is zero
            Destroy(gameObject);

            // Add to the kill counter
            KillCounter.instance.AddKillToCount();
        }
        else
        {
            // play the hit sound effect
            source.PlayOneShot(hitSound, volume);
        }
    }

    // The slime has been instakilled by something like a supershot
    public void instaKill()
    {
            // instantly kill this slime
            Destroy(gameObject);
    }

    //Gains health
    public void GainHealth()
    {
        health++;
    }

    //Method to adjust initial size of slime that spawns with >3 health
    public void setInitialSize()
    {
        ResizeSlime();
        //gameObject.transform.localScale = new Vector3(0.6f+(float)health * 0.2f, 0.6f+(float)health * 0.2f, 0.0f);
        /*
        if (health > 3)
        {
            UpSizeSlime();
        }
        else if (health < 3)
        {
            ResizeSlime();
        }
        */
    }

    // ResizeSlime and UpSizeSlime now do the same things
    // method for slime to grow or shrink based on health values
    public void ResizeSlime()
    {
        //Debug.Log("Resized");
        //gameObject.transform.localScale = new Vector3(0.7f + (float)health*0.1f, 0.7f + (float)health*0.1f, 1.0f);
        //gameObject.transform.localScale -= new Vector3((float)health * 0.15f, (float)health * 0.15f, 1.0f);
        //gameObject.transform.localScale -= new Vector3(0.15f, 0.15f, 1.0f);
        gameObject.transform.localScale = new Vector3(0.6f+(float)health * 0.2f, 0.6f+(float)health * 0.2f, 0.0f);
    }

    //Grows the Slime's Size
    public void UpSizeSlime()
    {
        //gameObject.transform.localScale += new Vector3((float)health * 0.15f, (float)health * 0.15f, 1.0f);
        //gameObject.transform.localScale += new Vector3(0.15f, 0.15f, 1.0f);
        gameObject.transform.localScale = new Vector3(0.6f+(float)health * 0.2f, 0.6f+(float)health * 0.2f, 0.0f);
    }

    //Sets the current lane, and initial goal lane for the slime.
    public void SetLanes(int lane)
    {
        currentLane = lane;
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.5f)
        {
            if (lane != 1)
            {
                moveTo = lane - 1;
            }
            else
            {
                moveTo = lane + 1;
            }
        }
        else
        {
            if (lane != 4)
            {
                moveTo = lane + 1;
            }
            else
            {
                moveTo = lane - 1;
            }
        }
    }

    public void ReassignLanes()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.5f)
        {
            if (currentLane != 1)
            {
                moveTo = currentLane - 1;
            }
            else
            {
                moveTo = currentLane + 1;
            }
        }
        else
        {
            if (currentLane != 4)
            {
                moveTo = currentLane + 1;
            }
            else
            {
                moveTo = currentLane - 1;
            }
        }
    }

    // collision detection
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "SuperAmmo(Clone)")
        {
            instaKill();
        }
        //Check slime type to determine collision behaviors.
        //Ice types take damage from fire ammo, and absorb ice ammo.
        //Vice versa for fire types.
        if (type == "ice")
        {
            if (collision.gameObject.name == "FireAmmo(Clone)") //Takes Damage
            {
                // delete the projectile
                Destroy(collision.gameObject);

                // call the takeDamage method
                TakeDamage();

                // then resize the slime based on the new health
                ResizeSlime();
            }
            else if (collision.gameObject.name == "IceAmmo(Clone)") //Grows
            {
                // delete the projectile
                Destroy(collision.gameObject);

                // call the takeDamage method
                GainHealth();

                // then resize the slime based on the new health
                UpSizeSlime();
            }
        }
        else if (type == "fire")
        {
            if (collision.gameObject.name == "IceAmmo(Clone)") //Takes Damage
            {
                // delete the projectile
                Destroy(collision.gameObject);

                // call the takeDamage method
                TakeDamage();

                // then resize the slime based on the new health
                ResizeSlime();
            }
            else if (collision.gameObject.name == "FireAmmo(Clone)") //Grows
            {
                // delete the projectile
                Destroy(collision.gameObject);

                // call the takeDamage method
                GainHealth();

                // then resize the slime based on the new health
                UpSizeSlime();
            }
        }
    }

    //Moving the Slimes
    protected virtual void MoveSlime()
    {
        this.transform.Translate(0f, -1f * slimeSpeed, 0f, Space.Self);
    }

    //Implement dashing movement
    public void Dash()
    {
        slideTimer += Time.deltaTime;

        //If the timer is in the range to dash, move the slime.
        if (slideTimer < dashTimer)
        {
            MoveSlime();
        }
        //If the timer reaches the alotted delay time, reset the timer to get the slime to move again, and rerandomize the dash length and delay length.
        else if (slideTimer > delayTimer)
        {
            slideTimer = 0.0f;
            dashTimer = Random.Range(0.3f, 0.7f);
            delayTimer = Random.Range(1.2f, 1.5f);
        }
    }

    //Method for slimes that swap between lanes.
    public void LaneSwap()
    {
        //If the slime has yet to reach it's goal lane.
        if (!reachedLane)
        {
            //Check which direction that the slime is going.
            if (moveTo > currentLane)
            {
                //Translate to the right
                this.transform.Translate(slimeSpeed, 0.0f, 0f, Space.Self);
                //Calculate to see if it has reached goal point. If it has, change the reachedLane variable to true, and set the new current lane.
                if (moveTo == 1)
                {
                    if (this.transform.position.x >= -1.8f)
                    {
                        reachedLane = true;
                        currentLane = 1;
                    }
                }
                else if (moveTo == 2)
                {
                    if (this.transform.position.x >= -0.6f)
                    {
                        reachedLane = true;
                        currentLane = 2;
                    }
                }
                else if (moveTo == 3)
                {
                    if (this.transform.position.x >= 0.6f)
                    {
                        reachedLane = true;
                        currentLane = 3;
                    }
                }
                else if (moveTo == 4)
                {
                    if (this.transform.position.x >= 1.8f)
                    {
                        reachedLane = true;
                        currentLane = 4;
                    }
                }
            }
            //This is translating the slime to the left.
            else
            {
                this.transform.Translate(-1f * slimeSpeed, 0.0f, 0f, Space.Self);
                if (moveTo == 4)
                {
                    if (this.transform.position.x <= 1.8f)
                    {
                        reachedLane = true;
                        currentLane = 4;
                    }
                }
                else if (moveTo == 3)
                {
                    if (this.transform.position.x <= 0.6f)
                    {
                        reachedLane = true;
                        currentLane = 3;
                    }
                }
                else if (moveTo == 2)
                {
                    if (this.transform.position.x <= -0.6f)
                    {
                        reachedLane = true;
                        currentLane = 2;
                    }
                }
                else if (moveTo == 1)
                {
                    if (this.transform.position.x <= -1.8f)
                    {
                        reachedLane = true;
                        currentLane = 1;
                    }
                }
            }
        }
        //If it reached the goal lane.
        else
        {
            //Set up a timer before the next lane swap.
            laneTimer += Time.deltaTime;
            //If the timer reaches 1 second.
            if (laneTimer >= 1.0f)
            {
                //Set the new goal lane for the slime, and set the reachedLane variable to false in order to return to movement loop.
                if (currentLane == 1)
                {
                    moveTo = 2;
                }
                else if (currentLane == 4)
                {
                    moveTo = 3;
                }
                else
                {
                    float random = Random.Range(0.0f, 1.0f);
                    if (random < 0.5f)
                    {
                        moveTo -= 1;
                    }
                    else
                    {
                        moveTo += 1;
                    }
                }
                //Failsafe statements to make sure the goal lane is within specified range.
                if (moveTo < 1)
                {
                    moveTo = 1;
                }
                if (moveTo > 4)
                {
                    moveTo = 4;
                }
                reachedLane = false;
                laneTimer = 0.0f;
            }
        }
        //And call the generic move method to have the slime move downward.
        MoveSlime();
    }
}
