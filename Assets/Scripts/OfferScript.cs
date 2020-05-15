using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferScript : MonoBehaviour
{
    public int currentOffer = 0;
    public bool activePlayer;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "£" + currentOffer.ToString();
            if (activePlayer)
            {
                gameObject.transform.Find("Title").gameObject.GetComponent<UnityEngine.UI.Text>().text = gameObject.transform.parent.parent.gameObject.GetComponent<TradeControllerScript>().player1.gameObject.GetComponent<PlayerScript>().playerName + " Offer";
            } else
            {
                gameObject.transform.Find("Title").gameObject.GetComponent<UnityEngine.UI.Text>().text = gameObject.transform.parent.parent.gameObject.GetComponent<TradeControllerScript>().player2.gameObject.GetComponent<PlayerScript>().playerName + " Offer";
            }
            if ((currentOffer > gameObject.transform.parent.parent.gameObject.GetComponent<TradeControllerScript>().player1.gameObject.GetComponent<PlayerScript>().money && activePlayer) || (currentOffer > gameObject.transform.parent.parent.gameObject.GetComponent<TradeControllerScript>().player2.gameObject.GetComponent<PlayerScript>().money && !activePlayer))
            {
                gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            } else
            {
                gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
            }
        }
    }

    public void ChangeOffer(int amount)
    {
        currentOffer += amount;
    }
}
