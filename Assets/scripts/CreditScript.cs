using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {
	public Texture creditFrameTexture;
	public Texture backBtnTexture;
	public GUISkin startMenuSkin;
	
	void OnGUI(){
		var screenWidth = camera.pixelWidth;
		var screenHeight = camera.pixelHeight;
		
		GUI.skin = startMenuSkin;
		
		GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), creditFrameTexture);
		
		if(GUI.Button(new Rect(screenWidth/2 - 150, (screenHeight/2)+180, 80,40), backBtnTexture)){
			Application.LoadLevel("StartScreen");
		}
		
	}
}