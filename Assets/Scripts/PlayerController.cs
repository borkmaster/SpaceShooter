using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	public float playerSpeed;
	public float tiltAmount; // ~ 1800f is a full rotation with this setup
	public float tiltSpeed;
	public Boundary boundary;

	void FixedUpdate ()
	{
		// Controls and restricts player movement without allowing any elastic movement past clamp limit
		Rigidbody playerRigidbody = GetComponent<Rigidbody>();
		float moveHorizontal = Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		playerRigidbody.velocity = movement;
		
		playerRigidbody.position = new Vector3 
			(
				Mathf.Clamp (playerRigidbody.position.x + moveHorizontal, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp (playerRigidbody.position.z + moveVertical, boundary.zMin, boundary.zMax)
			);

		// Smoothly rotates player as they move left/right
		Quaternion tiltTarget = Quaternion.Euler (0f, 0f, moveHorizontal * -tiltAmount);
		transform.rotation = Quaternion.Slerp (playerRigidbody.rotation, tiltTarget, Time.deltaTime * tiltSpeed);
	}
} 
