﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float upForce = 100f;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();    
	}

    public void flap()
    {    
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, upForce));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.name.Equals("top")) {
            flap();
        }
    }

}
