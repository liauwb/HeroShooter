using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingcamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.01f, gameObject.transform.position.y, -300f);
	}
}
