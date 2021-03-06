﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSelectScript : MonoBehaviour
{
    public int maxPlayers;
    public int minPlayers;
    int noOfPlayers;

    [System.Serializable]
    public class SubmitEvent : UnityEvent<int> {}

    public SubmitEvent StartGame;

    // Start is called before the first frame update
    void Start()
    {
        noOfPlayers = minPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = noOfPlayers.ToString();
    }

    public void Increase()
    {
        if (noOfPlayers < maxPlayers) {
            noOfPlayers += 1;
        }
    }

    public void Decrease()
    {
        if (noOfPlayers > minPlayers) {
            noOfPlayers -= 1;
        }
    }

    public void Submit()
    {
        StartGame.Invoke(noOfPlayers);
    }
}
