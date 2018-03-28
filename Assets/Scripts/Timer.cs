using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // necessary to access the text component of canvas objects

public class Timer : MonoBehaviour {

    public float timer; // actual time
    public int seconds; // time in seconds
    public int minutes; // time in minutes
    public GameObject timeText; // actual text of the time being displayed on the canvas
    private string niceTime;

	// Use this for initialization
	void Start ()
    {
        timer = 0.0f;
        seconds = 0;
        minutes = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // increment the timer each frame
        timer += Time.deltaTime;

        // convert the timer to minutes and seconds
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        // nice formatting for timer
        niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        // update the canvas text
        timeText.GetComponent<Text>().text = "TIME: " + niceTime + " ";
	}
}
