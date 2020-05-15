using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeControllerScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public int offer1Money;
    public int offer2Money;

    public List<int> properties1;
    public List<int> properties2;

    public int currentSelection;

    void Start()
    {
        gameObject.SetActive(false);
        transform.Find("Trade UI").Find("Offer 1").gameObject.SetActive(false);
        transform.Find("Trade UI").Find("Offer 2").gameObject.SetActive(false);
        transform.Find("Trade UI").Find("Make Deal").gameObject.SetActive(false);

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Find("Trade UI").Find("Trade With").Find("Player Selector").gameObject.GetComponent<UnityEngine.UI.Text>().text = transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[currentSelection].gameObject.GetComponent<PlayerScript>().playerName;
        }
    }

    public void NextPlayer()
    {
        do {
            currentSelection = (currentSelection + 1) % transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length;
        } while (!transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[currentSelection].gameObject.activeSelf || currentSelection == transform.parent.gameObject.GetComponent<PlayerControllerScript>().currentPlayer);
    }

    public void PrevPlayer()
    {
        do {
            currentSelection = (currentSelection - 1 + transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length) % transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length;
        } while (!transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[currentSelection].gameObject.activeSelf || currentSelection == transform.parent.gameObject.GetComponent<PlayerControllerScript>().currentPlayer);
    }

    public void SelectPlayer()
    {
        player1 = transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[transform.parent.gameObject.GetComponent<PlayerControllerScript>().currentPlayer];
        player2 = transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[currentSelection];
        Setup();
    }

    public void Setup()
    {
        offer1Money = 0;
        offer2Money = 0;
        properties1 = new List<int>();
        properties2 = new List<int>();
        transform.Find("Trade UI").Find("Offer 1").gameObject.SetActive(true);
        transform.Find("Trade UI").Find("Offer 2").gameObject.SetActive(true);
        transform.Find("Trade UI").Find("Make Deal").gameObject.SetActive(true);
    }

    public void MakeDeal()
    {
        offer1Money = transform.Find("Trade UI").Find("Offer 1").gameObject.GetComponent<OfferScript>().currentOffer;
        offer2Money = transform.Find("Trade UI").Find("Offer 2").gameObject.GetComponent<OfferScript>().currentOffer;

        PlayerScript script1 = player1.gameObject.GetComponent<PlayerScript>();
        PlayerScript script2 = player2.gameObject.GetComponent<PlayerScript>();

        if (offer1Money <= script1.money && offer2Money <= script2.money)
        {
            script1.Pay(offer1Money);
            script2.Income(offer1Money);
            script2.Pay(offer2Money);
            script1.Income(offer2Money);
            transform.Find("Trade UI").Find("Offer 1").gameObject.SetActive(false);
            transform.Find("Trade UI").Find("Offer 2").gameObject.SetActive(false);
            transform.Find("Trade UI").Find("Make Deal").gameObject.SetActive(false);
            transform.Find("Trade UI").Find("Trade With").gameObject.SetActive(true);
            gameObject.SetActive(false);
            transform.parent.gameObject.GetComponent<PlayerControllerScript>().ReturnFromTrade();
            transform.parent.Find("Game UI").gameObject.SetActive(true);
        }
    }
}
