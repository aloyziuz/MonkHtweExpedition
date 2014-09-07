using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	private float moveSpeed;
    internal float time;
    private GameObject bar;

	void Start () {
        bar = GameObject.Find("bar");
        float distance = bar.GetComponent<SpriteRenderer>().bounds.size.x;
        time += 0.3f;
        moveSpeed = distance / time;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
	}
}
