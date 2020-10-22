using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobility :MonoBehaviour
    
{
    public string skillname;
    protected float dashtime = 0;
    protected bool dash;
    protected float temp = -6;
    public GameObject player;
    public virtual bool useskill(bool grounded, float dashcd)
    {

        return true;
    }
    public virtual bool skillupdate(Rigidbody2D rb2d, float h)
    {
        return true;
    }


}
