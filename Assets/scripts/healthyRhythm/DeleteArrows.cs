using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteArrows : MonoBehaviour {

	public static int miss;
	private static Text missValue;
	private static Canvas canvas;

	// Use this for initialization
	public static void init () {
		miss = 0;
		missValue = GameObject.Find ("MissValue").GetComponent<Text> ();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision){
		collission (collision.gameObject);
	}

	public static void collission(GameObject gameObject){
		Destroy (gameObject);
		var missAnim = Instantiate ((GameObject)Resources.Load ("Miss"));
		missAnim.transform.SetParent (canvas.transform);
		var missBarVar = Instantiate ((GameObject)Resources.Load ("MissBar"));
		missBarVar.transform.SetParent (canvas.transform);
		miss++;
		missValue.text = miss.ToString();
	}



}
