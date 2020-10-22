using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public bool attack1 = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public int health = 3;
    public GameObject teleparticle;
    private Transform player;
    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    public bool can_dash = true;
    public int dashcount = -1;
    public int can_airjump = 2;
    public bool can_airdash = true;
    public int jumpcount = 0;
    public bool can_teleport;
    public float timestamp = 0f;
    public Vector3 spawnpoint;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player2").transform;
        teleparticle = GameObject.Find("Particle System");
        teleparticle.GetComponent<ParticleSystem>();
        timestamp = 0;
        spawnpoint = player.transform.position;
        can_teleport = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        grounded = Physics2D.Linecast(player.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
      // Debug.Log("player2 grounded = " + grounded);
        if (can_teleport && timestamp <= Time.time)
        {
            //Debug.Log("timestamp = " + timestamp + " time = " + Time.time);
            if (Input.GetMouseButtonDown(0))
                {
                Vector3 temp = player.position;
                teleparticle.transform.position = temp;
                teleparticle.GetComponent<ParticleSystem>().Play();
                
                player.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("temp = " + temp);
                Debug.Log("pos = " + player.position);
                if(temp.x < player.position.x && !facingRight)
                {
                    Flip();
                }
                else if(temp.x > player.position.x && facingRight)
                {
                    Flip();
                }
                //teleparticle.GetComponent<ParticleSystem>().Stop();
                timestamp = Time.time + 3;

            }



        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump = true;
            jumpcount+=1;
            Debug.Log(jumpcount);
            Debug.Log("player2 grounded = " + grounded);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && jumpcount < can_airjump) {
            jump = true;
            jumpcount+=1;
            
        }
        if(jumpcount >= can_airjump)
        {
            if (grounded)
            {
                jumpcount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && can_dash)
        {
            Debug.Log("pressed");
                if (can_airdash)
                {
                    dashcount = 0;
                }
                else if (grounded)
                    {
                    dashcount = 0;
                }
        }
        if(player.position.y < -10)
        {   
            health = 0;
        }
        if (attack1)
        {
            anim.Play("attack1");

            attack1 = false;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            attack1 = true;
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameObject temp = Instantiate(GameObject.Find("cannonball2"), GameObject.Find("leftHand2").transform.position, GameObject.Find("leftHand").transform.rotation);
            Destroy(temp, 0.5f);
        }


    }

    void FixedUpdate()
    {
        float h = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            h = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            h = -1;
        }
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);
        
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        if (dashcount <10 && dashcount >= 0) {
            rb2d.AddForce(Vector2.right * h * 3000f);
            dashcount++;
        }
        if(dashcount > 10)
        {
            dashcount = -1;
        }
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        
        
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
