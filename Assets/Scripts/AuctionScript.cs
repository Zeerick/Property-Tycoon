using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AuctionScript : MonoBehaviour
{
    public GameObject auctionOrderPrefab;
    public GameObject[] auction;
    public GameObject property;

    public int numOfParticipants;
    public int currentAuctioneer;

    public UnityEvent auctionEnded = new UnityEvent();

    public void Setup(int currentPlayer)
    {
        numOfParticipants = 0;
        currentAuctioneer = currentPlayer;
        auction = new GameObject[gameObject.transform.parent.parent.gameObject.GetComponent<PlayerControllerScript>().totalPlayers];
        foreach (GameObject player in gameObject.transform.parent.parent.gameObject.GetComponent<PlayerControllerScript>().players)
        {
            if (player.gameObject.activeSelf)
            {
                auction[numOfParticipants] = Instantiate(auctionOrderPrefab, new Vector3(0f,0f,0f), Quaternion.Euler(0,0,0), gameObject.transform);
                auction[numOfParticipants].GetComponent<AuctionOrderScript>().player = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerControllerScript>().players[numOfParticipants];
                auction[numOfParticipants].GetComponent<AuctionOrderScript>().controller = gameObject.transform.parent.parent.gameObject;
                numOfParticipants++;
            }
        }
        int space = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerControllerScript>().players[currentPlayer].gameObject.GetComponent<PlayerScript>().currentSpace;
        property = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().board[space];
    }

    public int AuctionOrderPosition(int playerNo)
    {
        int order = 0;
        for (int i = currentAuctioneer; i != playerNo; i = (i + 1) % auction.Length)
        {
            GameObject auctioneer = auction[i];
            if (auctioneer.gameObject.activeSelf)
            {
                order++;
            }
        }
        return order;
    }

    public void NextBid()
    {
        do {
            currentAuctioneer = (currentAuctioneer + 1) % auction.Length;
        } while (!auction[currentAuctioneer].gameObject.activeSelf);
        gameObject.transform.Find("Current Bid").gameObject.GetComponent<CurrentBidScript>().currentBid = auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().currentBid;
    }

    public void SubmitBid()
    {
        if (gameObject.transform.Find("Current Bid").gameObject.GetComponent<CurrentBidScript>().currentBid > BestBid() && gameObject.transform.Find("Current Bid").gameObject.GetComponent<CurrentBidScript>().currentBid <= PlayerMoney())
        {
            auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().currentBid = gameObject.transform.Find("Current Bid").gameObject.GetComponent<CurrentBidScript>().currentBid;
            NextBid();
        }
    }

    public void Withdraw()
    {
        auction[currentAuctioneer].gameObject.SetActive(false);
        NextBid();
        numOfParticipants--;
        if (numOfParticipants == 1)
        {
            auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().player.gameObject.GetComponent<PlayerScript>().Pay(auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().currentBid);
            auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().player.gameObject.GetComponent<PlayerScript>().AcquireProperty(property);
            foreach (GameObject a in auction)
            {
                Object.Destroy(a);
            }
            auctionEnded.Invoke();
        }
    }

    public int BestBid()
    {
        int bestBid = 0;
        foreach (GameObject auctioneer in auction)
        {
            if (auctioneer.gameObject.activeSelf && auctioneer.gameObject.GetComponent<AuctionOrderScript>().currentBid > bestBid)
            {
                bestBid = auctioneer.gameObject.GetComponent<AuctionOrderScript>().currentBid;
            }
        }
        return bestBid;
    }

    public int PlayerMoney()
    {
        return auction[currentAuctioneer].gameObject.GetComponent<AuctionOrderScript>().player.gameObject.GetComponent<PlayerScript>().money;
    }
}
