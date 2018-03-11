using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSlime : Slime {

    float slideTimer;
    float dashTimer,delayTimer; //2 random numbers to determine length of slime's dash, and delay before next dash. Randomized for sporatic movement.

	// Use this for initialization
	void Start () {
        // set the size to start
        setInitialSize();
        slideTimer = 0.0f;
        slimeSpeed = 0.04f;
        dashTimer = Random.Range(0.3f, 0.7f);
        delayTimer = Random.Range(1.2f, 1.5f);
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
	void Update () {
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


}
