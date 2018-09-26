using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInventory : MonoBehaviour
{
    public int MaxResource = 5;
    public int CurrentResource = 0;
    public bool Harvestable = false;
    public bool isPlayer = true;

    public Text text;
	// Use this for initialization
	void Start ()
    {
		
	}
	public void drop(int num)
    {
        for(int i = 0; i< num; i++)
        {
            if(CurrentResource >= num)
            {
                Instantiate(ResourceSpawner.ResourcePrefab, transform.position, transform.rotation);

            }
        }
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
