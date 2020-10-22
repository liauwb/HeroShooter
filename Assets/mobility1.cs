using System;
using UnityEngine;
public class mobility1 : mobility
{
    public override bool useskill(bool grounded, float dashcd)
    {
        if ( Time.time > temp + dashcd)
        {
            dashtime = Time.time;
            dash = true;
            return true;
        }
        return false;
    }
    public override bool skillupdate(Rigidbody2D rb2d, float h)
    {
        
        if (dash == true && Time.time - dashtime < 1)
        {
            rb2d.AddForce(Vector2.right * h * 2000f);
            return true;
        }
        else
        {
            if (dash == true)
            {

                temp = Time.time;
            }
            dash = false;
            dashtime = 0;
            return false;
        }
    }
}
