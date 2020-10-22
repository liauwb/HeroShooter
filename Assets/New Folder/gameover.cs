using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour {
    public CanvasRenderer backgroundcolor;
    public CanvasRenderer textcolor;
    public bool lose = false;
    public float fadetime;
	// Use this for initialization
	void Start () {
        backgroundcolor = GameObject.Find("Image").GetComponent<CanvasRenderer>();
        textcolor = GameObject.Find("Text").GetComponent<CanvasRenderer>();
        backgroundcolor.SetAlpha(0);
        textcolor.SetAlpha(0);
    }
	
	// Update is called once per frame
	void Update () {
		GameObject playertemp = GameObject.Find("hero1");
        if (playertemp.GetComponent<heroscript>().health <= 0 && lose == false )
        {
            fadetime = Time.time;
            lose = true;
            
            
        }
        if (backgroundcolor.GetAlpha() < 255 && lose == true)
        {
            Debug.Log("Game over boys " + 10 * (Time.time - fadetime));
            backgroundcolor.SetAlpha(backgroundcolor.GetAlpha() + 0.1f * (Time.time - fadetime));
            textcolor.SetAlpha(255);
        }
    }
}
