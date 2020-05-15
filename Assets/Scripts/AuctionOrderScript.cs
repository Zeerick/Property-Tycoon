using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionOrderScript : MonoBehaviour
{
    public GameObject player;
    public GameObject controller;

    public int currentBid = 0;

    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerScript>().playerName + " £" + currentBid.ToString();
        gameObject.GetComponent<UnityEngine.UI.Text>().color = player.GetComponent<PlayerScript>().color;
        gameObject.transform.localPosition = new Vector3(0,80 - (gameObject.transform.parent.gameObject.GetComponent<AuctionScript>().AuctionOrderPosition(player.gameObject.GetComponent<PlayerScript>().playerNo) * 30), 0);
    }
}
