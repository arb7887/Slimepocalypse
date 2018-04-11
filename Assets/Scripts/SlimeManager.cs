using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    public GameObject slimePrefab;
    public List<GameObject> slimePrefabs; //Created a list of slime prefabs so we wouldn't need to constantly make new GameObject variables for more slime types.
    public List<GameObject> slimeList;
    public float timer;
    public float globalTimer;
    public float spawnTime; //Adjusted spawn speed as game goes on.
    public int healthTotal; //Adjusting slime health as game goes on.
    public int healthRange; //The range of values a Slime's health can be set to
    public float slimeSpeedOffset; //Adjusting speed of slimes.
    //public KillCounter killCounterSingleton;
    int numMoveTypes;
    public int normalSpawnrate, nomSpawnrate, momSpawnrate, raveSpawnrate;
    public GameObject gameOverMenu;

    // Use this for initialization
    void Start () {
        timer = 0.0f;
        globalTimer = 0.0f;
        spawnTime = 2.0f;
        healthTotal = 2;
        healthRange = 3; //The max range is exlusive so this will always be 1 higher than the actual max range
        slimeSpeedOffset = 0.0f;
        //killCounterSingleton = GetComponent<KillCounter>();
        numMoveTypes = 1;
        nomSpawnrate = 0;
        momSpawnrate = 0;
        raveSpawnrate = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        globalTimer += Time.deltaTime;
        //Once the time for whatever we determine one round to be passes...
        if (globalTimer > 15.0f) //This number represents seconds
        {
            int randDifficulty = Random.Range(1, 4); //Randomly increase an aspect of the Slimes
            //Randomly increase an aspect of the Slimes to slowly ramp up difficulty at a non-exponential curve
            switch (randDifficulty)
            {
                case (1):
                    //healthTotal += 1;
                    healthRange += 2;
                    break;
                case (2):
                    slimeSpeedOffset += 0.005f;
                    break;
                case (3):
                    if (spawnTime >= 0.5)
                    {
                        spawnTime -= 0.1f;
                    }
                    break;
            }

            //Increase the spawnrate of a Slime
            int randSlimeRate = Random.Range(1, 4);

            switch (randSlimeRate)
            {
                case (1):
                    if(!(nomSpawnrate == 15))
                    {
                        nomSpawnrate += 5;
                    }
                    break;
                case (2):
                    if (!(momSpawnrate == 30))
                    {
                        momSpawnrate += 5;
                    }
                    break;
                case (3):
                    if (!(raveSpawnrate == 10))
                    {
                        raveSpawnrate += 2;
                    }
                    break;
            }


            //Then reset the timer so we can continue adjusting the difficulty as the rounds progress.
            globalTimer = 0.0f;
        }
        if (timer > spawnTime)
        {
            //Randomly choose between Fire and Ice
            //Less for a smaller screen

            //Generates 2 random integers. One to determine type, and one to determine movement pattern.
            //Range from 1 to 100 for calculating movement type.
            int randSlime = Random.Range(1, 101);
            GameObject newSlime = slimePrefab;
            //Debug.Log(randSlime);
            //Based on the random numbers that are generated, we create a slime of the corresponding type and movement pattern.
            //We have these spawn rate variables for each different slime type. Remainder is change for normal slime.
            if (randSlime <= momSpawnrate)
            {
                newSlime = Instantiate(slimePrefabs[3]);
            }
            else if(randSlime <= (momSpawnrate + nomSpawnrate))
            {
                newSlime = Instantiate(slimePrefabs[1]);
               
            }
            else if (randSlime <= (momSpawnrate + nomSpawnrate + raveSpawnrate))
            {
                newSlime = Instantiate(slimePrefabs[2]);
            }
            else
            {
                newSlime = Instantiate(slimePrefabs[0]);
            }
            //For now, generate a random number to determine movement type. Will later add functionality to have multiple movement types added at same time.
            int moveType = Random.Range(0, 3);
            if (moveType == 0)
            {
                newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            }
            else if (moveType == 1)
            {
                newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Dashing);
            }
            else if (moveType == 2)
            {
                newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.LaneSwap);

            }
            int type = Random.Range(0, 2);
            newSlime.GetComponent<Slime>().SetType(type);

            //Randomly give a Health value so not all Slimes have massive health during the end game
            healthTotal = Random.Range(2, healthRange);

            //And apply any difficulty changes that we have currently implemented.
            newSlime.GetComponent<Slime>().health = healthTotal;
            newSlime.GetComponent<Slime>().slimeSpeed += slimeSpeedOffset;
            newSlime.GetComponent<Slime>().dashSpeed += slimeSpeedOffset;
            newSlime.GetComponent<Slime>().manager = gameObject;
            newSlime.GetComponent<Slime>().gameOverMenu = gameOverMenu;

            //Randomly choose between 4 spawn points, and set the lane fields for each slime to be used in lane switching calculation.
            int random = Random.Range(1, 5);

            switch (random)
            {
                case (1):
                    newSlime.transform.position = new Vector3(-1.8f, 6.0f, 0.0f);
                    newSlime.GetComponent<Slime>().SetLanes(1);
                    break;
                case (2):
                    newSlime.transform.position = new Vector3(-0.6f, 6.0f, 0.0f);
                    newSlime.GetComponent<Slime>().SetLanes(2);
                    break;
                case (3):
                    newSlime.transform.position = new Vector3(0.6f, 6.0f, 0.0f);
                    newSlime.GetComponent<Slime>().SetLanes(3);
                    break;
                case (4):
                    newSlime.transform.position = new Vector3(1.8f, 6.0f, 0.0f);
                    newSlime.GetComponent<Slime>().SetLanes(4);
                    break;
            }

            slimeList.Add(newSlime);
            timer = 0.0f;
        }
	}

    public void stopSlimes()
    {
        for (int i = 0; i < slimeList.Count; i++)
        {
            if (slimeList[i] != null)
            {
                slimeList[i].GetComponent<Slime>().canMove = false;
            }
        }
    }
}
