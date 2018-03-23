using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveSlime : Slime {

    float switchTimer;

	// Use this for initialization
	void Start () {
        switchTimer = 0.0f;
        setInitialSize();
        slimeSpeed = 0.01f;
        dashSpeed = slimeSpeed * 3.0f;
        dashTimer = Random.Range(0.3f, 0.7f);
        delayTimer = Random.Range(1.2f, 1.5f);
        laneTimer = 0.0f;
        reachedLane = true;
        float random = Random.Range(0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
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
        //Timer for switching types
        switchTimer += Time.deltaTime;
        if (switchTimer > 3.0f)
        {
            //If timer is activated, check type, and switch sprite and type accordingly.
            if (type == "ice")
            {
                type = "fire";
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[1];
            }
            else
            {
                type = "ice";
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[0];
            }
            switchTimer = 0.0f;
        }
    }
}
