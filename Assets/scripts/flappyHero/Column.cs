using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D trigger)
    {
       if(trigger.GetComponent<Character>() != null)
        {
            GamesController.instance.BirdScored();
        } 
    }

}
