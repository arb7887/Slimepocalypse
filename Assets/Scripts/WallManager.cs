using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    // create variables for holding walls
    public List<GameObject> walls = new List<GameObject>(); 
    public GameObject wallImage1;
    public GameObject wallImage2;
    public GameObject wallImage3;

    private int numWalls = 5; // number of wall pieces
    private float imgWidth; // width of the image in pixels

    //private float screenWidth;
    //private float screenHeight;

	// Use this for initialization
	void Start ()
    {
        // calculate the screen dimensions
        //screenWidth = Camera.main.WorldToScreenPoint(Screen.width);

        // calculate image width
        imgWidth = wallImage1.GetComponent<SpriteRenderer>().sprite.rect.width;

        // call the create walls method
        createWall();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    // method to spawn walls
    private void createWall()
    {
        /*
        // loop to instantiate wall pieces
        for(int i=0; i < numWalls; i++)
        {
            // temporary wall object
            GameObject tempWall = Instantiate(wallImage,new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);

            // add to list of walls
            walls.Add(tempWall);
        }
        */

        // Hardcode wall positions
        Vector3 wallPos1 = new Vector3(-2.5f, -2.0f, 0.0f);
        Vector3 wallPos2 = new Vector3(-1.25f, -2.0f, 0.0f);
        Vector3 wallPos3 = new Vector3(0.0f, -2.0f, 0.0f);
        Vector3 wallPos4 = new Vector3(1.25f, -2.0f, 0.0f);
        Vector3 wallPos5 = new Vector3(2.5f, -2.0f, 0.0f);

        // instantiate the gameobjects
        GameObject tempWall1 = Instantiate(wallImage1, wallPos1, Quaternion.identity);
        GameObject tempWall2 = Instantiate(wallImage1, wallPos2, Quaternion.identity);
        GameObject tempWall3 = Instantiate(wallImage1, wallPos3, Quaternion.identity);
        GameObject tempWall4 = Instantiate(wallImage1, wallPos4, Quaternion.identity);
        GameObject tempWall5 = Instantiate(wallImage1, wallPos5, Quaternion.identity);

        // add them to the list of walls
        walls.Add(tempWall1);
        walls.Add(tempWall2);
        walls.Add(tempWall3);
        walls.Add(tempWall4);
        walls.Add(tempWall5);
    }
}
