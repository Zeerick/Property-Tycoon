using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject turnOrderPrefab;
    public GameObject[] players;
    public Color[] colors = new Color[6];
    public int currentPlayer = 0;

    void Start()
    {
        gameObject.transform.Find("Game UI").gameObject.SetActive(false);
    }

    public void Setup(int numOfPlayers)
    {
        players = new GameObject[numOfPlayers];
        for (int i = 0; i < numOfPlayers; i++)
        {
            GameObject instance = Instantiate(prefab, new Vector3(5.5f,0.25f,-5.5f), Quaternion.Euler(0,0,0), gameObject.transform);
            instance.GetComponent<PlayerScript>().playerNo = i;
            instance.GetComponent<PlayerScript>().name = "Player " + (i + 1).ToString();
            instance.GetComponent<PlayerScript>().color = colors[i];
            players[i] = instance;
            GameObject instanceTurnOrder = Instantiate(turnOrderPrefab, new Vector3(0f,0f,0f), Quaternion.Euler(0,0,0), gameObject.transform.Find("Game UI").gameObject.transform.Find("Turn Order").gameObject.transform);
            instanceTurnOrder.GetComponent<TurnOrderScript>().player = players[i];
            instanceTurnOrder.GetComponent<TurnOrderScript>().controller = gameObject;
        }
    }

    public void Rolled(int dice1, int dice2)
    {
        players[currentPlayer].GetComponent<PlayerScript>().Move(dice1 + dice2);
    }

    public void EndTurn()
    {
        currentPlayer = (currentPlayer + 1) % players.Length;
    }
}
