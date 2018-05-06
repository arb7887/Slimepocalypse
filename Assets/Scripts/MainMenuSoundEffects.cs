using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundEffects : MonoBehaviour {

    // audio variables
    private AudioSource source;
    public AudioClip clickSound;
    public AudioClip gameOverJingle;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // method to play a sound effect from a button
    public void PlaySoundEffect()
    {
        // play the sound
        source.PlayOneShot(clickSound,3.0f);
    }

    public void PlayGameOverJingle()
    {
        // play the sound
        source.PlayOneShot(gameOverJingle, 1.0f);
    }
}
