using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour {

    //Need reference the GameOver screen
    public GameObject gameOverMenu;
    public GameObject manager;
    public GameObject jukeboxSE;

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if ( !(collision.gameObject.name == "FireAmmo(Clone)" || collision.gameObject.name == "IceAmmo(Clone)") ) //Doesn't work for whatever reason
        {
            manager.GetComponent<SlimeManager>().stopSlimes();
            gameOverMenu.SetActive(true);

            // get the jukebox script to play the gameover jingle
            jukeboxSE.GetComponent<MainMenuSoundEffects>().PlayGameOverJingle();

            Time.timeScale = 0f; //Causes weird issues with enemy movement at the end
            manager.GetComponent<HighScore>().SaveHighScore(KillCounter.instance.currentScore);
        }
    }
}
