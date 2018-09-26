using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour {

    public float TransitionTime = 20;
    public float SeasonTime = 100;
    public GameObject ground;
    public Color Winter;
    public Color Spring;
    public Color Summer;
    public Color Fall;
    public float currentTime;
    public enum Season {  Winter,Spring,Summer,Fall};
    public Season currentSeason = Season.Winter;
    public bool Transition;
    // Use this for initialization
    void Start () {
        currentSeason = Season.Winter;
        // ground.GetComponent<MeshRenderer>().material.color = Color.red;
        currentTime = SeasonTime;
        ground.GetComponent<MeshRenderer>().material.color = Winter;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;

        if (currentTime < 0 && Transition == false)
        {
            Transition = true;
            currentTime = TransitionTime;
        }

        if (Transition)
        {
            switch (currentSeason)
            {
                case Season.Winter:
                    ground.GetComponent<MeshRenderer>().material.color = Color.Lerp(Spring, Winter, currentTime / TransitionTime);
                    if (currentTime < 0)
                    {
                        currentSeason = Season.Spring;
                        Transition = false;
                        currentTime = SeasonTime;
                    }
                    break;
                case Season.Spring:
                    ground.GetComponent<MeshRenderer>().material.color = Color.Lerp(Summer, Spring, currentTime / TransitionTime);
                    if (currentTime < 0)
                    {
                        currentSeason = Season.Summer;
                        Transition = false;
                        currentTime = SeasonTime;
                    }
                    break;
                case Season.Summer:
                    ground.GetComponent<MeshRenderer>().material.color = Color.Lerp(Fall, Summer, currentTime / TransitionTime);
                    if (currentTime < 0)
                    {
                        currentSeason = Season.Fall;
                        Transition = false;
                        currentTime = SeasonTime;
                    }
                    break;
                case Season.Fall:
                    ground.GetComponent<MeshRenderer>().material.color = Color.Lerp(Winter, Fall, currentTime / TransitionTime);
                    if (currentTime < 0)
                    {
                        currentSeason = Season.Winter;
                        Transition = false;
                        currentTime = SeasonTime;
                    }
                    break;
            }
        }
    }
}
