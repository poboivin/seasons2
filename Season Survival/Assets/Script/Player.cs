using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber {  P1,P2,P3,P4, PC}
    public PlayerNumber player;
    public ResourceInventory Inventory;
    private Rigidbody rb;
    public Animator animator;
    public float PickUpRange = 2;
   // public float MaxSpeed = 0.5f;
   // private Vector3 CurrentSpeed;
    public float Acceleration = 1f;
    public float RestingDrag = 5f;
    public float MovingDrag = 0;
    public float Mass;
    public float DashCoolDown = 2f;
    public float FoodWeight = 1.5f;
    public float dashTimer = 2f;
    // [Range(0,1)]
    // public float Friction = 0.9f;
    // public float InvPercent;
    public bool IsPunching = false;
    public bool IsFlipped = false;
    Vector3 dir;
    // Use this for initialization
    public void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        Mass = rb.mass;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, PickUpRange);
        Gizmos.color = Color.red;
   //     Gizmos.DrawLine(transform.position, transform.position+ CurrentSpeed *10);
    }

  

    private void doAnimation()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude );
        if(rb.velocity.x < 0 && IsFlipped == false)
        {
            IsFlipped = true;
            animator.gameObject.transform.localScale = new Vector3(animator.gameObject.transform.localScale.x *-1f, animator.gameObject.transform.localScale.y, animator.gameObject.transform.localScale.z);
        }
        if (rb.velocity.x > 0 && IsFlipped == true)
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
        if (player != PlayerNumber.PC)
        {
            return Input.GetAxis(player.ToString() + axisName);

        }
        else
        {
            return Input.GetAxis(axisName);

        }
    }
    //fixed updated is called once per physic step
    public void FixedUpdate()
    {
        rb.AddForce(dir * Acceleration, ForceMode.Force);
    }
    // Update is called once per frame

    public void Update()
    {
        rb.mass = Mass + FoodWeight *Inventory.CurrentResource;

        //physics
        if (GetAxis("Horizontal") == 0 && GetAxis("Vertical") == 0)
        {
            rb.drag = RestingDrag;
        }
        else
        {
            rb.drag = MovingDrag;
        }
       


        if (animator != null)
            doAnimation();
        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }

        //button input
        doInput();
    }

    bool GetButtonDown(string buttonName)
    {
        if (player != PlayerNumber.PC)
        {
            return Input.GetButtonDown(player.ToString() + buttonName);

        }
        else
        {
            return Input.GetButtonDown( buttonName);

        }
    }
    void doInput()
    {
        //joystick input
        dir = new Vector3(GetAxis("Horizontal"), 0, GetAxis("Vertical"));

        //pick up 
        if (GetButtonDown("Fire1"))
        {
            rb.velocity = Vector3.zero;
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
        //hit
        if (GetButtonDown("Fire2"))
        {
            Debug.Log(gameObject);

            foreach (Collider c in Physics.OverlapSphere(transform.position, PickUpRange))
            {
                ResourceInventory r = c.GetComponent<ResourceInventory>();

                if (r != null && r.transform != this.transform)
                {
                    IsPunching = true;

                    if (Inventory.CurrentResource < Inventory.MaxResource && r.CurrentResource >= 1)
                    {
                        r.Drop(1,Vector3.Lerp(this.transform.position,r.transform.position,0.5f));
                        //Inventory.CurrentResource++;
                        //Mass -= 1;
                       // r.CurrentResource--;
                    }

                    break;
                }
            }
        }
        //drop
        if (GetButtonDown("Fire3"))
        {
            Debug.Log(gameObject);

            if (Inventory.CurrentResource >= 1)
            {
                Inventory.CurrentResource--;
                Instantiate(ResourceSpawner.ResourcePrefab, transform.position, transform.rotation);
            }
        }
        //dash
        if (GetButtonDown("Jump") && dashTimer <= 0)
        {
            rb.AddForce(dir * Acceleration, ForceMode.VelocityChange);
            dashTimer = DashCoolDown;
        }
    }
   
    public void OnTriggerEnter(Collider other)
    {
        
    }
}
