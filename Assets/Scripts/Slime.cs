using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    private int health = 3; // number of hits until a slime dies
    private string slimeType = "fire"; // what element type of slime it is

	// Use this for initialization
	void Start ()
    {
        // default values for slime and type
        //health = 3;
        //slimeType = "fire"; // or "ice" but currently only fire
	}
	
	// Update is called once per frame
	void Update ()
    {

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
        }
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

    // method for slime to grow or shrink based on health values
    public void ResizeSlime()
    {
        gameObject.transform.localScale = new Vector3(0.7f + (float)health*0.1f, 0.7f + (float)health*0.1f, 1.0f);
    }

    // collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // on collision, call takeDamage
        // currently using ammo's tag as the determiner for which type of ammo it is
        //TakeDamage(collision.gameObject.GetComponent<Flickshot>().ammo.tag);
        // simple take damage method instead

        Debug.Log("Hit");
        // delete the projectile
        Destroy(collision.gameObject);

        // call the takeDamage method
        TakeDamage();

        // then resize the slime based on the new health
        ResizeSlime();
    }
}
