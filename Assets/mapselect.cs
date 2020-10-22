using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapselect : MonoBehaviour {
    public bool onmapselect = false;
    public GameObject[] list;
    public GameObject button1;
    public GameObject button2;
    public GameObject text;
    public GameObject charselect;
    public GameObject button3;
    public GameObject button4;
    public int index;
    public float selectcd;
    float temp;
    // Use this for initialization
    void Start () {
        list = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            list[i] = transform.GetChild(i).gameObject;
            list[i].SetActive(false);
        }
        selectcd = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (onmapselect == true)
        {
            text.GetComponent<Text>().text = "Select Map";
            float input = Input.GetAxisRaw("Horizontal");
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
            if (index < 0)
            {
                index = transform.childCount - 1;
            }

            list[index].SetActive(true);
        }

    }
    
   public void FinishSelection()
    {
        Debug.Log(PlayerPrefs.GetString("Mode"));
        if(PlayerPrefs.GetString("Mode") == "VS")
            SceneManager.LoadScene("map" + (index+1));
        else
            SceneManager.LoadScene("coop-map" + (index + 1));
    }

    public void back()
    {
        list[index].SetActive(false);
        onmapselect = false;
        text.GetComponent<Text>().text = "Player " + PlayerPrefs.GetInt("playernum");
        button1.SetActive(false);
        charselect.GetComponent<characterselect>().list[0].SetActive(true);
        charselect.GetComponent<characterselect>().list[0].GetComponent<Animator>().Play("idle");
        charselect.GetComponent<characterselect>().index = 0;
        button2.SetActive(true);
        button4.SetActive(false);
        //button5.SetActive(false);
        charselect.GetComponent<characterselect>().oncharselect = true;
        charselect.GetComponent<characterselect>().playernum--;
        charselect.GetComponent<characterselect>().back.SetActive(true);
        
    }
}
