using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesController : MonoBehaviour {

    public static GamesController instance;
    public Character character;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public Text scoreText;

    private int score = 0;

    // Used for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            updateFunction();
        }
    }

    // use same behaviour than with mouse press
    public void androidValue(string value)
    {
        int intVal = int.Parse(value);
        if (intVal > 0)
        {
            updateFunction();
        }
    }

    private void updateFunction()
    {
        if (gameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            character.flap();
        }
    }

    public void BirdScored()
    {
        if(gameOver)
        {
            return;
        }
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }


    public void BackToMenu()
    {
        //if (audioSource.isPlaying)
        //{
        //    audioSource.Stop();
        //}
        SceneManager.LoadScene("Menu");
    }

}
