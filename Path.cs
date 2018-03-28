using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public Color color; // kolor lini łączącej węzły sterujące

	private List<Transform> nodes = new List<Transform> ();

	void OnDrawGizmos(){
		Gizmos.color = color;

		Transform[] pathTransforms = GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms[i] != transform) {
				nodes.Add(pathTransforms[i]);
			}
		}

		Vector3 previousNode = new Vector3();
		Vector3 currentNode = new Vector3();
		//rysowanie lini pomiędzy wązłami
		for(int i = 0; i < nodes.Count; i++){
			if( i != 0){
				currentNode = nodes [i].position;
				previousNode = nodes [i - 1].position;
			}
			Gizmos.DrawLine (previousNode, currentNode);
		}
	}
}
