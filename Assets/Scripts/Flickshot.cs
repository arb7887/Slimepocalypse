using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Flickshot : MonoBehaviour
{

    Vector2 startVec2 = Vector2.zero;
    Vector2 releaseVec2 = Vector2.zero;
    Vector2 launchStartVec2 = Vector2.zero;
    Vector2 launchAngle = Vector2.zero;
    Vector2 launchAngleCheck = Vector2.zero;
    public GameObject ammo;
    public GameObject fireAmmo;
    public GameObject iceAmmo;
    public GameObject superAmmo;
    public int ammoType = 1; //Represents element: 1 = Fire | 2 = Ice
    private int superShotCount = 0; // Counts how many shots have been fired to check if we need to fire a super shot
    private int ammoTypeHolder = 0;

    public Button MagicCircleButton;
    public Sprite fireCircle;
    public Sprite iceCircle;

    public GameObject Camera;

    // audio objects
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip iceSound;
    public AudioClip superSound;

    // runs before start
    private void Awake()
    {
        // set up the audio source
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        launchStartVec2 = new Vector2(Screen.width * 0.5f, Screen.height * 0.1125f);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneCheck() == 1)
        {

            // done to make sure a shot doesn't fire as soon as the scene starts due to pressing the button
            if(KillCounter.instance.timer > 0.01f && !SlimeManagerSingleton.Instance.isGameOver)
            {
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
                else if (Input.GetMouseButtonDown(0) == true)
                {
                    // Save the touch's location in the beginning of the touch
                    startVec2 = Input.mousePosition;
                }
                // Check if the player has released
                if (Input.GetMouseButtonUp(0) == true && startVec2 != Vector2.zero)
                {
                    releaseVec2 = Input.mousePosition;
                    Launch();

                    // reset the start and release vectors
                    startVec2 = Vector2.zero;
                    releaseVec2 = Vector2.zero;
                }
            }
        }
    }

    // Calculates the launch angle using the 2 startVec2 and the releaseVec2, creates a new object on the screen, and launches it with a force
    void Launch()
    {

        // Check to see if we're done using the berserk state. If so, start using the previous ammo type again.
        if (KillCounter.instance.IsBerserkState() == false && ammoType == 3)
        {
            // Return the ammoType to the element it was on a bit ago
            ammoType = ammoTypeHolder;
        }

        // Calculate launch angle
        launchAngle = releaseVec2 - launchStartVec2; //Always start from a set position
        launchAngleCheck = releaseVec2 - startVec2; //Used for "if" checks in order to retain touch logic

        if (launchAngleCheck.y <= -(Screen.height * 0.1f))
        {
            // Swipe down to change element

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
        else if ((startVec2.x <= launchStartVec2.x + Screen.width / 8 &&
                  startVec2.x >= launchStartVec2.x - Screen.width / 8 &&
                  startVec2.y <= launchStartVec2.y + Screen.width / 8 &&
                  startVec2.y >= launchStartVec2.y - Screen.width / 8) &&
                  (releaseVec2.x <= launchStartVec2.x + Screen.width / 8 &&
                  releaseVec2.x >= launchStartVec2.x - Screen.width / 8 &&
                  releaseVec2.y <= launchStartVec2.y + Screen.width / 8 &&
                  releaseVec2.y >= launchStartVec2.y - Screen.width / 8))
        {

            // Begin the berserk state and reset the variables
            KillCounter.instance.BeginBerserkState();
        }
        else
        {
            // Check if this is a supershot
            if (KillCounter.instance.IsBerserkState() == true && ammoType != 3)
            {

                // Store the ammoType in the ammoTypeHolder
                ammoTypeHolder = ammoType;
                ammoType = 3;
            }

            var projectile = Instantiate(ammo, new Vector2(0, -10), Quaternion.identity);

            if (ammoType == 1)
            {
                projectile = Instantiate(fireAmmo, launchStartVec2, Quaternion.identity);
                // play a fire sound effect
                //source.clip = fireSound;
                source.PlayOneShot(fireSound,0.5f);
            }
            else if (ammoType == 2)
            {
                projectile = Instantiate(iceAmmo, launchStartVec2, Quaternion.identity);
                // play an ice sound effect
                //source.clip = iceSound;
                source.PlayOneShot(iceSound);
            }
            else if (ammoType == 3)
            {
                projectile = Instantiate(superAmmo, launchStartVec2, Quaternion.identity);

                KillCounter.instance.BeginBerserkState();

                // play a super shot sound effect
                //source.clip = superSound;
                source.PlayOneShot(superSound,1.5f);
            }

            launchAngle.Normalize();

            projectile.transform.position = new Vector2(Camera.transform.localPosition.x, Camera.transform.localPosition.y - 4.2f);

            // Add a force to the ammo and adjust its angle
            projectile.GetComponent<Rigidbody2D>().AddForce(launchAngle * 600);
            projectile.GetComponent<Rigidbody2D>().transform.up = launchAngle;

        }
    }

    public int SceneCheck()
    {
        //If we are in the main game scene
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (Camera == null)
            {
                Camera = GameObject.Find("Main Camera");
            }
            if (MagicCircleButton == null)
            {
                MagicCircleButton = GameObject.Find("Button").GetComponent<Button>();
            }
            return 1;
        }
        else
        {
            return 2;
        }
    }
    
    // Depricated Flicking Code
    /*
    // Calculates the launch angle using the 2 startVec2 and the releaseVec2, creates a new object on the screen, and launches it with a force
    void Launch()
    {

        // Calculate launch angle
        launchAngle = releaseVec2 - launchStartVec2; //Always start from a set position
        launchAngleCheck = releaseVec2 - startVec2; //Used for "if" checks in order to retain touch logic

        //Debug.Log("launchStartVec2: " + launchStartVec2);
        //Debug.Log("releaseVec2: " + releaseVec2);
        //Debug.Log("launchAngle: " + launchAngle);
        //Debug.Log("Screen Width: " + Screen.width);


        // Checking if there was enough of a flick to count it
        if (Mathf.Abs(launchAngleCheck.x) >= 1.0f && launchAngleCheck.y >= 1.0f &&
              //!(startVec2.x <= launchStartVec2.x + Screen.width / 8 &&
              //startVec2.x >= launchStartVec2.x - Screen.width / 8 &&
              //startVec2.y <= launchStartVec2.y + Screen.width / 8 &&
              //startVec2.y >= launchStartVec2.y - Screen.width / 8) && //Commented out so you can flick from the circle
              releaseVec2.y > launchStartVec2.y)
        {

            // Check if this is a supershot
            if (KillCounter.instance.IsSuperShot() == true)
            {

                // Store the ammoType in the ammoTypeHolder
                ammoTypeHolder = ammoType;
                ammoType = 3;
            }

            var projectile = Instantiate(ammo, new Vector2(0, -10), Quaternion.identity);

            if (ammoType == 1)
            {
                projectile = Instantiate(fireAmmo, launchStartVec2, Quaternion.identity);
            }
            else if (ammoType == 2)
            {
                projectile = Instantiate(iceAmmo, launchStartVec2, Quaternion.identity);
            }
            else if (ammoType == 3)
            {
                projectile = Instantiate(superAmmo, launchStartVec2, Quaternion.identity);

                // Return the ammoType to the element it was on a bit ago
                ammoType = ammoTypeHolder;
            }

            launchAngle.Normalize();

            projectile.transform.position = new Vector2(Camera.transform.localPosition.x, Camera.transform.localPosition.y - 4.2f);

            // Add a force to the ammo and adjust its angle
            projectile.GetComponent<Rigidbody2D>().AddForce(launchAngle * 600);
            projectile.GetComponent<Rigidbody2D>().transform.up = launchAngle;

        }
        else if (launchAngleCheck.y <= -(Screen.height * 0.1f))
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
        else if ((startVec2.x <= launchStartVec2.x + Screen.width / 8 &&
                  startVec2.x >= launchStartVec2.x - Screen.width / 8 &&
                  startVec2.y <= launchStartVec2.y + Screen.width / 8 &&
                  startVec2.y >= launchStartVec2.y - Screen.width / 8) &&
                  (releaseVec2.x <= launchStartVec2.x + Screen.width / 8 &&
                  releaseVec2.x >= launchStartVec2.x - Screen.width / 8 &&
                  releaseVec2.y <= launchStartVec2.y + Screen.width / 8 &&
                  releaseVec2.y >= launchStartVec2.y - Screen.width / 8))
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
    */


}
