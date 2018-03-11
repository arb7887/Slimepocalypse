using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomSlime : Slime {

    void Start()
    {
        // set the size to start
        setInitialSize();

        //Sets initial speed
        slimeSpeed = 0.02f;

        //Randomly generates a number to determine slime type, and assign sprite from there.
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SuperAmmo(Clone)")
        {
            instaKill();
        }
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
                //UpSizeSlime();
                ResizeSlime();
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
                //UpSizeSlime();
                ResizeSlime();
            }
        }
        //If the slime collides with another slime
        if (collision.gameObject.tag == "Slime")
        {
            //Gain health and get bigger.
            health += 2;
            ResizeSlime();
            //UpSizeSlime();
            //UpSizeSlime();

            //And change type based on the slime that it collided with.
            type = collision.gameObject.GetComponent<Slime>().type;
            if (type == "fire")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[1];
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[0];
            }

            //Then the other slime is deleted.
            Destroy(collision.gameObject);
        }
    }

}
