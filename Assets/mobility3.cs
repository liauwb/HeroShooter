using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobility3: mobility
{

   public GameObject shield;
    public GameObject tempshield;
    public override bool useskill(bool grounded, float dashcd)
    {
        if (Time.time > temp + dashcd)
        {
            dashtime = Time.time;
            dash = true;
            return true;
        }
        return false;
    }
    public override bool skillupdate(Rigidbody2D rb2d, float h)
    {
        //Debug.Log(dash);
        if (dash == true && Time.time - dashtime < 3 )
        {
            if (shield == null)
            {
               // Debug.Log("sheild should be on");
                shield = Instantiate(tempshield, new Vector3(player.transform.position.x,player.transform.position.y,12f), player.transform.rotation);
                
            }
            shield.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 12f);
                return true;
        }
        else
        {
            if (shield != null)
            {
                Destroy(shield);
                temp = Time.time;
            }
            return false;
        }
    }
}
