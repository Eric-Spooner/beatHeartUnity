using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private static AndroidJavaObject curActivity;
    private static Canvas canvas;
    public static Menu instance;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Used for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Quit()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.Quit();
    }

    public void startFlappy()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("levels/" + "flappyHero", LoadSceneMode.Single);
    }

    public void startRhythm()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadSceneAsync("levels/" + "healthyRhythm");
    }

    public static void CallJavaFunc(string strFuncName, string strTemp)
    {
        if (curActivity == null)
        {
            //log.text = "Log: no activity";
            return;
        }
        //log.text = "Log: activity call function " + strFuncName;
        curActivity.Call(strFuncName, strTemp);
    }

}
