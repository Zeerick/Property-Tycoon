using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayRentScript : MonoBehaviour
{
    public int playerNo;
    public int amount;

    void Update()
    {
        if (amount > gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        } else
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
        }
    }

    public void Setup(int pN, int am)
    {
        playerNo = pN;
        amount = am;
        gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Pay £" + amount.ToString() + " to " + gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].GetComponent<PlayerScript>().playerName;
    }

    public void PayTo()
    {
        if (amount <= gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().Pay(amount);
            gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].GetComponent<PlayerScript>().Income(amount);
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().MoveDone();
        }
    }
}
