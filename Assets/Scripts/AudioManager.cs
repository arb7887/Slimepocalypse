using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip Spell;
    public AudioClip Poppies;
    public AudioClip Straw_Fields;
    public AudioClip Neoishiki;
    public AudioClip Juglar_Street;
    AudioSource audioSource;

    private int recentTrack;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();


        // Choose a startup song (Spell for now)
        audioSource.clip = Spell;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
		if(audioSource.clip.length <= audioSource.time)
        {
            chooseRandomAudioClip();
        }
	}
    

    // Randomly Selects an AudioClip
    public void chooseRandomAudioClip()
    {

        // Check if we chose the same clip twice
        int chosenClip = Random.Range(0, 10);
        while (chosenClip == recentTrack || chosenClip < 1 || chosenClip > 5)
        {
            chosenClip = Random.Range(0, 10);
        }


        Debug.Log("Is Game Over: " + SlimeManagerSingleton.Instance.isGameOver);

        // If we're in the main menu, always play the main menu song.
        if (SlimeManagerSingleton.Instance.isGameOver)
        {
            chosenClip = 1;
        }

        Debug.Log("Clip: " + chosenClip);

        recentTrack = chosenClip; // Save the most recently played clip so that we can avoid playing the same track twice.

        switch (chosenClip)
        {
            case 1:
                audioSource.clip = Spell;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = Poppies;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = Straw_Fields;
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = Neoishiki;
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = Juglar_Street;
                audioSource.Play();
                break;
        }
    }

    // Selects an AudioClip from the list of AudioClips
    public void chooseAudioClip(int chosenClip)
    {
        switch (chosenClip)
        {
            case 1:
                audioSource.clip = Spell;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = Poppies;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = Straw_Fields;
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = Neoishiki;
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = Juglar_Street;
                audioSource.Play();
                break;
        }
    }
}
