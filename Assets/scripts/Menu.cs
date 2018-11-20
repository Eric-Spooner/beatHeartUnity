using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	
	private string levelName;
	private static Canvas canvas;
	private static AndroidJavaObject curActivity;
	public static Text log;
    private Image beatzEasyValue;
    private Image beatzNormalValue;
    private Image beatzHardValue;

    void Start(){
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		log = GameObject.Find ("Log").GetComponent<Text> ();
    }

	public void Quit(){
        Application.Quit ();
	}

	public void SetDifficulty(int difficulty){
        LevelInfo.difficulty = difficulty;
		if (difficulty == 2) {
			LevelInfo.textAsset = ((TextAsset)Resources.Load (levelName + "_b1"));

		} else {
			LevelInfo.textAsset = ((TextAsset)Resources.Load (levelName + "_b0"));
		}
		var loading = Instantiate ((GameObject)Resources.Load("Loading"));
		loading.transform.SetParent (canvas.transform);

		SceneManager.LoadSceneAsync("levels/" + levelName);
	}

	public void SetLevel(string levelName){
        this.levelName = levelName;
        LevelInfo.levelName = levelName;
		LevelInfo.audioSource = ((AudioSource)Resources.Load (levelName + ".mp3"));
        beatzEasyValue = GameObject.Find("BeatzEasyValue").GetComponent<Image>();
        beatzNormalValue = GameObject.Find("BeatzNormalValue").GetComponent<Image>();
        beatzHardValue = GameObject.Find("BeatzHardValue").GetComponent<Image>();
        CallJavaFunc("getBeatz", levelName + ";0");
        CallJavaFunc("getBeatz", levelName + ";1");
        CallJavaFunc("getBeatz", levelName + ";2");
    }

	public void AndroidCallFunction(string value){
		Text android = GameObject.Find ("Text").GetComponent<Text> ();
		android.text = ("Android: " + value);
	}

    public static void CallJavaFunc(string strFuncName, string strTemp){
		if( curActivity == null ){
			//log.text = "Log: no activity";
			return;
		}
		//log.text = "Log: activity call function " + strFuncName;
		curActivity.Call( strFuncName, strTemp );
	}

    public void resetBeatz(){
        beatzEasyValue.fillAmount = 0;
        beatzNormalValue.fillAmount = 0; 
        beatzHardValue.fillAmount = 0; 
    }

}
