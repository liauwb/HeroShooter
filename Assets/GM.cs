using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {
    public GameObject[] playerlist;
    public GameObject[] startupmsg;
    public GameObject meteor;
    float redetime;
    float meteortime;
    float gotime = -1f;
    bool rdone = false;
    bool gdone = false;
    float poweruptime;
    public GameObject powerup;
	// Use this for initialization
	void Start () {
        playerlist = new GameObject[transform.childCount];
        //Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            playerlist[i] = transform.GetChild(i).gameObject;
        }
        startupmsg = new GameObject[2];
        startupmsg[0] = GameObject.Find("ready");
        startupmsg[1] = GameObject.Find("go");
        startupmsg[1].SetActive(false);
        startupmsg[0].GetComponent<Animator>().Play("rede");
        redetime = Time.time;
        //Debug.Log();
        meteortime = Time.time + Random.Range(5f, 9f);
        poweruptime = Time.time + Random.Range(6f, 10f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > redetime + 2.5f && rdone == false)
        {
            startupmsg[1].SetActive(true);
            gotime = Time.time;
            rdone = true;
        }
        if(Time.time > gotime + 1f && rdone == true && gdone == false)
        {
            gdone = true;
            foreach (GameObject temp in playerlist)
            {
                foreach (GameObject temp2 in temp.GetComponent<heroselected>().list)
                {
                    if (temp2.activeInHierarchy == true)
                    {
                        temp2.GetComponent<hero1>().ready = true;
                    }
                }
            }
            startupmsg[1].SetActive(false);
        }
        if(Time.time > meteortime)
        {
            GameObject temp = Instantiate(meteor, new Vector3(Random.Range(-30f,19f),22,0), meteor.transform.rotation);
            meteortime = Time.time + Random.Range(5f, 10f);
        }
        if(Time.time > poweruptime)
        {
            GameObject temp = Instantiate(powerup, new Vector3(Random.Range(-30f, 19f),Random.Range(-10f,22f),0), powerup.transform.rotation);
            poweruptime = Time.time + Random.Range(10f, 20f);
        }
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
            foreach (GameObject temp in playerlist)
            {
                int i = 0;
                foreach (GameObject temp2 in temp.GetComponent<heroselected>().list)
                {
                    if (temp2.activeInHierarchy == true)
                    {
                        PlayerPrefs.SetInt("Winner", i);
                        StartCoroutine(winner());  
                    }
                    i++;
                }
            }
        }
	}
    IEnumerator winner()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("winner");
    }
}
