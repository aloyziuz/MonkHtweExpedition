using UnityEngine;
using System.Collections;

public class MainControlScript : MonoBehaviour {
	public Transform rocketPrefab;
	public GameObject monk;
    public GameObject musicPlayer;
    #region sprites
    public Sprite monkFallImg;
    public Sprite monkFlyImg;
    public GameObject cloud;
    public Sprite[] cloudSprites;
    #endregion
    #region GUI images
    public GUISkin pauseSkin;
    public GUISkin gameOverSkin;
    public Texture resumeBtnTexture;
    public Texture mainMenuBtnTexture;
	public Texture pauseBtnTexture;
    public Texture gameOverBoxTexture;
    public Texture gameOverFrameTexture;
    public Texture gameOverTxtTexture;
	public Texture highScoreTexture;
	public Texture scoreTexture;
    public Texture playBtnTexture;
	public GUIStyle guiStyle;
	public GUIStyle scoreStyle;
    #endregion
    
    public float countdownTime = 5.0f;
    private BGMscript mpScript;
	private RaycastHit2D hit;
	public enum GameState{ Waiting, GameStart, Paused, GameOver, Win};
    internal GameState state;
	
	void Start () {
        state = GameState.Waiting;
	}
	
	// Update is called once per frame
	void Update () {
		MonkScript script = (MonkScript)monk.GetComponent("MonkScript");
		//5 seconds countdown
        if (countdownTime < 0)
        {
            state = GameState.GameStart;
            script.SetSpeed(10);
            StartCoroutine(SpawnRocket());
            countdownTime = 0;
        }
        else if(countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
        }
		
		if(Input.GetButton("Fire1")){
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			//if draggable item is selected
            if (hit.collider != null && hit.collider.gameObject.tag == "Draggable" && GUIUtility.hotControl == 0)
            {
                DraggableScript dragScript = (DraggableScript)hit.collider.gameObject.GetComponent("DraggableScript");
                dragScript.MouseClick(hit);
            }
			//if selected is not draggable and GUI pause button
            else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
            {
                mpScript = (BGMscript)musicPlayer.GetComponent("BGMscript");
                mpScript.isClicked = true;
                mpScript.CheckRhythm();
				mpScript.bell.audio.Play();
            }
		}
		
		//determine gameover
		if (script.IsBelowMinSpeed() || monk.transform.position.y < 0)
		{
            state = GameState.GameOver;
			script.SetSpeed(0);
        }
		
		//if game over save the player score
        if (state == GameState.GameOver)
        {
            if (PlayerPrefs.HasKey("highscore"))
            {
                int prevhighscore = PlayerPrefs.GetInt("highscore");
                if (prevhighscore < (int)monk.transform.position.y)
                {
                    PlayerPrefs.SetInt("highscore", (int)monk.transform.position.y);
                }
            }
            else
            {
                PlayerPrefs.SetInt("highscore", (int)monk.transform.position.y);
            }
        }

        #region change monk
        if (script.IsSpeedNegative())
        {
            SpriteRenderer renderer = (SpriteRenderer)monk.GetComponent("SpriteRenderer");
            renderer.sprite = monkFallImg;
        }
        else
        {
            SpriteRenderer renderer = (SpriteRenderer)monk.GetComponent("SpriteRenderer");
            renderer.sprite = monkFlyImg;
        }
        #endregion

        #region change cloud
        if (script.currentSpeed <= -10)
        {
            SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
            renderer.sprite = cloudSprites[0];
        }
        else if (script.currentSpeed <= -5)
        {
            SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
            renderer.sprite = cloudSprites[1];
        }
        else if (script.currentSpeed <= 0)
        {
            SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
            renderer.sprite = cloudSprites[2];
        }
        else if (script.currentSpeed <= 5)
        {
            SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
            renderer.sprite = cloudSprites[3];
        }
        else
        {
            SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
            renderer.sprite = cloudSprites[4];
        }
#endregion
	}

	void OnLevelWasLoaded(){
		Time.timeScale = 1;
	}

	void OnGUI(){
		var screenWidth = camera.pixelWidth;
		var screenHeight = camera.pixelHeight;
		GUI.skin = pauseSkin;

        MonkScript script = (MonkScript)monk.GetComponent("MonkScript");
        GUI.Label(new Rect(0, 0, 70, 20), "Speed: " + script.currentSpeed, guiStyle);
        GUI.Label(new Rect(0, 20, 150, 20), "Distance: " + (int) monk.transform.position.y, guiStyle);
		
		if(GUI.Button(new Rect(screenWidth-40, 0, 40, 40), pauseBtnTexture)){
			state = togglePause();
        }

        #region GUI reaction to game states
        if (state == GameState.Paused){
			GUI.DrawTexture(new Rect(screenWidth -50, 0, 40, 40), pauseBtnTexture);
			GUI.Box(new Rect(0, 0, screenWidth, screenHeight), "");
			if(GUI.Button(new Rect((screenWidth/2) - 70, (screenHeight/2)- 90, 150, 65), resumeBtnTexture)){
				state = togglePause();
			}
			if(GUI.Button(new Rect((screenWidth/2) - 70, (screenHeight/2) , 150, 65), mainMenuBtnTexture)) {
				Application.LoadLevel("StartScreen");
			}
		}
		else if(state == GameState.Waiting){
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.fontSize = 50;
			GUI.Label(new Rect(screenWidth/4, 0, screenWidth/2, screenHeight/2), ""+(int)countdownTime, guiStyle);
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.fontSize = 20;
		}
        else if (state == GameState.GameOver)
        {
            GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), gameOverFrameTexture);
            GUI.DrawTexture(new Rect(screenWidth / 2 - 150, screenHeight / 2 - 100, 300, 300), gameOverBoxTexture);
            GUI.DrawTexture(new Rect(screenWidth / 2 - 100, screenHeight / 2 - 60, 200, 40), gameOverTxtTexture);
			GUI.DrawTexture(new Rect(screenWidth/2- 100, screenHeight/2-10, 120, 40), highScoreTexture);
			GUI.Label(new Rect(screenWidth/2+ 20, screenHeight/2-10, 200, 200)," " + PlayerPrefs.GetInt("highscore"), scoreStyle);
			GUI.DrawTexture(new Rect(screenWidth / 2 - 100, screenHeight / 2+30, 100, 40), scoreTexture);
			GUI.Label(new Rect(screenWidth / 2 + 10, screenHeight / 2+30, 200, 200)," " + (int) monk.transform.position.y, scoreStyle);
			if (GUI.Button(new Rect((screenWidth / 2) - 120, (screenHeight / 2) + 50, 120, 120), playBtnTexture))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (GUI.Button(new Rect((screenWidth / 2), (screenHeight / 2) + 50, 120, 120), mainMenuBtnTexture))
            {
                Application.LoadLevel("StartScreen");
            }
            StopCoroutine("SpawnRocket");
        }
        #endregion
    }

    //start only when the game is started
	IEnumerator SpawnRocket(){
		while(true){
			int n =  Random.Range(0, 10);
            if (n == 1)
            {
                float xAxis = Random.Range(-25, 25);
                float yAxis = Random.Range(monk.transform.position.y + 50, monk.transform.position.y + 150);
                Instantiate(rocketPrefab, new Vector3(xAxis, yAxis, 0), Quaternion.identity);
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return new WaitForSeconds(3);
            }
		}
	}
	
	//toggle pause state
	private GameState togglePause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
            return GameState.Paused;
		}
		else{
			Time.timeScale = 1;
            return GameState.GameStart;
		}
	}
}
