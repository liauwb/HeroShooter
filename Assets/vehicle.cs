using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(-100f, 0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
