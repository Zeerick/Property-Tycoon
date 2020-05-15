using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayScript : MonoBehaviour
{
    public bool toFreeParking;
    public int amount;
    GameObject freeParking;

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

    public void Setup(bool fp, bool jail, int am)
    {
        toFreeParking = fp;
        amount = am;
        freeParking = gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerControllerScript>().GetSpaceOfType("Free Parking");
        bool getOutOfJailFree = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().getOutOfJailFree;
        string desc;
        if (toFreeParking && !jail)
        {
            desc = "Pay £" + amount.ToString() + " to " + freeParking.gameObject.GetComponent<FreeParkingScript>().spaceName;
        } else if (toFreeParking && jail && !getOutOfJailFree)
        {
            desc = "Pay £" + amount.ToString() + " bribe";
        } else if (toFreeParking && jail && getOutOfJailFree)
        {
            desc = "Use Get Out Of Jail Free Card";
            amount = 0;
        } else
        {
            desc = "Pay £" + amount.ToString();
        }
        gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = desc;
    }

    public void Setup(bool fp, int am)
    {
        Setup(fp, false, am);
    }

    public void PayTo()
    {
        if (amount <= gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().Pay(amount);
            freeParking.gameObject.GetComponent<FreeParkingScript>().pot += amount;
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().MoveDone();
        }
    }

    public void PayJail()
    {
        if (amount <= gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().Pay(amount);
            freeParking.gameObject.GetComponent<FreeParkingScript>().pot += amount;
            gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().turnsLeftInJail = 0;
            if (gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().getOutOfJailFree)
            {
                gameObject.transform.parent.parent.gameObject.GetComponent<PlayerScript>().getOutOfJailFree = false;
            }
            gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerControllerScript>().reRoll.Invoke();
        }
    }
}
