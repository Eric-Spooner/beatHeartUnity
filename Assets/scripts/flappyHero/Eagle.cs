using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.GetComponent<Character>() != null)
        {
            Character.anim.SetTrigger("Bad");
            Destroy(gameObject);
            GamesController.instance.BirdDied();
        }
    }

}
