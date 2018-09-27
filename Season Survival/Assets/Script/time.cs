using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Transform[] Bases;
    public Text EndGameText;
    public Text SeasonText;

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
    // Use this for initialization
    void Start () {
        currentSeason = Season.Winter;
        // ground.GetComponent<MeshRenderer>().material.color = Color.red;
        currentTime = SeasonTime;
        ground.GetComponent<MeshRenderer>().material.color = Winter;
    }
	public void GameOver()
    {
        int WinnerIndex = 0;
        int Winnercount = 0 ;
        for (int i = 0; i < Bases.Length;i++) 
        {
            int num = 0;
            foreach (Collider c in Physics.OverlapSphere(Bases[i].position, 3))
            {
                Resource r = c.GetComponent<Resource>();

                if (r != null)
                {
                    num++;
                }
            }
            if(num > Winnercount)
            {
                Winnercount = num;
                WinnerIndex = i;
            }
        }
        Debug.Log("winner is " + Bases[WinnerIndex].name.ToString());
        if (EndGameText != null)
        {
            EndGameText.transform.parent.gameObject.SetActive(true);
            EndGameText.text = "winner is " + Bases[WinnerIndex].name.ToString();

        }
    }
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        SeasonText.text = currentSeason.ToString();
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
                        GameOver();

                    }
                    break;
            }
        }
    }
}
