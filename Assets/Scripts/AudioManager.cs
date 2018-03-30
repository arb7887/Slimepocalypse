using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip Poppies;
    public AudioClip Spell;
    public AudioClip Straw_Fields;
    public AudioClip Neoishiki;
    AudioSource audioSource;

    private int recentTrack;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        // Start the game with a random audio clip
        chooseRandomAudioClip();
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
        while (chosenClip == recentTrack || chosenClip < 1 || chosenClip > 4)
        {
            chosenClip = Random.Range(0, 10);
        }


        recentTrack = chosenClip; // Save the most recently played clip so that we can avoid playing the same track twice.
        
        switch (chosenClip)
        {
            case 1:
                audioSource.clip = Poppies;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = Spell;
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
        }
    }
    
    // Selects an AudioClip from the list of AudioClips
    public void chooseAudioClip(int chosenClip)
    {
        switch (chosenClip)
        {
            case 1:
                audioSource.clip = Poppies;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = Spell;
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
        }
    }
}
