using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour {

    public float timeLeft = 10f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            print("Time Over!");
        }
    }
}
