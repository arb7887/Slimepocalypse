using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameObject jukeboxSE;
    // Use this for initialization
    void Start () {
        jukeboxSE.GetComponent<MainMenuSoundEffects>().PlayGameOverJingle();
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
        KillCounter.instance.ResetBeserkStateVariables();
        KillCounter.instance.SaveHighScore(KillCounter.instance.currentScore);
        KillCounter.instance.LoadHighScore();
        KillCounter.instance.SetCurrentScore(0);
        SlimeManagerSingleton.Instance.Reset();
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        KillCounter.instance.SetScore(0);
        KillCounter.instance.SetTimer(0.0f);
        KillCounter.instance.SetKillCount(0);
        KillCounter.instance.ResetBeserkStateVariables();
        KillCounter.instance.SaveHighScore(KillCounter.instance.currentScore);
        SlimeManagerSingleton.Instance.Reset();
        SceneManager.LoadScene("MainMenu");
    }
}
