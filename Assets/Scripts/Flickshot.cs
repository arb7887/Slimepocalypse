using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickshot : MonoBehaviour {

    Vector2 startVec2 = Vector2.zero;
    Vector2 releaseVec2 = Vector2.zero;
    Vector2 launchAngle = Vector2.zero;
    //bool isHolding = false;
    public GameObject ammo;
    public GameObject fireAmmo;
    public GameObject iceAmmo;
    public int ammoType = 1; //Represents element: 1 = Fire | 2 = Ice
    //GameObject[] ammoList = new GameObject[10];
    //int currentAmmo = 0;
    //int maxAmmo = 10;

    public GameObject Camera;

	// Use this for initialization
	void Start () {
        /*
        for(int i = 0; i < maxAmmo; i++)
        {
            ammoList[i] = Instantiate(ammo, new Vector2(-10,-10), Quaternion.identity);
        }*/
    }
	
	// Update is called once per frame
	void Update () {


        // Check if the player is touching the screen
        if (Input.touchCount > 0)
        {

            // Store a touch
            Touch touch = Input.GetTouch(0);

            // Check which part of the touch we're on
            switch (touch.phase)
            {
                // Save the touch's location in the beginning of the touch
                case TouchPhase.Began:
                    //startVec2 = touch.position; //Using this for angle calculation would create an angle from where you touch, to where you end, then apply that angle to a projectile that starts at the magic bolt's location, which feels weird.
                    startVec2 = new Vector2(0, -10); //We want to calculate the angle from the origin point of the magic bolt, which is always going to be the same location
                    break;
                // Useable for calculating angle
                case TouchPhase.Moved:
                    // Might be used later to draw a line representing the angle that the player will use when shooting ammo
                    break;
                // Save the touch's location on the release of the touch
                case TouchPhase.Ended:
                    releaseVec2 = touch.position;
                    Launch();

                    // reset the start and release vectors
                    startVec2 = Vector2.zero;
                    releaseVec2 = Vector2.zero;
                    break;
            }
        }
        // Check if the player is clicking on the screen
        else if (Input.GetMouseButtonDown(0) == true) {
            // Save the touch's location in the beginning of the touch
            startVec2 = Input.mousePosition;
        }
        // Check if the player has released
        if (Input.GetMouseButtonUp(0) == true && startVec2 != Vector2.zero) {
            releaseVec2 = Input.mousePosition;
            Launch();
            
            // reset the start and release vectors
            startVec2 = Vector2.zero;
            releaseVec2 = Vector2.zero;
        }

        //Temporary testing of shooting the right projectile
        if(Input.GetKey("up"))
        {
            ammoType = 1;
        }
        if (Input.GetKey("down"))
        {
            ammoType = 2;
        }

    }

    // Calculates the launch angle using the 2 startVec2 and the releaseVec2, creates a new object on the screen, and launches it with a force
    void Launch() {

        // Calculate launch angle
        launchAngle = releaseVec2 - startVec2;


        Debug.Log("Launch Vector: " + launchAngle);
        /*
        // Checking if there was enough of a flick to count it
        if (Mathf.Abs(launchAngle.x) >= 1.0f && launchAngle.y >= 1.0f)
        {
            launchAngle.Normalize();

            // Moves the ammo and resets its force
            ammoList[currentAmmo].transform.position = new Vector2(Camera.transform.localPosition.x, Camera.transform.localPosition.y - 4.2f);
            ammoList[currentAmmo].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Add a force to the ammo and adjust its angle
            ammoList[currentAmmo].GetComponent<Rigidbody2D>().AddForce(launchAngle * 600);
            ammoList[currentAmmo].GetComponent<Rigidbody2D>().transform.up = launchAngle;

            // Increment and if necessary, reset the current ammo count
            currentAmmo++;
            if (currentAmmo >= maxAmmo) currentAmmo = 0;
        }
        */

        //Allows for infinite ammo
        //var projectile = Instantiate(ammo, new Vector2(-10, -10), Quaternion.identity);

        // Checking if there was enough of a flick to count it
        if (Mathf.Abs(launchAngle.x) >= 1.0f && launchAngle.y >= 1.0f)
        {
            var projectile = Instantiate(ammo, new Vector2(0, -10), Quaternion.identity);

            if(ammoType == 1)
            {
                projectile = Instantiate(fireAmmo, new Vector2(0, -10), Quaternion.identity);
            }
            else if (ammoType == 2)
            {
                projectile = Instantiate(iceAmmo, new Vector2(0, -10), Quaternion.identity);
            }

            launchAngle.Normalize();

            projectile.transform.position = new Vector2(Camera.transform.localPosition.x, Camera.transform.localPosition.y - 4.2f);

            // Add a force to the ammo and adjust its angle
            projectile.GetComponent<Rigidbody2D>().AddForce(launchAngle * 600);
            projectile.GetComponent<Rigidbody2D>().transform.up = launchAngle;

        }
        else
        {
            if(ammoType == 1)
            {
                ammoType = 2;
            }
            else if(ammoType == 2)
            {
                ammoType = 1;
            }
        }
    }


}
