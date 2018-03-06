﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour {

    //Need reference the GameOver screen
    public GameObject gameOverMenu;

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if ( !(collision.gameObject.name == "FireAmmo(Clone)" || collision.gameObject.name == "IceAmmo(Clone)") ) //Doesn't work for whatever reason
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f; //Causes weird issues with enemy movement at the end
        }
    }
}
