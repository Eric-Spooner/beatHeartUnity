using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Creator : MonoBehaviour {

	private TextAsset levelInfo;
	public int velocity;
	private int difficulty;

	private static double unitsToTravel;
	private static double unitsToTravelLong;
	private float spawnTime;
	private float time;
	private Canvas canvas;
	private AudioSource audioSource;
	private Text songTime; 
	private Image progressSong;
    private Image beatzValues;
    private Transform gameOver;

	private bool gameOverReady;
	private float gameOverTime;

	private LoadMusic loadMusic;
	private int musicIndex = 0;

	// Use this for initialization
	void Start () {
		//AndroidJavaObject myAndroidJavaObject = new AndroidJavaObject ("com.my");
		levelInfo = ((TextAsset)Resources.Load("FOOLS_G_b0"));

		RectTransform controlMask = GameObject.Find ("controlMask").GetComponent<RectTransform> ();
		unitsToTravel = Screen.height / 2.0 + controlMask.transform.localPosition.y +(-1)*((GameObject)Resources.Load ("kross")).transform.localPosition.y; 
		
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		audioSource = GameObject.Find ("Song").GetComponent<AudioSource> ();
		loadMusic = new LoadMusic(levelInfo,CalcCorrectionSeconds(velocity));
		songTime = GameObject.Find ("SongTime").GetComponent<Text> ();
		progressSong = GameObject.Find ("progressSong").GetComponent<Image> ();
		gameOver = GameObject.Find ("Finish").GetComponent<Transform>();
        gameOverReady = true;

		DeleteArrows.init ();
		var readySetGoVar = Instantiate ((GameObject)Resources.Load ("ReadySetGo"));
		readySetGoVar.transform.SetParent (canvas.transform);	

		audioSource.Play();
	}

	void Update(){
		if (audioSource.isPlaying && musicIndex < loadMusic.creatorCommands.Length) {
			songTime.text = ((int)(audioSource.time / 60.0f)).ToString ("D2") + ":" + ((int)(audioSource.time % 60.0f)).ToString ("D2");
			progressSong.fillAmount = audioSource.time / audioSource.clip.length;
			if (audioSource.time > loadMusic.creatorCommands [musicIndex].seconds) {
			    CreateKross2D (getKross(), velocity);
                musicIndex++;
            }
            gameOverTime = audioSource.time;
        } else if(audioSource.isPlaying){
			progressSong.fillAmount = audioSource.time / audioSource.clip.length;
		}
        if (((audioSource.time - gameOverTime) > 7) && musicIndex >= loadMusic.creatorCommands.Length && gameOverReady) {
            gameOver.Translate(new Vector3(130,0,0));
			gameOverReady = false;
            //Save the statistics
        }
	}

	/**
	 * Dependent on the level selected
	*/
	private GameObject getKross(){
		return ((GameObject)Resources.Load ("kross"));
	}

	private float CalcCorrectionSeconds(int velocity){
		return ((float)unitsToTravel / ((float)velocity))/6.0f;
	}

	private float CalcCorrectionSecondsLongArrow(int velocity){
		return ((float)unitsToTravelLong / ((float)velocity))/6.0f;
	}

	private void CreateKross2D(GameObject kross, int velocity){
		var krossVar = Instantiate (kross);
        krossVar.transform.SetParent (canvas.transform);
		Rigidbody2D krossRigid = krossVar.GetComponent<Rigidbody2D> ();
        krossRigid.velocity = new Vector2(0,velocity);
	} 

}
