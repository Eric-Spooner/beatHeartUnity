using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {
    public GameObject firstAidKit;                                 //The column game object.
    public GameObject angryEagle;                                 
    private float spawnRate = 3.8f;                                    //How quickly columns spawn.
    private float spawnRateEagle = 6f;                                    //How quickly columns spawn.
    private float columnMin = -1f;                                   //Minimum y value of the column position.
    private float columnMax = 4f;                                  //Maximum y value of the column position.
    private float spawnXPosition = 10f;
    private float timeSinceLastSpawned;
    private float timeSinceLastSpawnedEagle;

    void Start()
    {
        timeSinceLastSpawned = 0f;
        timeSinceLastSpawnedEagle = 0f;
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        timeSinceLastSpawnedEagle += Time.deltaTime;

        if (GamesController.instance.gameOver == false)
        {
            // Packet spawn
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;
                spawnRate = 3.8f + Random.Range(-0.3f, 0.4f);

                //Set a random y position for the column
                float spawnYPosition = Random.Range(columnMin, columnMax);

                int columns = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));
                int rows = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        Instantiate(firstAidKit, new Vector2(spawnXPosition + i, spawnYPosition - j), Quaternion.identity);
                    }
                }
            }

            //angry eagle spawn
            if(timeSinceLastSpawnedEagle >= spawnRateEagle)
            {
                timeSinceLastSpawnedEagle = 0f;
                spawnRateEagle = 6f + Random.Range(-1f, 2f);

                //Set a random y position for the column
                float spawnYPositionEagle = Random.Range(columnMin, columnMax);
                Instantiate(angryEagle, new Vector2(spawnXPosition, spawnYPositionEagle), Quaternion.identity);
            }
        }
    }

}
