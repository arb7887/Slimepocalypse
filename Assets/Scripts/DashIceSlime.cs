using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIceSlime : DashSlime {

    protected override void OnCollisionEnter2D(Collision2D collision)
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
}

