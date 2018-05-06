using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
    {
        SlimeManagerSingleton.Instance.Reset();
        SlimeManagerSingleton.Instance.isGameOver = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
