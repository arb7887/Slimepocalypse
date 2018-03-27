using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hello : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        KillCounter.instance.killCountText = gameObject;
        KillCounter.instance.resetSupershotImages();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
