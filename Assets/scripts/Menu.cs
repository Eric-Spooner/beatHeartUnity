using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	
	private static Canvas canvas;

    void Start(){
        Screen.orientation = ScreenOrientation.Portrait;
        canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
    }

	public void Quit(){
        Application.Quit ();
	}

    public void startFlappy()
    {
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("levels/" + "flappyHero");
    }

    public void startRhythm()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadSceneAsync("levels/" + "healthyRhythm");
    }

}
