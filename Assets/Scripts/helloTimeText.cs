using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helloTimeText : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        KillCounter.instance.timeText = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
