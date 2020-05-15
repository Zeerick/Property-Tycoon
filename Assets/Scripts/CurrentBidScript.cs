using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBidScript : MonoBehaviour
{
    public int currentBid = 0;

    void Update()
    {
        gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "£" + currentBid.ToString();
        if (currentBid <= gameObject.transform.parent.gameObject.GetComponent<AuctionScript>().BestBid() || currentBid > gameObject.transform.parent.gameObject.GetComponent<AuctionScript>().PlayerMoney())
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            gameObject.transform.parent.Find("Submit").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        } else
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
            gameObject.transform.parent.Find("Submit").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
        }
    }

    public void ChangeBid(int amount)
    {
        currentBid += amount;
    }
}
