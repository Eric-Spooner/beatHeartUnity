using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float upForce = 100f;

    public Rigidbody2D rb;
    public static Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    public void flap()
    {    
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0f, upForce));
        anim.SetTrigger("Flap");
    }

    public void flapGround()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(2f, upForce));
        anim.SetTrigger("Flap");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (!collision.collider.name.Equals("top")) {
            if (!GamesController.instance.gameOver)
            {
                flapGround();
            }
        }
    }

}
