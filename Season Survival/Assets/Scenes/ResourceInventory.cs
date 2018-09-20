using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInventory : MonoBehaviour
{
    public int MaxResource = 5;
    public int CurrentResource = 0;
    public bool Harvestable = false;
    public Text text;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(text!= null)
        {
            text.text = CurrentResource.ToString() + "/" + MaxResource.ToString();
        }
	}
}
