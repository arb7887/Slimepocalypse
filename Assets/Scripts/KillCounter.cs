using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour {

    private int killCount = 0; // Counts how many slimes have been killed
    private bool fifthShot = false; // Tells if this is a super slime

    private static KillCounter _instance;

    public static KillCounter instance
    {
        get {

            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<KillCounter>();

                DontDestroyOnLoad(_instance.gameObject);

                _instance.killCount = _instance.GetComponent<int>();
                _instance.fifthShot = _instance.GetComponent<bool>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }


    // Adds to the kill count.
    public void AddKillToCount()
    {
        // Add to the killCount
        killCount++;
    }

    // Gets the kill count.
    public int GetKillCount()
    {
        // Add to the killCount
        return killCount;
    }

    // If the kill count is divisible by 5, the next shot will be a supershot.
    public bool IsSuperShot()
    {
        if (killCount % 5 == 0 && killCount != 0)
        {
            fifthShot = true;
            killCount = 0;
        }
        else fifthShot = false;

        return fifthShot;
    }

}
