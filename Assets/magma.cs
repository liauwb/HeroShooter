using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magma : MonoBehaviour {
    public int dot = 10;
    float[] time;
    bool[] burning;
    public GameObject[] playerlist;
    public GameObject player;
	// Use this for initialization
	void Start () {
        playerlist = new GameObject[player.transform.childCount];
        //Debug.Log(transform.childCount);
        for (int i = 0; i < player.transform.childCount; i++)
        {
            playerlist[i] = player.transform.GetChild(i).gameObject;
            Debug.Log(playerlist[i].name);
        }

        time = new float[4];
        for(int i = 0; i < 4; i++)
        {
            time[i] = Time.time-3f;
        }
        burning = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            burning[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i<4; i++)
        {
            if (burning[i])
            {
               // Debug.Log(playerlist[i].name);
                if(Time.time > time[i]+2)
                {
                    foreach (GameObject temp2 in playerlist[i].GetComponent<heroselected>().list)
                    {
                        if (temp2.activeInHierarchy == true)
                        {
                            temp2.GetComponent<hero1>().health -=10;
                        }
                    }
                    time[i] = Time.time;
                }
            }
        }   
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer + " + " + collision.gameObject.name);
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            int temp = -1;
            temp = temp + (collision.gameObject.transform.parent.gameObject.name[6] - 48);
            burning[temp] = true;
        }
        
      
        //else if((collision.tag == "Barrier" && transform.parent.gameObject.name != collision.transform.gameObject.name))
        // {
        //  Destroy(gameObject);
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
   
    {
        Debug.Log(collision.gameObject.layer+ " + "+ collision.gameObject.name);
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            int temp = -1;
            temp = temp + (collision.gameObject.transform.parent.gameObject.name[6] - 48);
            burning[temp] = false;
        }
    }
}
