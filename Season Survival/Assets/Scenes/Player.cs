using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ResourceInventory Inventory;
    private Rigidbody rb;
    public Animator animator;
    public float PickUpRange = 2;
    public float MaxSpeed = 0.5f;
    private Vector3 CurrentSpeed;
    public float Acceleration = 0.2f;
    [Range(0,1)]
    public float Friction = 0.9f;
    public float InvPercent;
    public bool IsPunching = false;
    // Use this for initialization
    public void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, PickUpRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position+ CurrentSpeed *10);
    }

    public void calcInvPercent()
    {
        InvPercent = Inventory.CurrentResource / Inventory.MaxResource;
    }

    private void doAnimation()
    {
        animator.SetFloat("Velocity", CurrentSpeed.magnitude * 10);
       
        if (Inventory.CurrentResource > 0)
        {
            animator.SetBool("Mouth", true);
        }
        else
        {
            animator.SetBool("Mouth", false);

        }

        if (IsPunching)
        {
            animator.SetTrigger("Punch");
            IsPunching = false;
        }
    }
    // Update is called once per frame
    public void Update()
    {
        //Vector3 dir = Vector3.zero;
     



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

        if (animator != null)
            doAnimation();


        doInput();
    }
    void doInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            IsPunching = true;
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
            IsPunching = true;
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
            /*while (CurrentSpeed.x < MaxSpeed && CurrentSpeed.z < MaxSpeed)
            {
                CurrentSpeed *= 4;
            }*/
            CurrentSpeed *= 4;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }
}
