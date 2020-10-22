using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot2 : MonoBehaviour {
    public float yvelocity;
    public GameObject Hero;
    public Vector3 pos;
    public float temp;
    public float timelive;
    public float speed;
    public Rigidbody2D rb2d;
    public bool faceright;
    void Flip()
    {
        faceright = !faceright;
        Vector3 theScale = gameObject.transform.localScale;
        theScale.x *= -1;
        gameObject.transform.localScale = theScale;
    }

    void Start()
    {
        //Hero = GameObject.Find("player1");
        rb2d = GetComponent<Rigidbody2D>();
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
        rb2d.AddForce(new Vector2(speed * temp, yvelocity));
        timelive = Time.time;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    //public GameObject Hero = GameObject.Find("player4");
    public void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            if (collision.tag != gameObject.tag)
            {
                GameObject particle = Instantiate(GameObject.Find("explosion"), gameObject.transform.position, gameObject.transform.rotation);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.layer == 9)
        {
            if ((Hero == null || collision.gameObject.tag != Hero.tag))
            {
                collision.gameObject.GetComponent<hero1>().health = collision.gameObject.GetComponent<hero1>().health - 50;
                //Debug.Log("Hero = "+ collision.gameObject.name + " Health = " + collision.gameObject.GetComponent<hero1>().health);
                GameObject particle = Instantiate(GameObject.Find("Particle System"), gameObject.transform.position, gameObject.transform.rotation);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
                //Destroy(particle);
            }

        }

        else if (collision.tag == "Barrier")
        {
            

        }

    }


}
