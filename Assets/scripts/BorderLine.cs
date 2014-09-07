using UnityEngine;
using System.Collections;

public class BorderLine : MonoBehaviour {
    public GameObject bgm;
    public GameObject mainCamera;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monk")
        {
			// determine which stage it is by using the name of the colliderr
            string name = this.gameObject.name;
            switch (name)
            {
                case "stage2":
                    bgm.GetComponent<BGMscript>().state = BGMscript.MusicState.Stage2;
                    break;

                case "stage3":
                    bgm.GetComponent<BGMscript>().state = BGMscript.MusicState.Stage3;
                    break;

                case "stage4":
                    bgm.GetComponent<BGMscript>().state = BGMscript.MusicState.Stage4;
                    break;

                case "finish":
                    other.gameObject.GetComponent<MonkScript>().SetSpeed(0);
                    mainCamera.GetComponent<MainControlScript>().state = MainControlScript.GameState.GameOver;
                    break;
            }
        }
    }
}
