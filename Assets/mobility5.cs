using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobility5 : mobility
{

    public bool channeling = false;
    public override bool useskill(bool grounded, float dashcd)
    {
        if (Time.time > temp + dashcd)
        {
            dashtime = Time.time;
            dash = true;
            channeling = true;
            return true;
        }
        return false;
    }
    public override bool skillupdate(Rigidbody2D rb2d, float h)
    {
        
        if (dash == true)
        {
            Debug.Log(dash);
            StartCoroutine(channel(rb2d));
            dash = false;
            temp = Time.time;
        }
        if (rb2d.velocity.y > 0)
        {
            //Debug.Log("not grounded");
            gameObject.layer = 10;
        }
        else gameObject.layer = 9;
        return false;
        
    }
    IEnumerator channel(Rigidbody2D rb2d)
    {
        yield return new WaitForSeconds(0.5f);
        if (Input.GetAxis(player.GetComponent<hero1>().axisname) != 0)
        {
            channeling = false;

        }
        if (channeling)
            rb2d.velocity = new Vector2(0, 50f);
    }
}
