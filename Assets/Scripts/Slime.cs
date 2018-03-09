using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    public int health = 2; // number of hits until a slime dies
    //private string slimeType = "fire"; // what element type of slime it is

    public float slimeSpeed;
    public string type; //Determines collision detection for different ammo types.
    public List<Sprite> spriteList; //List of sprites for different slime types.

    // Use this for initialization
    void Start ()
    {
        // default values for slime and type
        //health = 3;
        //slimeType = "fire"; // or "ice" but currently only fire
        setInitialSize();
        slimeSpeed = 0.01f;
        //Randomly set the type and sprite of the slime.
        int rand = Random.Range(0, 2);
        if (rand == 0)
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
	
	// Update is called once per frame
	void Update ()
    {
        //Moving the slimes every frame
        MoveSlime();
	}

    // simple takedamage
    public void TakeDamage()
    {
        // decrease health by 1
        health--;

        // check to make sure the slime is still alive
        if (health <= 0)
        {
            // destroy the slime when healt is zero
            Destroy(gameObject);

            // Add to the kill counter
            KillCounter.instance.AddKillToCount();
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

    /* more advanced takedamage method
    // method for a slime to take damage
    public void TakeDamage(string projectileType)
    {
        // check to make sure the slime was hit by a different element than its type
        if(slimeType != projectileType)
        {
            // decrease health by 1
            health--;

            // check to make sure the slime is still alive
            if (health <= 0)
            {
                // destroy the slime when healt is zero
                Destroy(gameObject);
            }
        }

        // otherwise they gain health and grow in size
        else
        {
            health++;
        }
    }
    */

    //Method to adjust initial size of slime that spawns with >3 health
    public void setInitialSize()
    {
        if (health > 3)
        {
            UpSizeSlime();
        }
        else if (health < 3)
        {
            ResizeSlime();
        }
    }

    // method for slime to grow or shrink based on health values
    public void ResizeSlime()
    {
        //gameObject.transform.localScale = new Vector3(0.7f + (float)health*0.1f, 0.7f + (float)health*0.1f, 1.0f);
        //gameObject.transform.localScale -= new Vector3((float)health * 0.15f, (float)health * 0.15f, 1.0f);
        gameObject.transform.localScale -= new Vector3(0.15f, 0.15f, 1.0f);
    }

    //Grows the Slime's Size
    public void UpSizeSlime()
    {
        //gameObject.transform.localScale += new Vector3((float)health * 0.15f, (float)health * 0.15f, 1.0f);
        gameObject.transform.localScale += new Vector3(0.15f, 0.15f, 1.0f);
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
}
