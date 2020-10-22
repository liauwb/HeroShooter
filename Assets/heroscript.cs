using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroscript{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public int health;
    public float maxSpeed = 5f;
    public Transform player;
    public GameObject playerobj;
    public bool grounded = false;
    public Animator anim;
    public Transform groundCheck;   
    public Rigidbody2D rb2d;
    public float moveForce = 365f;
    public float jumpForce = 1500f;
    public int jumpcount = 0;
    public int can_airjump;
    public float h = 0;
    // Use this for initialization

    public heroscript(string hero_id)
    {
        player = GameObject.FindGameObjectWithTag(hero_id).transform;
        //playerobj = GameObject.FindGameObjectWithTag();
        anim = player.GetComponent<Animator>();
        rb2d = player.GetComponent<Rigidbody2D>();
        groundCheck = player.Find("groundCheck");
    } 
    public bool ground()
    {
        grounded = Physics2D.Linecast(player.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        return grounded;
    }
	// Update is called once per frame
	public void Basic (KeyCode up, KeyCode shoot, KeyCode down) {
        grounded = Physics2D.Linecast(player.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetKeyDown(up) && grounded)
        {
            jump = true;
            jumpcount += 1;
        }
        else if (Input.GetKeyDown(KeyCode.W) && jumpcount < can_airjump)
        {
            jump = true;
            jumpcount += 1;
            //Debug.Log(jumpcount);
        }
        if (jumpcount >= can_airjump)
        {
            if (grounded)
            {
                jumpcount = 0;
            }
        }
        if (Input.GetKeyDown(shoot))
        {
            if (grounded)
                anim.Play("shoot");
            else
                anim.Play("on_air");
            //GameObject temp = Instantiate(GameObject.Find("lightningshot"), GameObject.Find("shotpoint").transform.position, GameObject.Find("shotpoint").transform.rotation);
            //Destroy(temp, 0.5f);
        }

     
           
        
    }
    public void FixedBasic(KeyCode left, KeyCode right, bool hasdash, bool dashing) {
        h = 0;
        if (Input.GetKey(right))
        {
            h = 1;
            if (hasdash && dashing)
                anim.Play("dash");
            else
                anim.Play("walking");
        }
        if (Input.GetKey(left))
        {
            h = -1;
            if (hasdash && dashing)
                anim.Play("dash");
            else
                anim.Play("walking");
        }
        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        if (jump)
        {
            anim.Play("jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;
    }
}
