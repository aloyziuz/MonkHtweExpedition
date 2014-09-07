using UnityEngine;
using System.Collections;

public class MonkScript : MonoBehaviour {
	public int currentSpeed;
	public bool isBoosted;
	public float boostDuration;
	private const int MAXSPEED = 10;
	private const int MINSPEED = -20;
	public int speedBefore;

	// Use this for initialization
	void Start () {
		currentSpeed = 10;
		isBoosted = false;
	}

	void Update(){
		if(isBoosted){
			boostDuration -= Time.deltaTime;
			if(boostDuration <= 0){
				isBoosted = false;
				SetSpeed(speedBefore);
			}
		}
	}
	
	public void IncreaseSpeed(int value){
		SetSpeed(currentSpeed + value);
	}

	public void DecreaseSpeed(int value){
		SetSpeed(currentSpeed - value);
	}

	public void SetSpeed(int value){
		speedBefore = currentSpeed;
		currentSpeed = value;
        if (speedBefore > MAXSPEED)
        {
            speedBefore = MAXSPEED;
        }
        if (IsAboveMaxSpeed() && !isBoosted)
        {
            currentSpeed = MAXSPEED;
        }
		Vector2 movement = new Vector2(0, currentSpeed);
		this.rigidbody2D.velocity = movement;
	}

	public bool IsAboveMaxSpeed(){
		if(this.currentSpeed >= MAXSPEED)
			return true;
		return false;
	}

	public bool IsBelowMinSpeed(){
		if(this.currentSpeed <= MINSPEED)
			return true;
		return false;
	}

    public bool IsSpeedNegative()
    {
        if (currentSpeed < 0)
        {
            return true;
        }
        return false;
    }
}
