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
    public bool getOutOfJailFree = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        money = 1500;
        turnsLeftInJail = 0;
        boardLaps = -1;
        if (playerNo == 0)
        {
            gameObject.transform.Find("Player UI").gameObject.SetActive(true);
        } else
        {
            gameObject.transform.Find("Player UI").gameObject.SetActive(false);
        }
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.SetActive(false);
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.SetActive(false);
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay").gameObject.SetActive(false);
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Jail").gameObject.SetActive(false);
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
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay").gameObject.SetActive(true);
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay").gameObject.GetComponent<PayScript>().Setup(true, tile.gameObject.GetComponent<ActionTileScript>().amount);
                    break;
                case "Take Card":
                    gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().TakeCard(tile.gameObject.GetComponent<ActionTileScript>().deck);
                    break;
            }
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
        if (currentSpace == targetSpace)
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

    public void JailFine()
    {
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Jail").gameObject.SetActive(true);
        gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Jail").gameObject.GetComponent<PayScript>().Setup(true, true, 50);
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
        PropertyScript script = tile.gameObject.GetComponent<PropertyScript>() as PropertyScript;
        currentSpace = script.space;
        if (currentSpace == targetSpace) //Landed on a Property
        {
            if (script.owned)
            {
                if (!script.mortgaged && script.ownerNo != playerNo)
                {
                    int cost = script.rent[script.houses];
                    if (script.houses == 0 && script.IsSetComplete())
                    {
                        cost = cost * 2;
                    }
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.SetActive(true);
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.GetComponent<PayRentScript>().Setup(script.ownerNo, cost);
                } else
                {
                    MoveDone();
                }
            } else
            {
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.SetActive(true);
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.transform.Find("Buy").gameObject.GetComponent<BuyScript>().Setup(tile);
            }
        }
    }

    void Station(GameObject tile)
    {
        StationScript script = tile.gameObject.GetComponent<StationScript>() as StationScript;
        currentSpace = script.space;
        if (currentSpace == targetSpace)
        {
            if (script.owned)
            {
                if (!script.mortgaged && script.ownerNo != playerNo)
                {
                    int cost = script.rent[script.StationsOwned() - 1];
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.SetActive(true);
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.GetComponent<PayRentScript>().Setup(script.ownerNo, cost);
                } else
                {
                    MoveDone();
                }
            } else
            {
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.SetActive(true);
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.transform.Find("Buy").gameObject.GetComponent<BuyScript>().Setup(tile);
            }
        }
    }

    void Utility(GameObject tile)
    {
        UtilityScript script = tile.gameObject.GetComponent<UtilityScript>() as UtilityScript;
        currentSpace = script.space;
        if (currentSpace == targetSpace)
        {
            if (script.owned)
            {
                if (!script.mortgaged && script.ownerNo != playerNo)
                {
                    int cost = script.multiplier[script.UtilitiesOwned() - 1] * (gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().dice1 + gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().dice2);
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.SetActive(true);
                    gameObject.transform.Find("Player UI").gameObject.transform.Find("Pay Rent").gameObject.GetComponent<PayRentScript>().Setup(script.ownerNo, cost);
                } else
                {
                    MoveDone();
                }
            } else
            {
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.SetActive(true);
                gameObject.transform.Find("Player UI").gameObject.transform.Find("Property Available").gameObject.transform.Find("Buy").gameObject.GetComponent<BuyScript>().Setup(tile);
            }
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

    public void MoveDirect(int spaceNo)
    {
        transform.position = BoardControllerScript.SpacePosition(spaceNo, gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length, playerNo, jailStatus);
    }

    public void Income(int amount)
    {
        money += amount;
    }

    public void Pay(int amount)
    {
        money -= amount;
    }

    public void AcquireProperty(GameObject property)
    {
        if(property.gameObject.GetComponent<PropertyScript>() != null)
        {
            property.gameObject.GetComponent<PropertyScript>().owned = true;
            property.gameObject.GetComponent<PropertyScript>().ownerNo = playerNo;
        } else if(property.gameObject.GetComponent<StationScript>() != null)
        {
            property.gameObject.GetComponent<StationScript>().owned = true;
            property.gameObject.GetComponent<StationScript>().ownerNo = playerNo;
        } else if(property.gameObject.GetComponent<UtilityScript>() != null)
        {
            property.gameObject.GetComponent<UtilityScript>().owned = true;
            property.gameObject.GetComponent<UtilityScript>().ownerNo = playerNo;
        }
    }

    public void MoveDone()
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().MoveDone();
    }

    public void Auction()
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().Auction();
    }

    public void DeclareBankruptcy()
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().DeclareBankruptcy(playerNo);
    }
}
