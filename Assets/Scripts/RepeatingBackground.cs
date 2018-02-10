using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour 
{
	public bool firstTile;
	private float backgroundLength;				// A float to store the length of the attached GameObject.
	private Vector3 startPosition;
	private float resetPosition;
	private Transform myTransform;
	private float lastResetTime;
	private static float backgroundScrollSpeed = -5f;
	
	private void Awake ()
	{
		lastResetTime = 0;
		myTransform = GetComponent<Transform>();
		backgroundLength = GetComponent<Renderer>().bounds.size.z;
		startPosition = myTransform.localPosition;
		resetPosition = startPosition.z - backgroundLength * 2f;
		if (firstTile)
			resetPosition += backgroundLength;
	}
	
	//Update runs once per frame
	private void Update()
	{
		//Check if the difference along the x axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
		if (myTransform.localPosition.z < resetPosition)
		{
			Debug.Log ("Object has fallen lower than its length. Resetting...");
			//If true, this means this object is no longer visible and we can safely move it forward to be re-used.
			RepositionBackground ();
		}
		else
		{
			float newPosition = (Time.time - lastResetTime) * backgroundScrollSpeed;
			transform.localPosition = startPosition + (Vector3.forward * newPosition);
		}
	}
	
	//Moves the object this script is attached to right in order to create our looping background effect.
	private void RepositionBackground()
	{
		
		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		myTransform.localPosition = new Vector3
		(
			myTransform.localPosition.x,
			myTransform.localPosition.y,
			myTransform.localPosition.z + (backgroundLength * 2f)
		);
		lastResetTime = Time.time;
		firstTile = false;
	}
	
	public void setScrollSpeed(float newScrollSpeed)
	{
		backgroundScrollSpeed = newScrollSpeed;
	}
}