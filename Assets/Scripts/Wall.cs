﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private int health = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // collision detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if a slime colliders with wall
        if (collision.gameObject.tag == "Slime")
        {
            // decrement health
            health--;

            // destroy slime
            Destroy(collision.gameObject);
        }

        // call the method to update the visual
        updateVisual();

        // if the health of the wall reaches or falls below 0, destroy the wall
        if(health <=0)
        {
            Destroy(gameObject);
        }
    }

    // method to change the sprite based on health
    private void updateVisual()
    {
        if (health == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Manager").GetComponent<WallManager>().wallImage2.GetComponent<SpriteRenderer>().sprite;
            // create a new hitbox for the new visual
            var boxCollider = GetComponent<BoxCollider2D>() as BoxCollider2D;
            boxCollider.size = new Vector2(1.28f,0.5f);
        }
        if (health == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Manager").GetComponent<WallManager>().wallImage3.GetComponent<SpriteRenderer>().sprite;
            // create a new hitbox for the new visual
            var boxCollider = GetComponent<BoxCollider2D>() as BoxCollider2D;
            boxCollider.size = new Vector2(1.28f, 0.2f);
        }
    }
}