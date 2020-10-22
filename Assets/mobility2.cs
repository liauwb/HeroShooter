using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobility2 : mobility {

    public override bool useskill(bool grounded, float dashcd)
    {
        if (Time.time > temp + dashcd)
        {
            Debug.Log("time = " +Time.time);
            Debug.Log("dashcd = " + dashcd);
            Debug.Log("temp =");
            Debug.Log("pressed");
            dashtime = Time.time;
            dash = true;
            return true;
        }
        return false;
    }
    public override bool skillupdate(Rigidbody2D rb2d, float h)
    {
        //Debug.Log(dash);
        if (dash == true)
        {
            Vector3 temppos = player.transform.position;
            temppos.x += 10f * h;
            player.transform.position = temppos;
            temp = Time.time;
            dash = false;
            dashtime = 0;
            return false;
        }
        else
        
            return false;
    }
}
