using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwins : MonoBehaviour {
    public GameObject[] playerlist;
	// Use this for initialization
	void Start () {
        playerlist = new GameObject[transform.childCount];
        Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            playerlist[i] = transform.GetChild(i).gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        int playeralive = 0;
		foreach(GameObject temp in playerlist)
        {
            foreach(GameObject temp2 in temp.GetComponent<heroselected>().list)
            {
                if(temp2.activeInHierarchy == true)
                {
                    playeralive++;
                }
            }
        }
        if(playeralive == 1) {
            Debug.Log("one man standing ");
        }
        Debug.Log("player alive = " + playeralive);
	}
}
