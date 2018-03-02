using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlime : Slime {

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FireAmmo(Clone)") //Takes Damage
        {
            Debug.Log("Fire Hit");
            // delete the projectile
            Destroy(collision.gameObject);

            // call the takeDamage method
            TakeDamage();

            // then resize the slime based on the new health
            ResizeSlime();
        }
        else if (collision.gameObject.name == "IceAmmo(Clone)") //Grows
        {
            Debug.Log("Ice Grow");
            // delete the projectile
            Destroy(collision.gameObject);

            // call the takeDamage method
            GainHealth();

            // then resize the slime based on the new health
            UpSizeSlime();
        }
    }
}
