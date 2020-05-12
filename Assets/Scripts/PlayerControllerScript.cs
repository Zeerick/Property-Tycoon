using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject turnOrderPrefab;
    public GameObject boardController;
    public GameObject[] players;
    public Color[] colors = new Color[6];
    public int currentPlayer = 0;
    int doubles;
    bool doubleRolled;

    public UnityEvent reRoll = new UnityEvent();
    public UnityEvent endTurn = new UnityEvent();

    void Start()
    {
        gameObject.transform.Find("Game UI").gameObject.SetActive(false);
        gameObject.transform.Find("Game UI").gameObject.transform.Find("End Turn Button").gameObject.SetActive(false);
        doubles = 0;
        doubleRolled= false;
    }

    public void Setup(int numOfPlayers)
    {
        players = new GameObject[numOfPlayers];
        for (int i = 0; i < numOfPlayers; i++)
        {
            GameObject instance = Instantiate(prefab, new Vector3(5.5f,0.25f,-5.5f), Quaternion.Euler(0,0,0), gameObject.transform);
            instance.GetComponent<PlayerScript>().playerNo = i;
            instance.GetComponent<PlayerScript>().playerName = "Player " + (i + 1).ToString();
            instance.GetComponent<PlayerScript>().color = colors[i];
            players[i] = instance;
            GameObject instanceTurnOrder = Instantiate(turnOrderPrefab, new Vector3(0f,0f,0f), Quaternion.Euler(0,0,0), gameObject.transform.Find("Game UI").gameObject.transform.Find("Turn Order").gameObject.transform);
            instanceTurnOrder.GetComponent<TurnOrderScript>().player = players[i];
            instanceTurnOrder.GetComponent<TurnOrderScript>().controller = gameObject;
        }
    }

    public void Rolled(int dice1, int dice2)
    {
        if (dice1 == dice2)
        {
            doubles += 1;
            doubleRolled= true;
        }
        players[currentPlayer].GetComponent<PlayerScript>().Move(dice1, dice2);
    }

    public void MoveDone()
    {
        if (doubles >= 3)
        {
            players[currentPlayer].GetComponent<PlayerScript>().InJail();
            endTurn.Invoke();
        } else if (doubleRolled)
        {
            doubleRolled = false;
            reRoll.Invoke();
        } else
        {
            endTurn.Invoke();
        }
    }

    public void EndTurn()
    {
        players[currentPlayer].gameObject.transform.Find("Player UI").gameObject.SetActive(false);
        currentPlayer = (currentPlayer + 1) % players.Length;
        players[currentPlayer].gameObject.transform.Find("Player UI").gameObject.SetActive(true);
        doubles = 0;
        doubleRolled = false;
        reRoll.Invoke();
    }
}
