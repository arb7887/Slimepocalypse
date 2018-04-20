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
        SceneManager.LoadScene("Main");
        KillCounter.instance.Reset();
        KillCounter.instance.superShotImage = GameObject.Find("SuperShotUI");
        SlimeManagerSingleton.Instance.Reset();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        KillCounter.instance.Reset();
        SlimeManagerSingleton.Instance.Reset();
    }
}
