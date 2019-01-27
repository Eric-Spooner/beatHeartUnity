using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {
    public GameObject firstAidKit;                                 //The column game object.
    private float spawnRate = 3.8f;                                    //How quickly columns spawn.
    private float columnMin = -1f;                                   //Minimum y value of the column position.
    private float columnMax = 4f;                                  //Maximum y value of the column position.
    private float spawnXPosition = 10f;
    private float timeSinceLastSpawned;

    void Start()
    {
        timeSinceLastSpawned = 0f;
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GamesController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            //Set a random y position for the column
            float spawnYPosition = Random.Range(columnMin, columnMax);

            Instantiate(firstAidKit, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity);
        }
    }

}
