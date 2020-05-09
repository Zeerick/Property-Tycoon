using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 dice1Velocity;
	Vector3 dice2Velocity;
	int diceNumber1;
	int diceNumber2;
	bool stopped;
	float timer;

	[System.Serializable]
    public class Rolled : UnityEvent<int,int> {}

	public UnityEvent reRoll = new UnityEvent();

    public Rolled DiceRolled;

	void Start()
	{
		stopped = true;
	}

	void FixedUpdate ()
	{
		dice1Velocity = DiceScript.dice1Velocity;
		dice2Velocity = DiceScript.dice2Velocity;
		timer += Time.deltaTime;
		if (!stopped && timer > 15f)
		{
			reset();
			reRoll.Invoke();
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (!stopped)
		{
			if (dice1Velocity.x == 0f && dice1Velocity.y == 0f && dice1Velocity.z == 0f && dice2Velocity.x == 0f && dice2Velocity.y == 0f && dice2Velocity.z == 0f) {
				if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.position.y < 0.4 && col.gameObject.transform.parent.name == "dice1") {
					switch (col.gameObject.name) {
						case "Side1":
							diceNumber1 = 6;
							break;
						case "Side2":
							diceNumber1 = 5;
							break;
						case "Side3":
							diceNumber1 = 4;
							break;
						case "Side4":
							diceNumber1 = 3;
							break;
						case "Side5":
							diceNumber1 = 2;
							break;
						case "Side6":
							diceNumber1 = 1;
							break;
					}
				}
				else if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.position.y < 0.4 && col.gameObject.transform.parent.name == "dice2") {
					switch (col.gameObject.name) {
						case "Side1":
							diceNumber2 = 6;
							break;
						case "Side2":
							diceNumber2 = 5;
							break;
						case "Side3":
							diceNumber2 = 4;
							break;
						case "Side4":
							diceNumber2 = 3;
							break;
						case "Side5":
							diceNumber2 = 2;
							break;
						case "Side6":
							diceNumber2 = 1;
							break;
					}
				}
				if (diceNumber1 != 0 && diceNumber2 != 0)
				{
					DiceRolled.Invoke(diceNumber1, diceNumber2);
					stopped = true;
				}
			}
		}
	}

	public void reset()
	{
		diceNumber1 = 0;
		diceNumber2 = 0;
		stopped = false;
		timer = 0.0f;
	}
}
