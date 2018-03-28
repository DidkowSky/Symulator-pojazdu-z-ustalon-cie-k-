using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaeEngine : MonoBehaviour {

	public Transform path;
	public bool braking = false;
	public int speed = 200;
	public WheelCollider fl_Wheel;
	public WheelCollider fr_Wheel;
	public WheelCollider bl_Wheel;
	public WheelCollider br_Wheel;

	private List<Transform> nodes;
	private int currentNode = 0;
	private float angle = 30f;	
	//car sensors settings
	private float length = 5f;
	private Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
	private float frontSideSensorPosition = 0.2f;


	// Use this for initialization
	void Start () {
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != path.transform) {
				nodes.Add(pathTransforms[i]);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Sensors();
		Steer ();
		Drive ();
		CheckNodeDistance ();
		Braking ();

		//print (currentNode);
	}

	private void Sensors() {
		RaycastHit hit;
		Vector3 sensorStartPos = transform.position;
		sensorStartPos += transform.forward * frontSensorPosition.z;

		//front right sensor
		sensorStartPos += transform.right * frontSideSensorPosition;
		if (Physics.Raycast(sensorStartPos, transform.forward, out hit, length)) {
			Debug.DrawLine(sensorStartPos, hit.point);
			braking = true;
		}

		//front left sensor
		sensorStartPos -= transform.right *2 * frontSideSensorPosition;
		if (Physics.Raycast(sensorStartPos, transform.forward, out hit, length)) {
			Debug.DrawLine(sensorStartPos, hit.point);
			braking = true;
		}
	}

	private void Steer(){
		Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
		//print (relativeVector);
		float newSteerAngle = (relativeVector.x / relativeVector.magnitude) * angle;
		//print (newSteerAngle);

		fl_Wheel.steerAngle = newSteerAngle;
		fr_Wheel.steerAngle = newSteerAngle;
	}

	private void Drive(){
		if (!braking) {
			fl_Wheel.motorTorque = speed;
			fr_Wheel.motorTorque = speed;
		}else{
			fl_Wheel.motorTorque = 0;
			fr_Wheel.motorTorque = 0;			
		}
	}

	private void CheckNodeDistance(){
		if (Vector3.Distance (transform.position, nodes[currentNode].position) < 2f) {
			if (currentNode == nodes.Count -1) {
				currentNode = 0;
			} else {
				currentNode++;
			}
		}
	}

	private void Braking(){
		if (braking) {
			bl_Wheel.brakeTorque = 150;
			br_Wheel.brakeTorque = 150;
		} else {
			bl_Wheel.brakeTorque = 0;
			br_Wheel.brakeTorque = 0;
		}
	}
}
