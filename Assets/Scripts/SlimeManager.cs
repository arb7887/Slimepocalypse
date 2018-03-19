using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    public GameObject slimePrefab;
    public List<GameObject> slimePrefabs; //Created a list of slime prefabs so we wouldn't need to constantly make new GameObject variables for more slime types.
    public List<GameObject> slimeList;
    float timer;
    float globalTimer;
    float spawnTime; //Adjusted spawn speed as game goes on.
    int healthTotal; //Adjusting slime health as game goes on.
    float slimeSpeedOffset; //Adjusting speed of slimes.
    public KillCounter killCounterSingleton;
    int numMoveTypes;

    // Use this for initialization
    void Start () {
        timer = 0.0f;
        globalTimer = 0.0f;
        spawnTime = 2.0f;
        healthTotal = 2;
        slimeSpeedOffset = 0.0f;
        killCounterSingleton = GetComponent<KillCounter>();
        numMoveTypes = 1;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        globalTimer += Time.deltaTime;
        //Once the time for whatever we determine one round to be passes...
        if (globalTimer > 30.0f)
        {
            int randDifficulty = Random.Range(1, 4); //Randomly increase an aspect of the Slimes

            //We lower the spawn rate, but only until it reaches a time minimum as to not completely overload the player.
            /*if (spawnTime >= 0.5)
            {
                spawnTime -= 0.25f;
            }*/


            //Randomly increase an aspect of the Slimes to slowly ramp up difficulty at a non-exponential curve
            switch (randDifficulty)
            {
                case (1):
                    healthTotal += 1;
                    //Debug.Log("Health Increased!");
                    break;
                case (2):
                    slimeSpeedOffset += 0.005f;
                    //Debug.Log("Speed Increased!");
                    break;
                case (3):
                    if (spawnTime >= 0.5)
                    {
                        spawnTime -= 0.25f;
                    }
                    //Debug.Log("Spawn Rate Increased!");
                    break;
            }

            //And adjust slime speed and health.
            //healthTotal += 1;
            //slimeSpeedOffset += 0.005f;

            //Then reset the timer so we can continue adjusting the difficulty as the rounds progress.
            globalTimer = 0.0f;
        }
        if (timer > spawnTime)
        {
            //Randomly choose between Fire and Ice
            //Less for a smaller screen

            //Generates 2 random integers. One to determine type, and one to determine movement pattern.
            int randSlime = Random.Range(0, 3);
            GameObject newSlime = slimePrefab;

            //Based on the random numbers that are generated, we create a slime of the corresponding type and movement pattern.
            if (randSlime == 0)
            {
                newSlime = Instantiate(slimePrefabs[0]);

            }
            else if(randSlime == 1)
            {
                newSlime = Instantiate(slimePrefabs[1]);
               
            }
            else if (randSlime == 2)
            {
                newSlime = Instantiate(slimePrefabs[2]);
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

            //And apply any difficulty changes that we have currently implemented.
            newSlime.GetComponent<Slime>().health = healthTotal;
            newSlime.GetComponent<Slime>().slimeSpeed += slimeSpeedOffset;
            newSlime.GetComponent<Slime>().dashSpeed += slimeSpeedOffset;
            slimeList.Add(newSlime);
            timer = 0.0f;
        }
	}
}
