using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class winner : MonoBehaviour {
    public GameObject[] list;
    // Use this for initialization
    void Start () {
        list = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            list[i] = transform.GetChild(i).gameObject;
            list[i].SetActive(false);
        }
        list[PlayerPrefs.GetInt("Winner")].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void backtomenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
