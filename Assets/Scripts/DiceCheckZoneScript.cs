using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 dice1Velocity;
	Vector3 dice2Velocity;

	// Update is called once per frame
	void FixedUpdate () {
		dice1Velocity = DiceScript.dice1Velocity;
		dice2Velocity = DiceScript.dice2Velocity;
	}

	void OnTriggerStay(Collider col)
	{
		if (dice1Velocity.x == 0f && dice1Velocity.y == 0f && dice1Velocity.z == 0f && dice2Velocity.x == 0f && dice2Velocity.y == 0f && dice2Velocity.z == 0f) {
			if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.position.y < 1.5 && col.gameObject.transform.parent.name == "dice1") {
				switch (col.gameObject.name) {
					case "Side1":
						DiceNumberTextScript.diceNumber1 = 6;
						break;
					case "Side2":
						DiceNumberTextScript.diceNumber1 = 5;
						break;
					case "Side3":
						DiceNumberTextScript.diceNumber1 = 4;
						break;
					case "Side4":
						DiceNumberTextScript.diceNumber1 = 3;
						break;
					case "Side5":
						DiceNumberTextScript.diceNumber1 = 2;
						break;
					case "Side6":
						DiceNumberTextScript.diceNumber1 = 1;
						break;
				}
			}
			else if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.position.y < 1.5 && col.gameObject.transform.parent.name == "dice2") {
				switch (col.gameObject.name) {
					case "Side1":
						DiceNumberTextScript.diceNumber2 = 6;
						break;
					case "Side2":
						DiceNumberTextScript.diceNumber2 = 5;
						break;
					case "Side3":
						DiceNumberTextScript.diceNumber2 = 4;
						break;
					case "Side4":
						DiceNumberTextScript.diceNumber2 = 3;
						break;
					case "Side5":
						DiceNumberTextScript.diceNumber2 = 2;
						break;
					case "Side6":
						DiceNumberTextScript.diceNumber2 = 1;
						break;
				}
			}
		}
	}
}
