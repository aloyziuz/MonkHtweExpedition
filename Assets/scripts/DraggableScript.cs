using UnityEngine;
using System.Collections;

public class DraggableScript : MonoBehaviour {
	public Transform monk;

	void Start () {
		monk = GameObject.FindGameObjectWithTag("Monk").transform;
	}
	
	void Update () {
		if(this.gameObject.transform.position.y < monk.position.y - 50){
			Destroy(this.gameObject);
		}
	}
	
	//update the icon position
	public void MouseClick(RaycastHit2D hit){
		this.gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, -1);
	}
	
	//check collision with monk
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Monk"){
			MonkScript script = (MonkScript) other.gameObject.GetComponent("MonkScript");
			if(!script.isBoosted){
				script.isBoosted = true;
				script.boostDuration = 5.0f;
				script.IncreaseSpeed(10);
				Destroy(this.gameObject);
			}
			else{
				script.boostDuration = 5.0f;
				Destroy(this.gameObject);
			}
		}
	}
}
