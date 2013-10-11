﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Body : MonoBehaviour {
	// All Body-objects add themselves to this list
	protected static List<Body> sBodies = new List<Body>();
	
	// The velocity of the body.
	protected Vector2 mVelocity = new Vector2();
	public Vector2 Velocity {
		get { return mVelocity; }
	}
	
	// The mass of the body
	protected float mMass;
	public float Mass {
		get { return mMass; }
		set { mMass = value; }
	}
	
	void Start () {
		sBodies.Add (this);	
	}
	
	void Update () {
		UpdateVelocity();
		
		Vector3 nPos = transform.position;
		nPos.x += mVelocity.x;
		nPos.y += mVelocity.y;
		transform.position = nPos;
	}
	
	// Update velocity by acceleration from the 
	protected virtual void UpdateVelocity() {
		Vector2 acceleration = new Vector2();
		
		foreach (Body tempBody in sBodies) {
			if (tempBody != this) {
				
				//m1*m2/dist*dist
				float massProduct = tempBody.Mass * mMass;
				Vector3 distance = tempBody.transform.position - transform.position;
				acceleration.x += massProduct / (distance.x * distance.x);
				acceleration.y += massProduct / (distance.y * distance.y);
			}
		}
		
		mVelocity += acceleration;
	}
}

