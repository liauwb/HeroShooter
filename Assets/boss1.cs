using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour {
    public int health;
    public float maxhealth;
    public GameObject healthbar;
    public GameObject shot;
    public GameObject shot2;
    public bool ready = false;
    public Animator anim;
    public Rigidbody2D rb2d;
    public bool faceright;
    public bool oncd;
    public float cd;
    public float temptime;
    public float shoottime;
    public bool done = false;
    public int shooting = 0;
    public int shooting2 = 0;
	// Use this for initialization
	void Start () {
        health = 1500;
        faceright = true;
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        cd = 3f;
        temptime = Time.time;
        maxhealth = health;
    }
	
	
}
