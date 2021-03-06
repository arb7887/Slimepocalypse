﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameObject jukeboxSE;
    public GameObject manager;
    // Use this for initialization
    void Start () {
        jukeboxSE.GetComponent<MainMenuSoundEffects>().PlayGameOverJingle();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene("Main");
        KillCounter.instance.Reset();
        KillCounter.instance.superShotImage = GameObject.Find("SuperShotUI");
        // kill the slimes before reseting the singleton instance
        foreach (GameObject slime in SlimeManagerSingleton.Instance.slimeList)
        {
            Destroy(slime);
        }
        SlimeManagerSingleton.Instance.Reset();
        manager.GetComponent<WallManager>().resetWalls();
        SlimeManagerSingleton.Instance.isGameOver = false;
        UnityEngine.Analytics.Analytics.CustomEvent("Replay game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene("MainMenu");
        SlimeManagerSingleton.Instance.isGameOver = true;
        KillCounter.instance.Reset();
        KillCounter.instance.superShotImage = GameObject.Find("SuperShotUI");
        SlimeManagerSingleton.Instance.Reset();
        manager.GetComponent<WallManager>().resetWalls();
    }
}
