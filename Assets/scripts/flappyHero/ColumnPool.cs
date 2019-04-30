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

            int columns = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));
            int rows = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));
            for (int i = 0; i < columns; i++)
            {
                for(int j = 0; j < rows; j++) { 
                    Instantiate(firstAidKit, new Vector2(spawnXPosition+i, spawnYPosition-j), Quaternion.identity);
                }
            }
        }
    }

}
