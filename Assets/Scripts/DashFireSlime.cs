using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFireSlime : DashSlime {

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "IceAmmo(Clone)") //Takes Damage
        {
            Debug.Log("Ice Hit");
            // delete the projectile
            Destroy(collision.gameObject);

            // call the takeDamage method
            TakeDamage();

            // then resize the slime based on the new health
            ResizeSlime();
        }
        else if (collision.gameObject.name == "FireAmmo(Clone)") //Grows
        {
            Debug.Log("Fire Grow");
            // delete the projectile
            Destroy(collision.gameObject);

            // call the takeDamage method
            GainHealth();

            // then resize the slime based on the new health
            UpSizeSlime();
        }
    }
}
