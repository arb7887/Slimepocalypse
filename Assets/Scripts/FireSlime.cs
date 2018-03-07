using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : Slime {

    
	// Use this for initialization
	void Start () {
        type = "fire";
	}
	

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "IceAmmo(Clone)") //Takes Damage
        {
            // delete the projectile
            Destroy(collision.gameObject);

            // call the takeDamage method
            TakeDamage();

            // then resize the slime based on the new health
            ResizeSlime();
        }
        else if(collision.gameObject.name == "FireAmmo(Clone)") //Grows
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
