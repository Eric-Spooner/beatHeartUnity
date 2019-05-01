﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectEagle : MonoBehaviour {

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GamesController.instance.scrollSpeed*2.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	    if(GamesController.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }	
	}

}
