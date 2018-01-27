using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float boltSpeed;
	
	void Start () {
		GetComponent<Rigidbody>().velocity = GetComponent<Transform>().forward * boltSpeed;
	}
	
}
