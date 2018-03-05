using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flickshot : MonoBehaviour {

    Vector2 startVec2 = Vector2.zero;
    Vector2 releaseVec2 = Vector2.zero;
    Vector2 launchStartVec2 = Vector2.zero;
    Vector2 launchAngle = Vector2.zero;
    Vector2 launchAngleCheck = Vector2.zero;
    //bool isHolding = false;
    public GameObject ammo;
    public GameObject fireAmmo;
    public GameObject iceAmmo;
    public int ammoType = 1; //Represents element: 1 = Fire | 2 = Ice

    public Button MagicCircleButton;
    public Sprite fireCircle;
    public Sprite iceCircle;

    public GameObject Camera;

	// Use this for initialization
	void Start () {
        //launchStartVec2 = new Vector2(240, 80);
        launchStartVec2 = new Vector2(MagicCircleButton.transform.position.x, MagicCircleButton.transform.position.y);
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
                    startVec2 = touch.position;
                    //startVec2 = new Vector2(0, -10); //We want to calculate the angle from the origin point of the magic bolt, which is always going to be the same location (The Magic Circle)
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

    }

    // Calculates the launch angle using the 2 startVec2 and the releaseVec2, creates a new object on the screen, and launches it with a force
    void Launch() {

        // Calculate launch angle
        launchAngle = releaseVec2 - launchStartVec2; //Always start from a set position
        launchAngleCheck = releaseVec2 - startVec2; //Used for "if" checks in order to retain touch logic


        // Checking if there was enough of a flick to count it
        if (Mathf.Abs(launchAngleCheck.x) >= 1.0f && launchAngleCheck.y >= 1.0f &&
            !(startVec2.x <= MagicCircleButton.transform.position.x + 50.0f &&
              startVec2.x >= MagicCircleButton.transform.position.x - 50.0f &&
              startVec2.y <= MagicCircleButton.transform.position.y + 50.0f &&
              startVec2.y >= MagicCircleButton.transform.position.y - 50.0f))
        {
            var projectile = Instantiate(ammo, new Vector2(0, -10), Quaternion.identity);

            if(ammoType == 1)
            {
                //projectile = Instantiate(fireAmmo, new Vector2(0, -10), Quaternion.identity);
                projectile = Instantiate(fireAmmo, new Vector2(MagicCircleButton.transform.position.x, MagicCircleButton.transform.position.y + 80), Quaternion.identity);
            }
            else if (ammoType == 2)
            {
                //projectile = Instantiate(iceAmmo, new Vector2(0, -10), Quaternion.identity);
                projectile = Instantiate(iceAmmo, new Vector2(MagicCircleButton.transform.position.x, MagicCircleButton.transform.position.y), Quaternion.identity);
            }

            launchAngle.Normalize();

            projectile.transform.position = new Vector2(Camera.transform.localPosition.x, Camera.transform.localPosition.y - 4.2f);

            // Add a force to the ammo and adjust its angle
            projectile.GetComponent<Rigidbody2D>().AddForce(launchAngle * 600);
            projectile.GetComponent<Rigidbody2D>().transform.up = launchAngle;

        }
        else if (launchAngleCheck.y <= -125.0f)
        {
            if(ammoType == 1)
            {
                ammoType = 2;
                MagicCircleButton.GetComponent<Image>().sprite = iceCircle;
            }
            else if(ammoType == 2)
            {
                ammoType = 1;
                MagicCircleButton.GetComponent<Image>().sprite = fireCircle;
            }
        }
        else if ((startVec2.x <= MagicCircleButton.transform.position.x + 50.0f &&
                  startVec2.x >= MagicCircleButton.transform.position.x - 50.0f &&
                  startVec2.y <= MagicCircleButton.transform.position.y + 50.0f &&
                  startVec2.y >= MagicCircleButton.transform.position.y - 50.0f) &&
                  (releaseVec2.x <= MagicCircleButton.transform.position.x + 50.0f &&
                  releaseVec2.x >= MagicCircleButton.transform.position.x - 50.0f &&
                  releaseVec2.y <= MagicCircleButton.transform.position.y + 50.0f &&
                  releaseVec2.y >= MagicCircleButton.transform.position.y - 50.0f))
        {
            if (ammoType == 1)
            {
                ammoType = 2;
                MagicCircleButton.GetComponent<Image>().sprite = iceCircle;
            }
            else if (ammoType == 2)
            {
                ammoType = 1;
                MagicCircleButton.GetComponent<Image>().sprite = fireCircle;
            }
        }
    }


}
