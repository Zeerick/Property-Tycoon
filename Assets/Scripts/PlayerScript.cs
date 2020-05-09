using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerNo;
    public string playerName;
    public Color color;
    public int currentSpace;
    int targetSpace;
    string jailStatus;
    public int money;
    public int turnsLeftInJail;
    int boardLaps;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        money = 1500;
        turnsLeftInJail = 0;
        boardLaps = -1;
    }

    void FixedUpdate()
    {
        jailStatus = "";
        if (turnsLeftInJail > 0)
        {
            jailStatus = "In Jail";
            transform.position = BoardControllerScript.SpacePosition(targetSpace, gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length, playerNo, jailStatus);
        } else if (currentSpace == gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().GetTypeLocation("Jail"))
        {
            jailStatus = "Just Visiting";
        }
        if (targetSpace != currentSpace)
        {
            transform.position = Vector3.MoveTowards(transform.position, BoardControllerScript.SpacePosition(currentSpace + 1, gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length, playerNo, jailStatus), 0.2f);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, BoardControllerScript.SpacePosition(targetSpace, gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length, playerNo, jailStatus), 0.2f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.parent.name == "Board Controller")
        {
            if(col.gameObject.GetComponent<ActionTileScript>() != null)
            {
                Action(col.gameObject);
            } else if(col.gameObject.GetComponent<FreeParkingScript>() != null)
            {
                FreeParking(col.gameObject);
            } else if(col.gameObject.GetComponent<GoScript>() != null)
            {
                Go(col.gameObject);
            } else if(col.gameObject.GetComponent<GoToJailScript>() != null)
            {
                GoToJail(col.gameObject);
            } else if(col.gameObject.GetComponent<JailScript>() != null)
            {
                Jail(col.gameObject);
            } else if(col.gameObject.GetComponent<PropertyScript>() != null)
            {
                Property(col.gameObject);
            } else if(col.gameObject.GetComponent<StationScript>() != null)
            {
                Station(col.gameObject);
            } else if(col.gameObject.GetComponent<UtilityScript>() != null)
            {
                Utility(col.gameObject);
            }
        }
    }

    void Action(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<ActionTileScript>().space;
        if (currentSpace == targetSpace) //Landed on action tile
        {
            switch (tile.gameObject.GetComponent<ActionTileScript>().type)
            {
                case "Tax":
                    gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().AddToFPPot(tile.gameObject.GetComponent<ActionTileScript>().amount);
                    Pay(tile.gameObject.GetComponent<ActionTileScript>().amount);
                    break;
                case "Take Card":
                    break;
            }
            MoveDone();
        }
    }

    void FreeParking(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<FreeParkingScript>().space;
        if (currentSpace == targetSpace) //Landed on Free Parking
        {
            Income(tile.gameObject.GetComponent<FreeParkingScript>().TakePot());
            MoveDone();
        }
    }

    void Go(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<GoScript>().space;
        boardLaps += 1;
        if (boardLaps > 0)
        {
            money += tile.gameObject.GetComponent<GoScript>().salary;
            if (currentSpace == targetSpace) //Landed on Go
            {
                MoveDone();
            }
        }
    }

    void GoToJail(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<GoToJailScript>().space;
        if (currentSpace == targetSpace) //Landed on Go To Jail
        {
            InJail();
            MoveDone();
        }
    }

    public void InJail()
    {
        turnsLeftInJail = 3;
        targetSpace = gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().GetTypeLocation("Jail");
    }

    void Jail(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<JailScript>().space;
        if (currentSpace == targetSpace) //Landed on Jail
        {
            MoveDone();
        }
    }

    void Property(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<PropertyScript>().space;
        if (currentSpace == targetSpace) //Landed on a Property
        {
            MoveDone();
        }
    }

    void Station(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<StationScript>().space;
        if (currentSpace == targetSpace) //Landed on a Station
        {
            MoveDone();
        }
    }

    void Utility(GameObject tile)
    {
        currentSpace = tile.gameObject.GetComponent<UtilityScript>().space;
        if (currentSpace == targetSpace) //Landed on a Utility
        {
            MoveDone();
        }
    }

    public void Move(int d1, int d2)
    {
        if (turnsLeftInJail > 0 && d1 != d2)
        {
            turnsLeftInJail -= 1;
            if (turnsLeftInJail == 0)
            {
                Pay(50);
                Move(d1 + d2);
            }
            MoveDone();
        } else {
            Move(d1 + d2);
        }
    }

    public void Move(int spaces)
    {
        targetSpace = (currentSpace + spaces) % 40;
    }

    public void Income(int amount)
    {
        money += amount;
    }

    public void Pay(int amount)
    {
        money -= amount;
    }

    public void MoveDone()
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().MoveDone();
    }
}
