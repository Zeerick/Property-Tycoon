using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControllerScript : MonoBehaviour
{
    public class Card
    {
        public string description;
        public bool option;
        public int income;
        public int incomeFromPlayers;
        public int pay;
        public int payToFP;
        public int payPerHouse;
        public int payPerHotel;
        public bool move;
        public bool passGo;
        public int moveBy;
        public int moveTo;
        public bool getOutOfJailFree;

        public Card(string[] line)
        {
            description = line[0];
            option = line[9] == "Yes";
            income = int.Parse(line[12]);
            incomeFromPlayers = int.Parse(line[13]);
            pay = int.Parse(line[14]);
            payToFP = int.Parse(line[15]);
            payPerHouse = int.Parse(line[16]);
            payPerHotel = int.Parse(line[17]);
            move = line[18] == "Yes";
            passGo = line[19] == "Yes";
            moveBy = int.Parse(line[20]);
            moveTo = int.Parse(line[21]);
            getOutOfJailFree = line[22] == "Yes";
        }
    }

    public class Deck
    {
        public List<Card> deck = new List<Card>();
        public string name;

        public Deck(string n, string[][] lines)
        {
            name = n;
            foreach (string[] line in lines)
            {
                deck.Add(new Card(line));
            }
        }
    }

    public Deck[] decks = new Deck[2];
    public GameObject playerController;
    public GameObject boardController;
    public Card activeCard;
    public PlayerScript activePlayer;

    void Start()
    {
        transform.Find("Card UI").gameObject.SetActive(false);
        transform.Find("Card UI").Find("OK").gameObject.SetActive(false);
        transform.Find("Card UI").Find("Option 1").gameObject.SetActive(false);
        transform.Find("Card UI").Find("Option 2").gameObject.SetActive(false);
    }

    public void Setup(string csv)
    {
        string[] lines = csv.Split('\n');
        string[][] cells = new string[lines.Length][];
        for (int l = 0; l < lines.Length; l++)
        {
            cells[l] = lines[l].Split(',');
        }

        int startPoint = 3;
        int endPoint = startPoint;
        for (int i = 0; i < 2; i++)
        {
            string name = cells[startPoint - 2][0];
            while (cells[endPoint][0] != null && cells[endPoint][0] != "")
            {
                endPoint++;
            }
            endPoint--;
            string[][] deckSection = new string[endPoint - startPoint][];
            for (int c = startPoint; c < endPoint; c++)
            {
                deckSection[c - startPoint] = cells[c];
            }
            decks[i] = new Deck(name, deckSection);
            startPoint = endPoint + 4;
            endPoint = startPoint;
        }
    }

    public void TakeCard(string deckName, PlayerScript player)
    {
        activePlayer = player;
        foreach (Deck deck in decks)
        {
            if (deck.name == deckName)
            {
                activeCard = deck.deck[0];
                deck.deck.RemoveAt(0);
                deck.deck.Add(activeCard);
                transform.Find("Card UI").gameObject.SetActive(true);
                transform.Find("Card UI").Find("Card Back").Find("Title").gameObject.GetComponent<UnityEngine.UI.Text>().text = deckName;
                transform.Find("Card UI").Find("Card Back").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = activeCard.description;
                if (activeCard.option)
                {
                    transform.Find("Card UI").Find("Option 1").gameObject.SetActive(true);
                    transform.Find("Card UI").Find("Option 2").gameObject.SetActive(true);
                } else
                {
                    transform.Find("Card UI").Find("OK").gameObject.SetActive(true);
                }
            }
        }
    }

    public void TakeCard(string deckName, GameObject player)
    {
        TakeCard(deckName, player.gameObject.GetComponent<PlayerScript>());
    }

    public void TakeCardOption()
    {
        TakeCard(decks[1].name, activePlayer);
    }

    public void TakeAction()
    {
        activePlayer.Income(activeCard.income);
        foreach (GameObject player in playerController.gameObject.GetComponent<PlayerControllerScript>().players)
        {
            if (player.gameObject.activeSelf && player.gameObject.GetComponent<PlayerScript>().money >= 10)
            {
                player.gameObject.GetComponent<PlayerScript>().Pay(activeCard.incomeFromPlayers);
                activePlayer.Income(activeCard.incomeFromPlayers);
            }
        }
        activePlayer.Pay(activeCard.pay);
        activePlayer.Pay(activeCard.payToFP);
        boardController.gameObject.GetComponent<BoardControllerScript>().AddToFPPot(activeCard.payToFP);
        activePlayer.Pay(activeCard.payPerHouse * boardController.gameObject.GetComponent<BoardControllerScript>().NumberOfOwnedHouses(activePlayer.playerNo));
        activePlayer.Pay(activeCard.payPerHotel * boardController.gameObject.GetComponent<BoardControllerScript>().NumberOfOwnedHotels(activePlayer.playerNo));
        if (activeCard.getOutOfJailFree)
        {
            activePlayer.getOutOfJailFree = true;
        }
        if (activeCard.move)
        {
            if (activeCard.moveBy > 0)
            {
                activePlayer.Move(activeCard.moveBy);
            } else if (activeCard.passGo)
            {
                activePlayer.Move(((activeCard.moveTo - 1) - activePlayer.currentSpace) % boardController.gameObject.GetComponent<BoardControllerScript>().board.Length);
            } else
            {
                activePlayer.MoveDirect(activeCard.moveTo - 1);
                activePlayer.MoveDone();
            }
        } else
        {
            activePlayer.MoveDone();
        }
    }
}
