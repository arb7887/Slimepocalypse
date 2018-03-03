using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    public GameObject slimePrefab;
    public GameObject fireSlimePrefab;
    public GameObject iceSlimePrefab;
    public List<GameObject> slimeList;
    float timer;

    // Use this for initialization
    void Start () {
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            /*
            int random = Random.Range(1, 8);
            GameObject newSlime = Instantiate(slimePrefab);
            switch (random)
            { 
                case (1):
                    newSlime.transform.position = new Vector3(-2.4f, 5.0f, 0.0f);
                    break;
                case (2):
                    newSlime.transform.position = new Vector3(-1.8f, 5.0f, 0.0f);
                    break;
                case (3):
                    newSlime.transform.position = new Vector3(-1.2f, 5.0f, 0.0f);
                    break;
                case (4):
                    newSlime.transform.position = new Vector3(-0.6f, 5.0f, 0.0f);
                    break;
                case (5):
                    newSlime.transform.position = new Vector3(0.0f, 5.0f, 0.0f);
                    break;
                case (6):
                    newSlime.transform.position = new Vector3(0.75f, 5.0f, 0.0f);
                    break;
                case (7):
                    newSlime.transform.position = new Vector3(7.5f, 5.0f, 0.0f);
                    break;
                case (8):
                    newSlime.transform.position = new Vector3(10.0f, 5.0f, 0.0f);
                    break;
            }*/

            //Randomly choose between Fire and Ice
            //Less for a smaller screen
            int randSlime = Random.Range(1, 3);

            GameObject newSlime = null;

            if (randSlime == 1)
            {
                newSlime = Instantiate(fireSlimePrefab);
            }
            else if(randSlime == 2)
            {
                newSlime = Instantiate(iceSlimePrefab);
            }

            //Randomly choose between 4 spawn points
            int random = Random.Range(1, 5);

            switch (random)
            {
                case (1):
                    newSlime.transform.position = new Vector3(-1.8f, 6.0f, 0.0f);
                    break;
                case (2):
                    newSlime.transform.position = new Vector3(-0.6f, 6.0f, 0.0f);
                    break;
                case (3):
                    newSlime.transform.position = new Vector3(0.6f, 6.0f, 0.0f);
                    break;
                case (4):
                    newSlime.transform.position = new Vector3(1.8f, 6.0f, 0.0f);
                    break;
            }
            slimeList.Add(newSlime);
            timer = 0.0f;
        }
	}
}
