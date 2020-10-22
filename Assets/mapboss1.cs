using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapboss1 : boss1 {

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            anim.Play("start");
        }
        if (health > 0 && ready && shooting == 0 && shooting2 == 0)
        {
            int use = Random.Range(1, 4);

            if (temptime + cd < Time.time)
            {
                switch (use)
                {
                    case 1:
                        if (faceright)
                        {
                            Debug.Log(use);
                            rb2d.velocity = (new Vector2(50f, 50f));
                            faceright = false;
                            Vector3 theScale = gameObject.transform.localScale;
                            //print("prior scale = " + player.transform.localScale);
                            theScale.x *= -1;
                            gameObject.transform.localScale = theScale;

                        }
                        else
                        {
                            use = -2;
                            Debug.Log(use);
                            rb2d.velocity = (new Vector2(-50f, 50f));
                            faceright = true;
                            Vector3 theScale = gameObject.transform.localScale;
                            //print("prior scale = " + player.transform.localScale);
                            theScale.x *= -1;
                            gameObject.transform.localScale = theScale;
                        }
                        done = true;
                        break;
                    case 2:
                        //Debug.Log("shooooooot");
                        anim.Play("shoot1");
                        shoottime = Time.time;
                        shooting = 10;
                        break;
                    case 3:
                        anim.Play("shoot2");
                        shoottime = Time.time;
                        shooting2 = 15;
                        break;
                }
                if (health < 1000)
                {
                    shooting *= 2;
                    shooting2 *= 2;
                }
                if (health < 500)
                {
                    shooting *= 3;
                    shooting2 *= 3;
                }
                if (done == true)
                {
                    temptime = Time.time;
                    if (health < maxhealth / 2)
                    {
                        cd = 3f;
                    }
                    else cd = 6f;
                    done = false;
                }
            }

        }
        if (health <= 0)
        {
            GameObject particle = Instantiate(GameObject.Find("DeathParticle"), gameObject.transform.position, gameObject.transform.rotation);
            anim.Play("death");
            particle.GetComponent<ParticleSystem>().Play();
            StartCoroutine(dead());

        }

        if (shooting > 0)
        {
            if (shoottime + .3f < Time.time)
            {
                Vector2 asdf;
                if (faceright)
                    asdf = new Vector2(-17.7f, Random.Range(-13f, 11f));
                else
                    asdf = new Vector2(7f, Random.Range(-13f, 11f));
                GameObject temp = Instantiate(shot, asdf, shot.transform.rotation);
                shoottime = Time.time;
                shooting--;


            }
            done = true;

        }
        if (shooting2 > 0)
        {
            if (shoottime + .3f < Time.time)
            {
                Vector2 asdf = new Vector2(Random.Range(-30f, 23f), 16f);
                GameObject temp = Instantiate(shot2, asdf, shot2.transform.rotation);
                shoottime = Time.time;
                shooting2--;


            }
            done = true;
        }
    }
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }

}
