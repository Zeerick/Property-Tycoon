using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour {

	Text text;
	public static int diceNumber1;
	public static int diceNumber2;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		string testText;
		if (diceNumber1 != 0 && diceNumber2 != 0) {
			testText = diceNumber1.ToString() + " + " + diceNumber2.ToString() + " = " + (diceNumber1 + diceNumber2).ToString();
			if (diceNumber1 == diceNumber2) {
				testText = testText + " DOUBLE!!!";
			}
			text.text = testText;
		}
	}
}
