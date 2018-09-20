using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ResourceInventory Inventory;
    private Rigidbody rb;
    public float PickUpRange = 2;
    // Use this for initialization
    public void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, PickUpRange);
    }
    // Update is called once per frame
    public void Update()
    {
        Vector3 dir = Vector3.zero;
        dir +=  new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        rb.AddForce(dir, ForceMode.Acceleration);


        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach(Collider c in Physics.OverlapSphere(transform.position, PickUpRange))
            {
                Resource r = c.GetComponent<Resource>();

                if (r != null)
                {
                    if(Inventory.CurrentResource < Inventory.MaxResource)
                    {
                        Inventory.CurrentResource++;
                        Destroy(c.gameObject);
                    }
                  
                    break;
                }
            }
           
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (Collider c in Physics.OverlapSphere(transform.position, PickUpRange))
            {
                ResourceInventory r = c.GetComponent<ResourceInventory>();

                if (r != null)
                {
                    if (Inventory.CurrentResource < Inventory.MaxResource && r.CurrentResource >=1)
                    {
                        Inventory.CurrentResource++;
                        r.CurrentResource--;
                    }

                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Inventory.CurrentResource >= 1)
            {
                Inventory.CurrentResource--;
                Instantiate(ResourceSpawner.ResourcePrefab,transform.position,transform.rotation);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
