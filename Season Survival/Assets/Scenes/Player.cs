using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ResourceInventory Inventory;
    private Rigidbody rb;
    public float PickUpRange = 2;
    public float MaxSpeed = 2;
    private Vector3 CurrentSpeed;
    public float Acceleration = 1;
    [Range(0,1)]
    public float Friction = 0.9f;

    // Use this for initialization
    public void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, PickUpRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position+ CurrentSpeed *10);
    }
    // Update is called once per frame
    public void Update()
    {
        Vector3 dir = Vector3.zero;
        // dir +=  new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (CurrentSpeed.x < MaxSpeed)
                CurrentSpeed.x += Acceleration * Time.deltaTime * Input.GetAxis("Horizontal");

        }
        else
        {
            CurrentSpeed = new Vector3(CurrentSpeed.x * Friction, CurrentSpeed.y, CurrentSpeed.z);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (CurrentSpeed.z < MaxSpeed)
                CurrentSpeed.z += Acceleration * Time.deltaTime * Input.GetAxis("Vertical");

        }
        else
        {
            CurrentSpeed = new Vector3(CurrentSpeed.x, CurrentSpeed.y, CurrentSpeed.z * Friction);


        }


        rb.MovePosition(transform.position += CurrentSpeed);
        //0.
        //  rb.AddForce(dir, ForceMode.Acceleration);


        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (Collider c in Physics.OverlapSphere(transform.position, PickUpRange))
            {
                Resource r = c.GetComponent<Resource>();

                if (r != null)
                {
                    if (Inventory.CurrentResource < Inventory.MaxResource)
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
                    if (Inventory.CurrentResource < Inventory.MaxResource && r.CurrentResource >= 1)
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
                Instantiate(ResourceSpawner.ResourcePrefab, transform.position, transform.rotation);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CurrentSpeed *= 4;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }
}
