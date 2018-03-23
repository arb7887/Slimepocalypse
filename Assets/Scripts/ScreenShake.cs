using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public Transform camTransform; // transform of the camera
    public float shakeDuration = 0f; // how long the camera should shake for
    public float shakeAmount = 0.7f; // amplitude of the shake
    public float decreaseFactor = 1.0f; // 
    private Vector3 originalPosition; // stores the original position of the camera transform

    // happens before start
    private void Awake()
    {
        // check to see if the transform is null
        if(camTransform == null)
        {
            // set the transform
            camTransform = gameObject.GetComponent<Transform>() as Transform;
        }
    }

    // happens when the script is enabled
    private void OnEnable()
    {
        originalPosition = camTransform.localPosition;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("Shaking");
		if(shakeDuration > 0)
        {
            // set the screen shake to a random position inside the sphere
            camTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            // decrease the shake duration over time
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // reset the values to turn off the screenshake effect
            shakeDuration = 0f;
            camTransform.localPosition = originalPosition;
        }
	}
}
