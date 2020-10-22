using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshealthbar : MonoBehaviour {
    public GameObject greenbar;
    public GameObject boss;
	// Use this for initialization
	void Start () {
        Debug.Log(1490 / 1500);
	}
	
	// Update is called once per frame
	void Update () {
        greenbar.transform.localScale = new Vector3(boss.GetComponent<boss1>().health / boss.GetComponent<boss1>().maxhealth, 1f, 1f);
       //// Debug.Log(boss.GetComponent<boss1>().health / boss.GetComponent<boss1>().maxhealth);
        //Debug.Log(boss.GetComponent<boss1>().health);
        //Debug.Log(boss.GetComponent<boss1>().maxhealth);
        if (boss.GetComponent<boss1>().health <= 0)
            gameObject.SetActive(false);
        //  Debug.Log(greenbar.transform.localScale);
        greenbar.transform.localPosition = new Vector3((boss.GetComponent<boss1>().health / boss.GetComponent<boss1>().maxhealth) * 3 - 3, 0, -16.5f);
    }
}
