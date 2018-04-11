using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomSlime : Slime {

    public int childHealth;
    public GameObject slimePrefab;

    // Use this for initialization
    void Start () {
        setInitialSize();
        slimeSpeed = 0.01f;
        dashSpeed = slimeSpeed * 3.0f;
        dashTimer = Random.Range(0.3f, 0.7f);
        delayTimer = Random.Range(1.2f, 1.5f);
        laneTimer = 0.0f;
        reachedLane = true;
        float random = Random.Range(0.0f, 1.0f);
        health = 1;
        canMove = true;
    }

    //Altered damage taking method to include children spawning on death.
    public override void TakeDamage(int damage)
    {
        // decrease health by 1
        health -= damage;

        // check to make sure the slime is still alive
        if (health <= 0)
        {
            SpawnChildren();
            // get audio source
            AudioSource source = GetComponentInParent<AudioSource>();
            AudioClip deathClip = GetComponentInParent<Slime>().deathSound;
            source.clip = deathSound;
            source.Play();

            // give the illusion of death
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // set the slime to dead
            isDead = true;

            /*
            for (int i = 0; i < manager.GetComponent<SlimeManager>().slimeList.Count; i++)
            {
                if (manager.GetComponent<SlimeManager>().slimeList[i] == gameObject)
                {
                    manager.GetComponent<SlimeManager>().slimeList.RemoveAt(i);
                    break;
                }
            }
            // destroy the slime when healt is zero
            Destroy(gameObject);

            // Add to the kill counter
            KillCounter.instance.AddKillToCount();
            */
        }
    }

    //Method to spawn 4 normal slimes when this slime dies.
    void SpawnChildren()
    {
        //Gets a int representation of current type for type assignment.
        int newType = 0;
        if (type == "ice")
        {
            newType = 0;
        }
        else
        {
            newType = 1;
        }
        //Different spawning paradigm for mother slime that is on one of the end lanes. (2 in same line, 2 in adjacent lane)
        //Sets the lane, type, and movement type of each child slime.
        if (currentLane == 1)
        {
            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x + 1.2f, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane + 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x + 1.2f, gameObject.transform.position.y + 1.2f, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane + 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            /*
            newSlime = Instantiate(slimePrefab);
            newSlime.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
            newSlime.GetComponent<Slime>().SetLanes(currentLane);
            newSlime.GetComponent<Slime>().SetType(newType);
            newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            newSlime.GetComponent<Slime>().manager = gameObject;
            manager.GetComponent<SlimeManager>().slimeList.Add(newSlime);

            newSlime = Instantiate(slimePrefab);
            newSlime.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, 0.0f);
            newSlime.GetComponent<Slime>().SetLanes(currentLane);
            newSlime.GetComponent<Slime>().SetType(newType);
            newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            newSlime.GetComponent<Slime>().manager = gameObject;
            manager.GetComponent<SlimeManager>().slimeList.Add(newSlime);

            newSlime = Instantiate(slimePrefab);
            newSlime.transform.position = new Vector3(gameObject.transform.position.x + 1.2f, gameObject.transform.position.y, 0.0f);
            newSlime.GetComponent<Slime>().SetLanes(currentLane + 1);
            newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            newSlime.GetComponent<Slime>().SetType(newType);
            newSlime.GetComponent<Slime>().manager = gameObject;
            manager.GetComponent<SlimeManager>().slimeList.Add(newSlime);

            newSlime = Instantiate(slimePrefab);
            newSlime.transform.position = new Vector3(gameObject.transform.position.x + 1.2f, gameObject.transform.position.y + 1.2f, 0.0f);
            newSlime.GetComponent<Slime>().SetLanes(currentLane + 1);
            newSlime.GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            newSlime.GetComponent<Slime>().SetType(newType);
            newSlime.GetComponent<Slime>().manager = gameObject;
            manager.GetComponent<SlimeManager>().slimeList.Add(newSlime);*/

        }
        else if (currentLane == 4)
        {
            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x - 1.2f, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane - 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1] = Instantiate(slimePrefab);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x - 1.2f, gameObject.transform.position.y + 1.2f, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane - 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;
        }
        //Otherwise, normal spawning paradigm (T block shape)
        else
        {
            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x - 1.2f, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane - 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;

            manager.GetComponent<SlimeManager>().slimeList.Add(Instantiate(slimePrefab));
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].transform.position = new Vector3(gameObject.transform.position.x + 1.2f, gameObject.transform.position.y, 0.0f);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetLanes(currentLane + 1);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().activeMovetypes.Add(Slime.MoveTypes.Normal);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().SetType(newType);
            manager.GetComponent<SlimeManager>().slimeList[manager.GetComponent<SlimeManager>().slimeList.Count - 1].GetComponent<Slime>().manager = manager;
        }
    }
}
