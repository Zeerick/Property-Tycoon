using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject turnOrderPrefab;
    public GameObject boardController;
    public GameObject cardController;
    public GameObject[] players;
    public int totalPlayers;
    public Color[] colors = new Color[6];
    public int currentPlayer = 0;
    public int dice1;
    public int dice2;
    int doubles;
    bool doubleRolled;

    public UnityEvent reRoll = new UnityEvent();
    public UnityEvent endTurn = new UnityEvent();

    void Start()
    {
        gameObject.transform.Find("Game UI").gameObject.SetActive(false);
        gameObject.transform.Find("Game UI").gameObject.transform.Find("End Turn Button").gameObject.SetActive(false);
        gameObject.transform.Find("Game UI").gameObject.transform.Find("Auction").gameObject.SetActive(false);
        doubles = 0;
        doubleRolled = false;
    }

    public void Setup(int numOfPlayers)
    {
        totalPlayers = numOfPlayers;
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

    public void Rolled(int d1, int d2)
    {
        dice1 = d1;
        dice2 = d2;
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
        do {
            currentPlayer = (currentPlayer + 1) % players.Length;
        } while (!players[currentPlayer].gameObject.activeSelf);
        players[currentPlayer].gameObject.transform.Find("Player UI").gameObject.SetActive(true);
        doubles = 0;
        doubleRolled = false;
        if (players[currentPlayer].gameObject.GetComponent<PlayerScript>().turnsLeftInJail > 1)
        {
            players[currentPlayer].gameObject.transform.Find("Player UI").Find("Pay Jail").gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(360, 130);
            players[currentPlayer].gameObject.GetComponent<PlayerScript>().JailFine();
            reRoll.Invoke();
        } else if (players[currentPlayer].gameObject.GetComponent<PlayerScript>().turnsLeftInJail == 1)
        {
            players[currentPlayer].gameObject.transform.Find("Player UI").Find("Pay Jail").gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(360, 80);
            players[currentPlayer].gameObject.GetComponent<PlayerScript>().JailFine();
        } else
        {
            reRoll.Invoke();
        }
    }

    public void ReRoll()
    {
        players[currentPlayer].gameObject.GetComponent<PlayerScript>().gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Jail").gameObject.SetActive(false);
    }

    public void Auction()
    {
        gameObject.transform.Find("Game UI").Find("Auction").gameObject.SetActive(true);
        gameObject.transform.Find("Game UI").Find("Auction").gameObject.GetComponent<AuctionScript>().Setup(currentPlayer);
    }

    public void AuctionDone()
    {
        players[currentPlayer].gameObject.transform.Find("Player UI").gameObject.SetActive(true);
        players[currentPlayer].gameObject.transform.Find("Player UI").Find("Property Available").gameObject.SetActive(false);
        players[currentPlayer].gameObject.GetComponent<PlayerScript>().MoveDone();
    }

    public void DeclareBankruptcy(int playerNo)
    {
        foreach (GameObject space in boardController.gameObject.GetComponent<BoardControllerScript>().board)
        {
            if (space.gameObject.GetComponent<PropertyScript>() != null)
            {
                if (space.gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
                {
                    space.gameObject.GetComponent<PropertyScript>().owned = false;
                    space.gameObject.GetComponent<PropertyScript>().ownerNo = 0;
                    space.gameObject.GetComponent<PropertyScript>().houses = 0;
                }
            } else if (space.gameObject.GetComponent<StationScript>() != null)
            {
                if (space.gameObject.GetComponent<StationScript>().ownerNo == playerNo)
                {
                    space.gameObject.GetComponent<StationScript>().owned = false;
                    space.gameObject.GetComponent<StationScript>().ownerNo = 0;
                }
            } else if (space.gameObject.GetComponent<UtilityScript>() != null)
            {
                if (space.gameObject.GetComponent<UtilityScript>().ownerNo == playerNo)
                {
                    space.gameObject.GetComponent<UtilityScript>().owned = false;
                    space.gameObject.GetComponent<UtilityScript>().ownerNo = 0;
                }
            }
        }
        players[playerNo].gameObject.SetActive(false);
        totalPlayers--;
        endTurn.Invoke();
    }

    public void TakeCard(string deckName)
    {
        cardController.gameObject.GetComponent<CardControllerScript>().TakeCard(deckName, players[currentPlayer]);
    }

    public int TurnOrderPosition(int playerNo)
    {
        int order = 0;
        for (int i = currentPlayer; i != playerNo; i = (i + 1) % players.Length)
        {
            GameObject player = players[i];
            if (player.gameObject.activeSelf)
            {
                order++;
            }
        }
        return order;
    }

    public GameObject GetSpaceOfType(string type)
    {
        return boardController.gameObject.GetComponent<BoardControllerScript>().board[boardController.gameObject.GetComponent<BoardControllerScript>().GetTypeLocation(type)];
    }
}
