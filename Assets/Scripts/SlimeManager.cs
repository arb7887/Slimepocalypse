using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    public GameObject slimePrefab;
    public List<GameObject> slimeList;
    float timer;

    // Use this for initialization
    void Start () {
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            int random = Random.Range(1, 8);
            GameObject newSlime = Instantiate(slimePrefab);
            switch (random)
            { 
                case (1):
                    newSlime.transform.position = new Vector3(-10.0f, 3.6f, 0.0f);
                    break;
                case (2):
                    newSlime.transform.position = new Vector3(-7.5f, 3.6f, 0.0f);
                    break;
                case (3):
                    newSlime.transform.position = new Vector3(-5.0f, 3.6f, 0.0f);
                    break;
                case (4):
                    newSlime.transform.position = new Vector3(-2.5f, 3.6f, 0.0f);
                    break;
                case (5):
                    newSlime.transform.position = new Vector3(2.5f, 3.6f, 0.0f);
                    break;
                case (6):
                    newSlime.transform.position = new Vector3(5.0f, 3.6f, 0.0f);
                    break;
                case (7):
                    newSlime.transform.position = new Vector3(7.5f, 3.6f, 0.0f);
                    break;
                case (8):
                    newSlime.transform.position = new Vector3(10.0f, 3.6f, 0.0f);
                    break;
            }
            slimeList.Add(newSlime);
            timer = 0.0f;
        }
	}
}
