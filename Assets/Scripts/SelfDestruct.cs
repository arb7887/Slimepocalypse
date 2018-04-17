using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //If the projectile goes far enough off screen, it'll delete itself
        if (gameObject.transform.position.x > 3.5 || gameObject.transform.position.x < -3.5 || gameObject.transform.position.y > 4.5 || gameObject.transform.position.y < -4.5)
        {
            Destroy(gameObject);
        }
    }
}
