using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class characterselect : MonoBehaviour {
    public GameObject[] list;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject coop;
    public GameObject text;
    public GameObject mapselect;
    public int playernum = 0;
    int playermax = 0;
    public int index;
    public bool oncharselect = false;
    public float selectcd;
    // Use this for initialization
    float temp = 0;
    float tempindex = 0;
    public GameObject back;
    public GameObject mapback;

    void Start () {
        list = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            list[i] = transform.GetChild(i).gameObject;
            list[i].SetActive(false);
        }
        selectcd = Time.time;
        for(int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if(string.IsNullOrEmpty(Input.GetJoystickNames()[i]))
            {
                break;
            }
            playermax++;

        }
        playermax += 2;
        Debug.Log(playermax);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(oncharselect == true)
        {
            if (playernum != 4 && playernum != playermax)
                text.GetComponent<Text>().text = "Player " + (playernum+1);
            float input = 0;
            if (PlayerPrefs.GetInt("UseKeyboard" + (playernum + 1)) == 0)
            {
                input = Input.GetAxisRaw("KeyboardHorizontal"+(playernum+1));
            }
            else input = Input.GetAxisRaw("Horizontal"+ PlayerPrefs.GetInt("UseKeyboard" + (playernum + 1)));
            list[index].SetActive(false);
            if (Time.time >= selectcd + 0.4f && input != 0)
            {
                temp = input;
                selectcd = Time.time;
            }
            else
            {
                temp = 0;
            }

            if (temp > 0)
            {
                index++;
            }
            else if (temp < 0)
            {
                index--;
            }
            if (index == transform.childCount)
            {
                index = 0;
            }
            if(index < 0)
            {
                index = transform.childCount-1;
            }

            list[index].SetActive(true);
            //list[index].GetComponent<Animator>().Play("idle");

        }
	}
    public void SelectHero()
    {
        PlayerPrefs.SetInt("player"+(playernum+1), index);
        Debug.Log("player" + (playernum+1) + " " + index);
        playernum++;
        //text.GetComponent<Text>().text = "Player " + (playernum + 1);
        if (playernum == 2)
        {
            button3.SetActive(true);
        }
        if(playernum == 4||playernum == playermax)
        {
            text.GetComponent<Text>().text = "Selection Done";
            button2.SetActive(false);
            oncharselect = false;
        }
    }

    public void FinishSelection()
    {
                                                                                                                                                                                                                                                                                                                    
        //playernum++;
        PlayerPrefs.SetInt("playernum", playernum);
        list[index].SetActive(false);
        oncharselect = false;
        Debug.Log("playernum " + playernum);
        mapselect.GetComponent<mapselect>().onmapselect = true;
        button2.SetActive(false);
        button4.SetActive(true);
        text.GetComponent<Text>().text = "Select Map";
        button3.SetActive(false);
        mapselect.GetComponent<mapselect>().list[0].SetActive(true);
        back.SetActive(false);
        mapback.SetActive(true);
    }

    public void CancelSelection() {

        if (playernum > 0)
        {
            if(playernum == 4||playernum == playermax)
            {
                button2.SetActive(true);
                oncharselect = true;
            }
            playernum--;
            //text.GetComponent<Text>().text = "Player " + (playernum + 1);
        }
        else
        {
            list[index].SetActive(false);
            coop.SetActive(true);
            button1.SetActive(true);
            button2.SetActive(false);
            button5.SetActive(true);
            oncharselect = false;
            text.SetActive(false);
            back.SetActive(false);
            
        }
        if (playernum < 2)
        {
            button3.SetActive(false);
        }
    }
    public void StartSelection()
    {
        button1.SetActive(false);
        coop.SetActive(false);
        list[0].SetActive(true);
        list[0].GetComponent<Animator>().Play("idle");
        PlayerPrefs.SetString("Mode", "VS");
        index = 0;
        button2.SetActive(true);
        button5.SetActive(false);
        oncharselect = true;
        text.SetActive(true);
        back.SetActive(true);
    }

    public void CoopSelection()
    {
        button1.SetActive(false);
        coop.SetActive(false);
        PlayerPrefs.SetString("Mode", "Coop");
        list[0].SetActive(true);
        list[0].GetComponent<Animator>().Play("idle");
        index = 0;
        button2.SetActive(true);
        button5.SetActive(false);
        oncharselect = true;
        text.SetActive(true);
        back.SetActive(true);
    }

}
