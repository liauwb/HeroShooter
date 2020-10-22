using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour {
    float health;
    GameObject[] player;
    GameObject activeplayer;
    public int playernum;
    Vector3 Position;
    public GameObject greenbar;
    bool initiate = false;
    // Use this for initialization
	void Start () {
        //Debug.Log(GameObject.Find("GM").transform.GetChild(playernum).gameObject.transform.childCount);
        player = new GameObject[GameObject.Find("GM").transform.GetChild(playernum).gameObject.transform.childCount];

        if (activeplayer == null)
        {
            //Debug.Log("heya");
        }
        //greenbar = gameObject.transform.FindChild("greenbar").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if(initiate == false)
        {
            for (int i = 0; i < GameObject.Find("GM").transform.GetChild(playernum).gameObject.transform.childCount; i++)
            {
                player[i] = GameObject.Find("GM").transform.GetChild(playernum).gameObject.transform.GetChild(i).gameObject;
                if (player[i].activeSelf)
                {
                    activeplayer = player[i];
                    health = player[i].GetComponent<hero1>().health;
                    //Debug.Log(activeplayer.name);
                }
            }
            initiate = true;
        }
        if (activeplayer == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Position = activeplayer.transform.position;
            Position.y += 2.5f;
            gameObject.transform.position = Position;
            greenbar.transform.localScale = new Vector3(activeplayer.GetComponent<hero1>().health / health, 1, 1);
            if (activeplayer.GetComponent<hero1>().health <= 0)
                gameObject.SetActive(false);
            //  Debug.Log(greenbar.transform.localScale);
            greenbar.transform.localPosition = new Vector3((activeplayer.GetComponent<hero1>().health / health) * 3 - 3, 0, -16.5f);

        }
	}
}
