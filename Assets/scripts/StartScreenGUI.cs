using UnityEngine;
using System.Collections;

public class StartScreenGUI : MonoBehaviour {
	public Texture playBtnTexture;
	public Texture highScoreBtnTexture;
	public Texture startMenuFrameTexture;
	public Texture monkTexture;
	public Texture cloudTexture;
	public Texture titleTexture;
	public Texture creditBtnTexture;
	public GUISkin startMenuSkin;
	
	void OnGUI(){
		var screenWidth = camera.pixelWidth;
		var screenHeight = camera.pixelHeight;


		GUI.skin = startMenuSkin;

		GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), startMenuFrameTexture);
		GUI.DrawTexture(new Rect(screenWidth / 2 -145, screenHeight / 2 +120 , 150, 120), cloudTexture);
		GUI.DrawTexture(new Rect(screenWidth / 2 -145, screenHeight / 2  , 150, 180), monkTexture);
		GUI.DrawTexture(new Rect(screenWidth / 2 - 150, screenHeight / 2 - 200 , 300, 100), titleTexture);
		if(GUI.Button(new Rect(screenWidth/2, (screenHeight/2) , 150, 65), playBtnTexture)){
			Application.LoadLevel("GameScreen");
		}
		if(GUI.Button(new Rect(screenWidth/2, (screenHeight/2)+70 , 150, 65), highScoreBtnTexture)) {
			//go to highscore
		}
		if(GUI.Button(new Rect(screenWidth/2, (screenHeight/2)+140, 150,65), creditBtnTexture)){
			Debug.Log("lol");
			Application.LoadLevel("CreditScreen");
			//GUI.skin = startMenuSkin;
			//GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), creditFrameTexture);
			//if(GUI.Button(new Rect((screenWidth/2) - 70, (screenHeight/2)- 90, 150, 65), backBtnTexture)){
			
			//}
		}
	}
}
