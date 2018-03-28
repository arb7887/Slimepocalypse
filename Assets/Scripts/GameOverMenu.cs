using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        KillCounter.instance.SetScore(0);
        KillCounter.instance.SetTimer(0.0f);
        KillCounter.instance.SetKillCount(0);
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        KillCounter.instance.SetScore(0);
        KillCounter.instance.SetTimer(0.0f);
        KillCounter.instance.SetKillCount(0);
        SceneManager.LoadScene("MainMenu");
    }
}
