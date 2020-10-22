using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero2 : MonoBehaviour
{
    public heroscript hero;
    private float dashtime;
    private bool dash;
    private float dashcd;
    public bool faceright = true;
    // Use this for initialization
    void Start()
    {
        hero = new heroscript("HeroChar");
        hero.health = 6;
        hero.can_airjump = 2;
        hero.jumpForce = 1000f;
        dashtime = 0f;
        dashcd = -6f;
        faceright = hero.facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        hero.Basic(KeyCode.UpArrow, KeyCode.Keypad0, KeyCode.DownArrow);

        if (Input.GetKeyDown(KeyCode.R))
        {

            GameObject temp = Instantiate(GameObject.Find("lightningshot"), GameObject.Find("shotpoint").transform.position, GameObject.Find("lightningshot").transform.rotation);
            Destroy(temp, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Q) && hero.ground() && Time.time > dashcd + 3f)
        {
            dash = true;
            dashtime = Time.time;
        }
        faceright = hero.facingRight;
        if (hero.health == 0)
        {

        }
    }
    private void FixedUpdate()
    {
        hero.FixedBasic(KeyCode.LeftArrow, KeyCode.RightArrow, false, dash);
        if (dash == true && Time.time - dashtime < 1)
        {
            // hero.anim.Play("dash");

            //hero.rb2d.AddForce(Vector2.right * hero.h * 2000f);
        }
        else
        {
            if (dash == true)
            {

                dashcd = Time.time;
            }
            dash = false;
            dashtime = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dash)
        {
            if (collision.gameObject.tag == "player")
            {
                Physics2D.IgnoreLayerCollision(7, 7);

            }
        }
    }
}

