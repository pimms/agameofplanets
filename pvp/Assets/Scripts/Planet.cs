﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : Body {
	public enum PlayerSide {
		PLAYER_UNDEFINED,
		PLAYER_LEFT,
		PLAYER_RIGHT,
		PLANET_AI,
	}
	
	protected static List<Planet> PlayerPlanets = new List<Planet>();
	
	public Body mOrbitBody;
	public float mOrbitDistance;
	
	public PlayerSide mPlayerSide = PlayerSide.PLAYER_UNDEFINED;
	public Aim mAim;
	
	public float mOrbitXFactor = 1f;
	public float mOrbitYFactor = 1f;
	
	public float mTimer = 0f;
	
	protected int mHealth = 100;
	public int health {
		get { return mHealth; }
		set { mHealth = value; }
	}
	
	
	protected override void Start() {
		base.Start();
		
		if (mPlayerSide == PlayerSide.PLAYER_LEFT) {
			PlayerPlanets.Add(this);
			mAim.LeftKey = KeyCode.Q;
			mAim.RightKey = KeyCode.E;
			mAim.FireKey = KeyCode.W;
		} else if (mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			PlayerPlanets.Add(this);
			mAim.LeftKey = KeyCode.I;
			mAim.RightKey = KeyCode.P;
			mAim.FireKey = KeyCode.O;
		}
		else if (mPlayerSide ==PlayerSide.PLANET_AI) {
			
			renderer.material.color = Color.green;	
		}
	}
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		Vector3 position = mOrbitBody.transform.position;
		
		// Calculate the distance to the parent body
		position.x += Mathf.Cos(mTimer * 0.5f) * mOrbitXFactor * mOrbitDistance;
		position.y += Mathf.Sin(mTimer * 0.5f) * mOrbitYFactor * mOrbitDistance;
		
		transform.position = position;
	}
	
	protected override void OnRocketCollide(Rocket rocket) {
		mHealth -= 10;
		
		if (mHealth <= 0) {
			if (mPlayerSide == PlayerSide.PLAYER_LEFT) {
				Debug.Log("Left got PÅWND!");	
			}
			
			else {
				Debug.Log("Right got PÅWND!");	
			}
			
			Debug.Log (mHealth);
			
		}
		if (mPlayerSide == PlayerSide.PLANET_AI) {
				Debug.Log("AI ");	
				renderer.material.color = Color.red;
				
				Planet target = PlayerPlanets[FindClosestPlayer()];
				transform.LookAt(target.transform.position);
				mAim.FireRocketAI(transform.position);
		}
	}
	
	protected int FindClosestPlayer() {
		
		float DistRightPlayer;
		float DistLeftPlater;
		int numRightPlayer;
		int numLeftPlayer;
		
		if( PlayerPlanets[0].mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			
			DistRightPlayer = Vector3.Distance(PlayerPlanets[0].transform.position, this.transform.position);
			numRightPlayer = 0;
			DistLeftPlater = Vector3.Distance(PlayerPlanets[1].transform.position, this.transform.position);
			numLeftPlayer = 1;
		}
		else {
			
			DistRightPlayer = Vector3.Distance(PlayerPlanets[1].transform.position, this.transform.position);
			numRightPlayer = 1;
			DistLeftPlater = Vector3.Distance(PlayerPlanets[0].transform.position, this.transform.position);
			numLeftPlayer = 0;
		}
		
		if (DistLeftPlater < DistRightPlayer) {
			
			return numLeftPlayer;
		}
		else {
			
			return numRightPlayer;	
		}
	}
	
	
}
