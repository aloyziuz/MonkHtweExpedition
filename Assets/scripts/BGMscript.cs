using UnityEngine;
using System.Collections;

public class BGMscript : MonoBehaviour {
    public GameObject monk;
	public GameObject accordion, bell, violin, harp, tabla;
    public GameObject bellIconPrefab, disturbanceIconPrefab;
    public GameObject ratingSpawnPoint, goodPrefab, badPrefab;
	public bool isClicked, isChecked;
	internal bool soundControl1, soundControl2, soundControl3, soundControl4;
	internal enum MusicState{ Stage1, Stage2, Stage3, Stage4 };
	internal MusicState state;
    private bool isSupposedToBeClicked;
    private GameObject spawnPoint, bar;
    private MonkScript monkScript;
    private MainControlScript mainScript;
    private int random;
    private float tempo;
    private float life;
	
	// Use this for initialization
	void Start () {
        life = 0.5f;
        bar = GameObject.Find("bar");
        spawnPoint = GameObject.Find("notesSpawnPoint");
		mainScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainControlScript>();
		tempo = 1.0f;
		isSupposedToBeClicked = false;
		isClicked = false;
		soundControl1 = false;
		soundControl2 = false;
		soundControl3 = false;
		soundControl4 = false;
		monkScript = (MonkScript)monk.GetComponent<MonkScript>();
		state = MusicState.Stage1;

		if (mainScript.state == MainControlScript.GameState.Waiting) {
			bell.audio.mute = false;
			violin.audio.mute = false;
			harp.audio.mute = false;
			tabla.audio.mute = false;
			accordion.audio.mute = false;
		}	
		StartCoroutine (AllRhythms());
	}
	
	// Update is called once per frame
	void Update () {
		if (mainScript.state == MainControlScript.GameState.GameStart) {
			bell.audio.mute = false;
			violin.audio.mute = false;
			harp.audio.mute = false;
			tabla.audio.mute = false;	
			accordion.audio.mute = false;
		}
		else if (mainScript.state == MainControlScript.GameState.GameOver) {
			bell.audio.mute = true;
			violin.audio.mute = true;
			harp.audio.mute = true;
			tabla.audio.mute = true;
			accordion.audio.mute = true;
		}
		StartCoroutine (AllRhythms());
	}

	IEnumerator AllRhythms(){
		switch (state)
		{
		case MusicState.Stage1:
			if (soundControl1 == false){
				soundControl1 = true;
				StartCoroutine ("Rhythm1");
			}
			yield break;
		case MusicState.Stage2:
			if (soundControl2 == false){
				soundControl2 = true;
				StartCoroutine ("Rhythm2");
			}
			yield break;
		case MusicState.Stage3:
			if (soundControl3 == false){
				soundControl3 = true;
				StartCoroutine ("Rhythm3");
			}
			yield break;
		default:
			if (soundControl4 == false) {
				soundControl4 = true;
				StartCoroutine ("Rhythm4");
			}
			yield break;
		}
	}

	IEnumerator Rhythm1(){
		while (true) {
			harp.audio.Play();
			harp.audio.pitch = 0.7f;
			harp.audio.volume = 1.0f;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//1
			CreateBellIcon(tempo); 
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//2
			isSupposedToBeClicked = true;           
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
		}
	}
	
	IEnumerator Rhythm2(){
		while (true) {
			CreateBellIcon(tempo);
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//1
			CreateDisturbanceIcon(tempo);
			isSupposedToBeClicked = true;            
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//2
			CreateBellIcon(tempo);
			tabla.audio.Play();
			tabla.audio.volume = 0.3f;
			tabla.audio.pitch = 0.8f;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();

            CreateBellIcon(tempo);
			isSupposedToBeClicked = true;            
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//3
			CreateDisturbanceIcon(tempo);
			isSupposedToBeClicked = true;            
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//4
			CreateBellIcon(tempo);
			tabla.audio.Play();
			tabla.audio.volume = 0.3f;
			tabla.audio.pitch = 0.8f;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//5
			isSupposedToBeClicked = true;            
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
		}
	}
	
	IEnumerator Rhythm3(){
		while (true) {
            CreateBellIcon(1.55f);
			yield return new WaitForSeconds(1.5f);
			CheckRhythm();
			ResetBoolean();
			//1
			violin.audio.Play();
			violin.audio.volume = 0.4f;
			yield return new WaitForSeconds(0.05f);
			CheckRhythm();
			ResetBoolean();	

			isSupposedToBeClicked = true;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
			ResetBoolean();
			//2
			CreateDisturbanceIcon(tempo);
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
			ResetBoolean();
						
		}
	}
	
	IEnumerator Rhythm4(){
		while (true) {
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//1
			CreateDisturbanceIcon(tempo);
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
			//2
			CreateBellIcon(tempo);
			accordion.audio.Play();
			accordion.audio.volume = 0.6f;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();

			isSupposedToBeClicked = true;
			yield return new WaitForSeconds(tempo);
			CheckRhythm();
            ResetBoolean();
		}
	}
	
	internal void CheckRhythm()
	{
        if (!isChecked && mainScript.state == MainControlScript.GameState.GameStart)
        {
			//correct tap
			if (isSupposedToBeClicked && isClicked)
			{
				monkScript.IncreaseSpeed(2);
                GameObject goodIcon = CreateGoodIcon();
                Destroy(goodIcon, life);
			}
			
			//missed the beat
			else if (isSupposedToBeClicked && !isClicked)
			{
				monkScript.DecreaseSpeed(5);
                GameObject badIcon = CreateBadIcon();
                Destroy(badIcon, life);
			}
			
			//missed tap
			else if (!isSupposedToBeClicked && isClicked)
			{
				monkScript.DecreaseSpeed(5);
                GameObject badIcon = CreateBadIcon();
                Destroy(badIcon, life);
			}

            isChecked = true;
        }
	}
	
	private void ResetBoolean()
	{
		isSupposedToBeClicked = false;
		isClicked = false;
        isChecked = false;
	}

    private GameObject CreateGoodIcon()
    {
        GameObject a = (GameObject)Instantiate(goodPrefab, ratingSpawnPoint.transform.position, Quaternion.Euler(0, 0, 18));
        a.transform.parent = bar.transform;
        return a;
    }

    private GameObject CreateBadIcon()
    {
        GameObject a = (GameObject)Instantiate(badPrefab, ratingSpawnPoint.transform.position, Quaternion.Euler(0, 0, -18));
        a.transform.parent = bar.transform;
        return a;
    }

    private void CreateBellIcon(float time)
    {
        GameObject a = (GameObject)Instantiate(bellIconPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -3.2f), Quaternion.identity);
        a.transform.parent = bar.transform;
        a.GetComponent<Note>().time = time;
    }

    private void CreateDisturbanceIcon(float time)
    {
        GameObject a = (GameObject)Instantiate(disturbanceIconPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -3.0f), Quaternion.identity);
        a.transform.parent = bar.transform;
        a.GetComponent<Note>().time = time;
    }
}
