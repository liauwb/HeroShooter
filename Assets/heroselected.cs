using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroselected : MonoBehaviour {
    public GameObject[] list;
    public int playerid;
    // Use this for initialization
    void Start () {
        list = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            list[i] = transform.GetChild(i).gameObject;
            list[i].SetActive(false);
        }
        //list[transform.childCount-1].SetActive(true);
        if(playerid <= PlayerPrefs.GetInt("playernum"))
           list[PlayerPrefs.GetInt(gameObject.name)].gameObject.SetActive(true);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
