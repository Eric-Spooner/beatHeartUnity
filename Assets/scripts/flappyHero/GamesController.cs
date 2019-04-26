using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesController : MonoBehaviour {

    public static bool firstTime = true;
    public static GamesController instance;
    public Character character;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -100.0f;
    public float dieAmount = 3.0f;
    public GameObject scoreText;
    public Text scoreText2;

    private int score = 0;
    private int lost = 0;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (firstTime)
        {
            firstTime = false;
            BirdDied();
        }
    }
     
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
       // if (Input.GetMouseButtonDown(0))
        //{
        //    updateFunction();
      //  }
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
        scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
        scoreText2.text = "SCORE: " + score.ToString();
    }

    public void KitLost()
    {
        if (gameOver)
        {
            return;
        }
        lost++;
        if(lost >= dieAmount)
        {
            BirdDied();
        }
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        scoreText.SetActive(false);
        gameOver = true;
    }


    public void BackToMenu()
    {
        //if (audioSource.isPlaying)
        //{
        //    audioSource.Stop();
        //}
        //SceneManager.LoadScene("Menu");
        Application.Quit();
    }

}
