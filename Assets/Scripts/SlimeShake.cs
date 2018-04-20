using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code based on screenshake class
public class SlimeShake : MonoBehaviour {

    public Transform slimeTransform; // transform of the camera
    public float shakeDuration = 0.0f; // how long the camera should shake for
    private float shakeAmount = 0.05f; // amplitude of the shake
    public float decreaseFactor = 1.0f; // how fast it depletes
    private Vector3 originalPosition; // stores the original position of the camera transform

    // happens before start
    private void Awake()
    {
        // check to see if the transform is null
        if (slimeTransform == null)
        {
            // set the transform
            slimeTransform = gameObject.GetComponent<Transform>() as Transform;
        }
    }

    // happens when the script is enabled
    private void OnEnable()
    {
        originalPosition = slimeTransform.localPosition;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // updates the position
        originalPosition = slimeTransform.localPosition;
        // only keep shaking if the game is not over
        if (!SlimeManagerSingleton.Instance.isGameOver)
        {
            if (gameObject.GetComponent<Slime>().shaking)
            {
                //Debug.Log("Shaking");
                if (shakeDuration > 0)
                {
                    // set the screen shake to a random position inside the sphere
                    slimeTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

                    // decrease the shake duration over time
                    shakeDuration -= Time.deltaTime * decreaseFactor;
                }
                else
                {
                    // reset the values to turn off the screenshake effect
                    shakeDuration = 0f;
                    slimeTransform.localPosition = originalPosition;
                    gameObject.GetComponent<Slime>().shaking = false;
                }
            }
        }
    }
}
