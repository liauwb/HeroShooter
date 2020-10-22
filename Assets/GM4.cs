using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM4 : MonoBehaviour
{
    public GameObject[] playerlist;
    public GameObject[] startupmsg;
    public GameObject meteor;
    public GameObject meteor2;  
    float redetime;
    float meteortime;
    float gotime = -1f;
    bool rdone = false;
    bool gdone = false;
    float poweruptime;
    float cautiontime;
    public GameObject powerup;
    public GameObject caution;
    bool coming = false;
    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (Time.time > redetime + 2.5f && rdone == false)
        {
            startupmsg[1].SetActive(true);
            gotime = Time.time;
            rdone = true;
        }
        if (Time.time > gotime + 1f && rdone == true && gdone == false)
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
        if (Time.time > meteortime)
        {
            float lr = Random.Range(-1f, 1f);
            float ud = Random.Range(-1f, 1f);
          
           
            if (ud > 0)
            {
                if (lr < 0)
                {
                    GameObject temp = Instantiate(meteor, new Vector3(-48, -12, -20.7f), meteor.transform.rotation);
                    temp.GetComponent<shoot>().faceright = true;
                }
                else
                {
                    GameObject temp = Instantiate(meteor, new Vector3(35, -12.5f, -20.7f), meteor.transform.rotation);
                    temp.GetComponent<shoot>().temp = -1;
                }
            }
            else
            {
                if (lr > 0)
                {
                    GameObject temp = Instantiate(meteor2, new Vector3(-48, ud, -20.7f), meteor2.transform.rotation);
                    temp.GetComponent<shoot>().faceright = false;
                }
                else
                {
                    GameObject temp = Instantiate(meteor2, new Vector3(35, ud, -20.7f), meteor2.transform.rotation);
                    temp.GetComponent<shoot>().temp = -1;
                    temp.GetComponent<shoot>().faceright = true;
                }
            }
            meteortime = Time.time + Random.Range(5f, 10f);
             
        }
    

        if (Time.time > poweruptime)
        {
            GameObject temp = Instantiate(powerup, new Vector3(Random.Range(-30f, 19f), Random.Range(-10f, 22f), 0), powerup.transform.rotation);
            poweruptime = Time.time + Random.Range(10f, 20f);
        }
        int playeralive = 0;
        foreach (GameObject temp in playerlist)
        {
            foreach (GameObject temp2 in temp.GetComponent<heroselected>().list)
            {
                if (temp2.activeInHierarchy == true)
                {
                    playeralive++;
                }
            }
        }
        if (playeralive == 1)
        {
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
