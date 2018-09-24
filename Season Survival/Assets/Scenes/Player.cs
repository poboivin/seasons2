using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber {  P1,P2,P3,P4}
    public PlayerNumber player;
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
    public bool IsFlipped = false;

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
        animator.SetFloat("Velocity", CurrentSpeed.magnitude * 20);
        if(CurrentSpeed.x < 0 && IsFlipped == false)
        {
            IsFlipped = true;
            animator.gameObject.transform.localScale = new Vector3(animator.gameObject.transform.localScale.x *-1f, animator.gameObject.transform.localScale.y, animator.gameObject.transform.localScale.z);
        }
        if (CurrentSpeed.x > 0 && IsFlipped == true)
        {
            IsFlipped = false;
            animator.gameObject.transform.localScale = new Vector3(animator.gameObject.transform.localScale.x * -1f, animator.gameObject.transform.localScale.y, animator.gameObject.transform.localScale.z);
        }
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

    public float GetAxis(string axisName)
    {

        return Input.GetAxis(player.ToString()+axisName);
    }
    // Update is called once per frame
    public void Update()
    {
        //Vector3 dir = Vector3.zero;
     



        if (GetAxis("Horizontal") != 0)
        {
            if (CurrentSpeed.x < MaxSpeed)
                CurrentSpeed.x += Acceleration * Time.deltaTime * GetAxis("Horizontal");

        }
        else
        {
            CurrentSpeed = new Vector3(CurrentSpeed.x * Friction, CurrentSpeed.y, CurrentSpeed.z);
        }
        if (GetAxis("Vertical") != 0)
        {
            if (CurrentSpeed.z < MaxSpeed)
                CurrentSpeed.z += Acceleration * Time.deltaTime * GetAxis("Vertical");

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

    bool GetButtonDown(string buttonName)
    {
       
        return Input.GetButtonDown(player.ToString() + buttonName);
    }
    void doInput()
    {
        if (GetButtonDown("Fire1"))
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
        if (GetButtonDown("Fire2"))
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
        if (GetButtonDown("Fire3"))
        {
            if (Inventory.CurrentResource >= 1)
            {
                Inventory.CurrentResource--;
                Instantiate(ResourceSpawner.ResourcePrefab, transform.position, transform.rotation);
            }
        }
        if (GetButtonDown("Jump"))
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
