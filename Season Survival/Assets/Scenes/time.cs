using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour {

    public float timeLeft = 10f;
    public GameObject ground;
    
    // Use this for initialization
    void Start () {
       // ground.GetComponent<MeshRenderer>().material.color = Color.red;
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
