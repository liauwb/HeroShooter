using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour {
    public GameObject button1;
    public GameObject button2;
    public GameObject back;
    public GameObject change1;
    public GameObject change2;
    public GameObject changectrl1;
    public GameObject changectrl2;
    public GameObject coop;
    public GameObject txt1;
    public GameObject txt2;
    public GameObject txt3;
    public GameObject txt4;
   // public GameObject temp;
    int playermax = 0;
    // Use this for initialization
    void Start () {
        //Debug.Log(Input.GetJoystickNames()[0]);
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            //Debug.Log("i " + i);
            //Debug.Log(Input.GetJoystickNames()[i]);
            if (string.IsNullOrEmpty(Input.GetJoystickNames()[i]))
            {

                //Debug.Log("it ends here" +Input.GetJoystickNames()[i]);
                
                break;
            }
            playermax++;

        }
        Debug.Log("playermax " + playermax);
        for (int i = 1; i <= 4; i++)
        {
           PlayerPrefs.SetInt("UseKeyboard"+i, 0);
        }

        switch (playermax)
        {
            case 1:
                PlayerPrefs.SetInt("UseKeyboard" + 3, 1);
                break;
            case 2:
                PlayerPrefs.SetInt("UseKeyboard" + 3, 1);
                PlayerPrefs.SetInt("UseKeyboard" + 4, 2);
                break;
            case 3:
                PlayerPrefs.SetInt("UseKeyboard" + 2, 2);
                PlayerPrefs.SetInt("UseKeyboard" + 3, 1);
                PlayerPrefs.SetInt("UseKeyboard" + 4, 3);
                break;
            case 4:
                PlayerPrefs.SetInt("UseKeyboard" + 1, 1);
                PlayerPrefs.SetInt("UseKeyboard" + 2, 2);
                PlayerPrefs.SetInt("UseKeyboard" + 3, 3);
                PlayerPrefs.SetInt("UseKeyboard" + 4, 4);
                break;
        }
        Debug.Log("Usekeyboard1 = " + PlayerPrefs.GetInt("UseKeyboard1"));
        Debug.Log("Usekeyboard2 = " + PlayerPrefs.GetInt("UseKeyboard2"));
        Debug.Log("Usekeyboard3 = " + PlayerPrefs.GetInt("UseKeyboard3"));
        Debug.Log("Usekeyboard4 = " + PlayerPrefs.GetInt("UseKeyboard4"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeSetting()
    {
        button1.SetActive(false);
        coop.SetActive(false);
        button2.SetActive(false);
        back.SetActive(true);
        change1.SetActive(true);
        change2.SetActive(true);
        changectrl1.SetActive(true);
        changectrl2.SetActive(true);
        txt1.SetActive(true);
        txt2.SetActive(true);
        txt3.SetActive(true);
        txt4.SetActive(true);
    }
    public void Back()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        coop.SetActive(true);
        back.SetActive(false);
        change1.SetActive(false);
        change2.SetActive(false);
        changectrl1.SetActive(false);
        changectrl2.SetActive(false);
        txt1.SetActive(false);
        txt2.SetActive(false);
        txt3.SetActive(false);
        txt4.SetActive(false);
    }
    public void ChangeController1()
    {
        if (PlayerPrefs.GetInt("UseKeyboard1") == 0)
        {
            PlayerPrefs.SetInt("UseKeyboard1", 1);
            changectrl1.GetComponent<Text>().text = "Controller";
        }
        else
        {
            PlayerPrefs.SetInt("UseKeyboard1", 0);
            changectrl1.GetComponent<Text>().text = "KeyBoard";
        }
    }
    public void ChangeController2()
    {
        if (PlayerPrefs.GetInt("UseKeyboard2")== 0)
        {
            PlayerPrefs.SetInt("UseKeyboard2", 2);
            changectrl2.GetComponent<Text>().text = "Controller";
        }
        else
        {
            PlayerPrefs.SetInt("UseKeyboard2", 0);
            changectrl2.GetComponent<Text>().text = "KeyBoard";
        }
    }
}
