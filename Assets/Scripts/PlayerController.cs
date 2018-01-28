using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	public float playerSpeed;
	public float tiltAmount; // ~ 1800f is a full rotation with this setup
	public float tiltSpeed;
	public Boundary boundary;

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate ()
	{
	// ** Player Controls
		Rigidbody playerRigidbody = GetComponent<Rigidbody>();
		// Input.GetAxis("Horizontal/Vertical") return a number between -1 and 1
		// Multiplying a movement value by Time.deltaTime means move by this amount per second instead of per frame
		float moveHorizontal = Input.GetAxisRaw ("Horizontal") * playerSpeed * Time.deltaTime;
		float moveVertical = Input.GetAxisRaw ("Vertical") * playerSpeed * Time.deltaTime;
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		playerRigidbody.velocity = movement;
		
		// Restricts player movement without allowing any elastic movement past clamp limit
		playerRigidbody.position = new Vector3 
			(
				Mathf.Clamp (playerRigidbody.position.x + moveHorizontal, boundary.xMin, boundary.xMax),
				0.0f, 
				Mathf.Clamp (playerRigidbody.position.z + moveVertical, boundary.zMin, boundary.zMax)
			);

		// Rotates Player as they move left/right
		Quaternion tiltTarget = Quaternion.Euler (0f, 0f, moveHorizontal * -tiltAmount);
		transform.rotation = Quaternion.Slerp (playerRigidbody.rotation, tiltTarget, Time.deltaTime * tiltSpeed);
	// End Player controls **
	}
} 
