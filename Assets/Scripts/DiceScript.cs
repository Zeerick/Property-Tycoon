using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb1;
	static Rigidbody rb2;
	public static Vector3 dice1Velocity;
	public static Vector3 dice2Velocity;

	public int initX;

	// Use this for initialization
	void Start () {
		switch (gameObject.name) {
			case "dice1":
				rb1 = GetComponent<Rigidbody> ();
				break;
			case "dice2":
				rb2 = GetComponent<Rigidbody> ();
				break;
		}
	}

	// Update is called once per frame
	void Update () {
		dice1Velocity = rb1.velocity;
		dice2Velocity = rb2.velocity;

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (dice1Velocity.x == 0f && dice1Velocity.y == 0f && dice1Velocity.z == 0f && dice2Velocity.x == 0f && dice2Velocity.y == 0f && dice2Velocity.z == 0f) {
				DiceNumberTextScript.diceNumber1 = 0;
				DiceNumberTextScript.diceNumber2 = 0;
				float dirX1 = Random.Range (-500, 500);
				float dirY1 = Random.Range (-500, 500);
				float dirZ1 = Random.Range (-500, 500);
				float dirX2 = Random.Range (-500, 500);
				float dirY2 = Random.Range (-500, 500);
				float dirZ2 = Random.Range (-500, 500);
				transform.position = new Vector3 (initX, 2, 0);
				transform.rotation = Quaternion.identity;
				rb1.AddForce (transform.up * 250);
				rb2.AddForce (transform.up * 250);
				rb1.AddTorque (dirX1, dirY1, dirZ1);
				rb2.AddTorque (dirX2, dirY2, dirZ2);
			}
		}
	}
}
