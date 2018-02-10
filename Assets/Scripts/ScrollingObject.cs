using UnityEngine;
using System.Collections;

public class ScrollingObject : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
		tileSizeZ = GetComponent<Renderer>().bounds.size.z;
	}
	
	void Update ()
	{
		float newPosition = Time.time * scrollSpeed;
		transform.position = startPosition + (Vector3.forward * newPosition);
	}
}
