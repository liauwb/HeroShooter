using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobility4 : mobility
{

    public GameObject fireball2;
    bool chanelling;
    public override bool useskill(bool grounded, float dashcd)
    {
        if (Time.time > temp + dashcd)
        {
            dash = true;
            chanelling = true;
            return true;
            
        }
        return false;
    }
    public override bool skillupdate(Rigidbody2D rb2d, float h)
    {
        //Debug.Log("p4 dash = "+dash );
        
        if (dash == true)
        {
            //player.gameObject.GetComponent<hero1>().anim.Stop();
            player.gameObject.GetComponent<hero1>().anim.Play("dash");
            StartCoroutine(channel());
            dash = false;
            temp = Time.time;
        }
        return false;
    }

    IEnumerator channel()
    {
        yield return new WaitForSeconds(3);
        if (Input.GetAxis(player.GetComponent<hero1>().axisname) != 0){
            chanelling = false;
            
        }
        if(chanelling)
            fireball2 = Instantiate(GameObject.Find("fireball2"), GameObject.Find("shotpoint4").transform.position, GameObject.Find("shotpoint4").transform.rotation);
    }
}
