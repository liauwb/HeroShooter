using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
    public GameObject Hero;
    public GameObject Boss;
    public Vector3 pos;
    public float temp;
    public float timelive=2;
    public float speed;
    public Rigidbody2D rb2d;
    public bool faceright;
    public bool ignorewall = false;
    public int damage;
    // Use this for initialization
    void Flip()
    {
        faceright = !faceright;
        Vector3 theScale = gameObject.transform.localScale;
        theScale.x *= -1;
        gameObject.transform.localScale = theScale;
    }
    void Start () {
        //Hero = GameObject.Find("player1");
        rb2d = GetComponent<Rigidbody2D>();
        if (Hero != null)
        {
            if (!Hero.GetComponent<hero1>().faceright)
            {

                temp = -1f;
            }
            else
            {
                temp = 1f;
                Vector3 theScale = gameObject.transform.localScale;
                theScale.x *= -1;
                gameObject.transform.localScale = theScale;
            }
        }
        else if (Boss != null)
        {
            if (Boss.GetComponent<boss1>().faceright)
            {

                temp = 1f;
            }
            else
            {
                temp = -1f;
                Vector3 theScale = gameObject.transform.localScale;
                theScale.x *= -1;
                gameObject.transform.localScale = theScale;
            }
        }

        else
        {
            if (!faceright)
            {
                Vector3 theScale = gameObject.transform.localScale;
                theScale.x *= -1;
                gameObject.transform.localScale = theScale;
            }
        }
        
        timelive = Time.time;
        gameObject.layer = 8;
        //Debug.Log(transform.gameObject.name);
//gameObject.tag = gameObject.transform.parent.gameObject.tag;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        
        rb2d.velocity = new Vector2(speed*temp, rb2d.velocity.y);
        /*if(Time.time > timelive)
        {
            Destroy(gameObject);
        }*/
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("collision" + collision.gameObject.layer);
       if (collision.gameObject.layer == 14)
        {
            //Debug.Log(collision.tag);
            //Debug.Log(gameObject.tag);
            if(collision.tag!= gameObject.tag) 
                Destroy(gameObject);
        }
        if(collision.gameObject.layer == 16)
        {
            if (Hero != null)
            {// Debug.Log(gameObject.transform.parent.name);
                if (Hero.GetComponent<hero1>().health < Hero.GetComponent<hero1>().maxhealth)
                {
                    Hero.GetComponent<hero1>().health += 10;
                }
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == 19)
        {
            if (Hero != null)
            {
               collision.gameObject.GetComponent<boss1>().health = collision.gameObject.GetComponent<boss1>().health - damage;

                GameObject particle = Instantiate(GameObject.Find("Particle System"), gameObject.transform.position, gameObject.transform.rotation);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {

            
            if ((Hero==null||collision.gameObject.tag != Hero.tag))
            {
                collision.gameObject.GetComponent<hero1>().health = collision.gameObject.GetComponent<hero1>().health - damage;
                //Debug.Log("Hero = "+ collision.gameObject.name + " Health = " + collision.gameObject.GetComponent<hero1>().health);
                GameObject particle = Instantiate(GameObject.Find("Particle System"), gameObject.transform.position, gameObject.transform.rotation);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
                //Destroy(particle);
            }

        }

        else if((collision.tag == "ground"&&!ignorewall))
        {
            Destroy(gameObject);

        }
        //else if((collision.tag == "Barrier" && transform.parent.gameObject.name != collision.transform.gameObject.name))
       // {
          //  Destroy(gameObject);
       // }
    }
   
}
