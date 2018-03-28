using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

	public WheelCollider wheelColider;

	private Vector3 wheelPos = new Vector3 ();
	private Quaternion wheelRot = new Quaternion ();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		wheelColider.GetWorldPose (out wheelPos, out wheelRot);
		transform.position = wheelPos;
		transform.rotation = wheelRot;		
	}
}
