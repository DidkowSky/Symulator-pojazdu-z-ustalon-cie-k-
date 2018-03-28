using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour {

	public Transform path;
	public Transform prefab;

	private List<Transform> nodes;
	private Transform bezier;
	private Transform child;

	// Use this for initialization
	void Start () {
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != path.transform) {
				nodes.Add(pathTransforms[i]);
				//print (i);
				//print (nodes);
			}
		}

		Vector3 previousNode = new Vector3();
		Vector3 currentNode = new Vector3();

		bezier = GetComponent<Transform> ();
		//print(bezier);

		for(int i = 0; i < nodes.Count; i++){
			if( i != 0){
				currentNode = nodes [i].position;
				previousNode = nodes [i - 1].position;
			}else if( i == 0 && nodes.Count != 0){
				currentNode = nodes [i].position;
				previousNode = nodes[nodes.Count -1].position;
			}
		}	

		for(float i=0; i<1; i+=0.02f){
			countBezier(nodes, i);
		}
	}

	float factorial(float n)
	{
		if (n<=1)
			return(1);
		else
			n=n*factorial(n-1);
		return n;
	}

	float binomial_coff(float n,float k)
	{
		float ans;
		ans = factorial(n) / (factorial(k)*factorial(n-k));
		return ans;
	}

	void countBezier(List<Transform> PT, float t){

		if(PT.Count < 4)
			return;

		Vector3 P = new Vector3(0,0,0);

		for(int i=0;i<PT.Count;i++)
		{
			P.x = P.x + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.x;
			P.y = P.y + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.y;
			P.z = P.z + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.z;
		}

		prefab.position = P;
		child = Instantiate (prefab);
		child.parent = bezier;
	}

	void countBezierGizmos(List<Transform> PT, float t){

		if(PT.Count < 4)
			return;

		Vector3 P = new Vector3(0,0,0);

		for(int i=0;i<PT.Count;i++)
		{
			P.x = P.x + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.x;
			P.y = P.y + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.y;
			P.z = P.z + binomial_coff((PT.Count - 1), i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (PT.Count - 1 - i)) * PT[i].position.z;
		}

		Gizmos.DrawSphere (P, 1);
	}


	void OnDrawGizmos(){
		
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != path.transform) {
				nodes.Add(pathTransforms[i]);
			}
		}

		Vector3 previousNode = new Vector3();
		Vector3 currentNode = new Vector3();
		for(int i = 0; i < nodes.Count; i++){
			if( i != 0){
				currentNode = nodes [i].position;
				previousNode = nodes [i - 1].position;
			}else if( i == 0 && nodes.Count != 0){
				currentNode = nodes [i].position;
				previousNode = nodes[nodes.Count -1].position;
			}
		}

		for(float i=0; i<1; i+=0.02f){
			countBezierGizmos(nodes, i);
		}
	}
}
