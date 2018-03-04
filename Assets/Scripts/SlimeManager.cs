﻿using System.Collections;
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

    // Use this for initialization
    void Start () {
        timer = 0.0f;
        globalTimer = 0.0f;
        spawnTime = 2.0f;
        healthTotal = 3;
        slimeSpeedOffset = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        globalTimer += Time.deltaTime;
        //Once the time for whatever we determine one round to be passes...
        if (globalTimer > 30.0f)
        {
            //We lower the spawn rate, but only until it reaches a time minimum as to not completely overload the player.
            if (spawnTime >= 0.5)
            {
                spawnTime -= 0.25f;
            }
            //And adjust slime speed and health.
            healthTotal += 1;
            slimeSpeedOffset += 0.01f;

            //Then reset the timer so we can continue adjusting the difficulty as the rounds progress.
            globalTimer = 0.0f;
        }
        if (timer > spawnTime)
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

            //Generates 2 random integers. One to determine type, and one to determine movement pattern.
            int randSlime = Random.Range(0, 3);
            int moveType = Random.Range(0, 2);
            GameObject newSlime = null;

            //Based on the random numbers that are generated, we create a slime of the corresponding type and movement pattern.
            if (randSlime == 0)
            {
                if (moveType == 0)
                {
                    newSlime = Instantiate(slimePrefabs[0]);
                }
                else
                {
                    newSlime = Instantiate(slimePrefabs[1]);
                }
            }
            else if(randSlime == 1)
            {
                if (moveType == 0)
                {
                    newSlime = Instantiate(slimePrefabs[2]);
                }
                else
                {
                    newSlime = Instantiate(slimePrefabs[3]);
                }
            }
            //And apply any difficulty changes that we have currently implemented.
            newSlime.GetComponent<Slime>().health = healthTotal;
            newSlime.GetComponent<Slime>().slimeSpeed += slimeSpeedOffset;
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
