using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero1 : MonoBehaviour {
    private bool dash;
    public bool faceright = true;
    public mobility skill1;
    public GameObject shot;
    public GameObject shotpoint;
    public string axisname;
    public int health;
    public int can_airjump;
    public float dashcd;
    public float temptime = -6f;
    public Transform player;
    public Transform groundCheck;
    public Rigidbody2D rb2d;
    public bool grounded = false;
    public bool jump = false;
    public int jumpcount = 0;
    public Animator anim;
    public bool hasdash;
    public bool canfire;
    public float h;
    public float maxSpeed = 5f;
    public float moveForce = 365f;
    public float jumpForce = 20f;
    public bool rapidfire;
    public int ammo;
    public int initialammo;
    public float initialshottime;
    public float shottime;
    public float rechargetime;
    public float tempshottime = -6f;
    public bool ready;
    public int maxhealth;
    string up, shoot, skill, down;
    float y;
    bool doublepress = false;
    float doublepresstime;
    float doublepresscd = -6;
    float doublepressduration = 0;
    
    // Use this for initialization
    void Start () {
        anim = player.GetComponent<Animator>();
        rb2d = player.GetComponent<Rigidbody2D>();
        //skill1 = player.GetComponent<mobility>();
        ammo = initialammo;
        shottime = initialshottime;
        canfire = true;
        if (PlayerPrefs.GetInt("UseKeyboard"+ gameObject.transform.parent.gameObject.name[6])== 0)
        {
            axisname = "KeyboardHorizontal" + gameObject.transform.parent.gameObject.name[6];
            up = "KeyboardJump" + gameObject.transform.parent.gameObject.name[6];
            down = "KeyboardVertical" + gameObject.transform.parent.gameObject.name[6];
            shoot = "KeyboardShoot" + gameObject.transform.parent.gameObject.name[6];
            skill = "KeyboardSkill" + gameObject.transform.parent.gameObject.name[6];
        }
        else
        {
            axisname = "Horizontal" + PlayerPrefs.GetInt("UseKeyboard" + gameObject.transform.parent.gameObject.name[6]);
            up = "Jump" + PlayerPrefs.GetInt("UseKeyboard" + gameObject.transform.parent.gameObject.name[6]);
            shoot = "Shoot" + PlayerPrefs.GetInt("UseKeyboard" + gameObject.transform.parent.gameObject.name[6]);
            skill = "Skill" + PlayerPrefs.GetInt("UseKeyboard" + gameObject.transform.parent.gameObject.name[6]);
            down = "Vertical" + PlayerPrefs.GetInt("UseKeyboard" + gameObject.transform.parent.gameObject.name[6]);
        }
        gameObject.tag = gameObject.transform.parent.gameObject.tag;
        //groundCheck = player.FindChild("groundCheck");
        ready = false;
        maxhealth = health;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (health > 0 && ready)
        {
            int mask1 = 1 << LayerMask.NameToLayer("Ground");
            int mask2 = 1 << LayerMask.NameToLayer("LowestGround");
            int mask = mask1 | mask2;
            grounded = Physics2D.Linecast(player.transform.position, groundCheck.position,mask); //check if player is touching ground
            if (rb2d.velocity.y >0)
            {
               //d Debug.Log("not grounded");
                gameObject.layer = 10;
            }
            else if(doublepressduration < Time.time)
            {
                gameObject.layer = 9;
            }

            if (Input.GetAxisRaw(down) == -1 && !doublepress)
            {
                doublepress = true;
            }
            else if (Input.GetAxisRaw(down) == 0 && doublepress == true)
            {
                doublepresstime = Time.time + 0.5f;
            }
            else if (Input.GetAxisRaw(down) == -1 && doublepresstime > Time.time)
            {
                doublepress = false;
                //Debug.Log("double press");
                gameObject.layer = 10;
                doublepressduration = Time.time + 0.5f;
                //Debug.Log("doublepressduration " + doublepressduration);
            }
            if (Input.GetAxisRaw(down) == 0 && doublepressduration < Time.time)
            {
               
                gameObject.layer = 9;
            }

            if (Input.GetButtonDown(up) && grounded)
            {
                
                jump = true;
                jumpcount += 1;
            } // jump
            else if (Input.GetButtonDown(up) && jumpcount < can_airjump)
            {
                jump = true;
                jumpcount += 1;

            }//jump multiple times
            
            if (jumpcount >= can_airjump)
            {
                if (grounded)
                {
                    jumpcount = 0;
                }
            }// reset jump once grounded

            //does shoot on key press or rapid fire when you hold it. For now we have these 2 types. Might move to another function once we decide theres more types of shots
            if (Input.GetButtonDown(shoot))
            {
               // Debug.Log(ammo);
                if (!rapidfire && ammo > 0)
                {
                    if (grounded)
                        anim.Play("shoot");
                    else
                        anim.Play("on_air");
                    GameObject temp = Instantiate(shot, shotpoint.transform.position, shot.transform.rotation);
                    //temp.transform.parent = gameObject.transform;
                    //Destroy(temp, 6f);
                    ammo--;
                    tempshottime = Time.time;
                }
            }
            if (Input.GetButton(shoot) && rapidfire && Time.time - shottime < 1)
            {
                GameObject temp = Instantiate(shot, shotpoint.transform.position, shot.transform.rotation);
                Destroy(temp, 6f);
            }

            //use mobility skill
            if (Input.GetButtonDown(skill) && hasdash)
            {
                dash = skill1.useskill(grounded, dashcd);
            }
        }
        //kill player
        if(health <= 0)
        {
            anim.Play("death");
            GameObject particle = Instantiate(GameObject.Find("DeathParticle"), gameObject.transform.position, gameObject.transform.rotation);
            particle.GetComponent<ParticleSystem>().Play();
            StartCoroutine(dead());
            //
        }
        
        //recharge shot cooldown
        if (Time.time > tempshottime + rechargetime)
        {
            //Debug.Log("reloaded");
            ammo = initialammo;
            shottime = initialshottime;
        }
        
    }
    private void FixedUpdate()
    {
        if (health > 0 && ready)
        {
            h = 0;
            //play dash animation
            if (hasdash && dash)
                anim.Play("dash");
            //move player to left or right
            h = Input.GetAxisRaw(axisname);
            if(h!=0 && !dash)
            {
                //Debug.Log("h = " + h);
                anim.Play("walking");
            }
            
            
            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);
            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

            //flip body when turning
            if (h > 0 && !faceright)
            {
                Flip();

            }
            else if (h < 0)
            {

                if (faceright)
                {
                    Flip();
                    //print("shouldbeflipped");
                }
            }

            //add force so that player jumps and play animation
            if (jump)
            {
                anim.Play("jump");
                rb2d.velocity = (new Vector2(0f, jumpForce));
                jump = false;
            }

            //update the player based on the skill 
            dash = skill1.skillupdate(rb2d, h);
            //Debug.Log(dash);
        }
    }
    
    void Flip()
    {
        faceright = !faceright;
        //print("flippedtoleft" + faceright);
        Vector3 theScale = player.transform.localScale;
        //print("prior scale = " + player.transform.localScale);
        theScale.x *= -1;
        player.transform.localScale = theScale;
        
    }
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1);
       
        gameObject.SetActive(false);
    }
}
