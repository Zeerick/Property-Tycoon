using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControllerScript : MonoBehaviour
{
    public GameObject PlayerController;

    public GameObject Action;
    public GameObject FreeParking;
    public GameObject Go;
    public GameObject GoToJail;
    public GameObject Jail;
    public GameObject Property;
    public GameObject Station;
    public GameObject Utility;

    public GameObject[] board = new GameObject[40];

    string[] names = new string[40];
    string[] group = new string[40];
    Color[] color = new Color[40];
    string[] type = new string[40];
    int[] amount = new int[40];
    string[] deck = new string[40];
    int[] price = new int[40];
    int[] housePrice = new int[40];
    int[][] rent = new int[40][];

    public void Setup(string csv)
    {
        string[] lines = csv.Split('\n');
        string[][] cells = new string[lines.Length][];
        for (int l = 0; l < lines.Length; l++)
        {
            cells[l] = lines[l].Split(',');
        }

        for (int row = 0; row < 40; row++)
        {
            string[] line = cells[row + 4];
            names[row] = line[1];
            group[row] = line[3];
            color[row] = new Color(float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]));
            type[row] = line[9];
            amount[row] = int.Parse(line[10]);
            deck[row] = line[11];
            price[row] = int.Parse(line[13]);
            housePrice[row] = int.Parse(line[15]);
            rent[row] = new int[6];
            rent[row][0] = int.Parse(line[14]);
            rent[row][1] = int.Parse(line[16]);
            rent[row][2] = int.Parse(line[17]);
            rent[row][3] = int.Parse(line[18]);
            rent[row][4] = int.Parse(line[19]);
            rent[row][5] = int.Parse(line[20]);
        }

        CreateBoard();
    }

    void CreateBoard()
    {
        for (int space = 0; space < 40; space++)
        {
            switch (type[space]) {
                case "Free Parking":
                    board[space] = CreateFreeParking(space, names[space]);
                    break;
                case "Go":
                    board[space] = CreateGo(space, names[space], amount[space]);
                    break;
                case "Go To Jail":
                    board[space] = CreateGoToJail(space, names[space]);
                    break;
                case "Jail":
                    board[space] = CreateJail(space, names[space]);
                    break;
                case "Property":
                    board[space] = CreateProperty(space, names[space], group[space], color[space], price[space], housePrice[space], rent[space]);
                    break;
                case "Station":
                    board[space] = CreateStation(space, names[space], color[space], price[space], rent[space]);
                    break;
                case "Take Card":
                    board[space] = CreateTakeCard(space, names[space], deck[space]);
                    break;
                case "Tax":
                    board[space] = CreateTax(space, names[space], amount[space]);
                    break;
                case "Utility":
                    board[space] = CreateUtility(space, names[space], color[space], price[space], rent[space]);
                    break;
            }
        }
    }

    GameObject CreateTakeCard(int space, string spaceName, string spaceDeck)
    {
        return CreateAction(space, spaceName, "Take a\n" + spaceDeck + "\ncard from the deck", "Take Card", 0, spaceDeck);
    }

    GameObject CreateTax(int space, string spaceName, int spaceAmount)
    {
        return CreateAction(space, spaceName, "Pay £" + spaceAmount.ToString() + "\nto\n" + names[Array.IndexOf(type, "Free Parking")], "Tax", spaceAmount, "");
    }

    GameObject CreateAction(int space, string spaceName, string description, string spaceType, int spaceAmount, string spaceDeck)
    {
        GameObject instance = Instantiate(Action, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<ActionTileScript>().space = space;
        instance.GetComponent<ActionTileScript>().spaceName = spaceName;
        instance.GetComponent<ActionTileScript>().description = description;
        instance.GetComponent<ActionTileScript>().type = spaceType;
        instance.GetComponent<ActionTileScript>().amount = spaceAmount;
        instance.GetComponent<ActionTileScript>().deck = spaceDeck;
        return instance;
    }

    GameObject CreateFreeParking(int space, string spaceName)
    {
        GameObject instance = Instantiate(FreeParking, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<FreeParkingScript>().space = space;
        instance.GetComponent<FreeParkingScript>().spaceName = spaceName;
        return instance;
    }

    GameObject CreateGo(int space, string spaceName, int salary)
    {
        GameObject instance = Instantiate(Go, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<GoScript>().space = space;
        instance.GetComponent<GoScript>().spaceName = spaceName;
        instance.GetComponent<GoScript>().salary = salary;
        return instance;
    }

    GameObject CreateGoToJail(int space, string spaceName)
    {
        GameObject instance = Instantiate(GoToJail, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<GoToJailScript>().space = space;
        instance.GetComponent<GoToJailScript>().spaceName = spaceName;
        instance.GetComponent<GoToJailScript>().targetSpace = Array.IndexOf(type, "Jail");
        return instance;
    }

    GameObject CreateJail(int space, string spaceName)
    {
        GameObject instance = Instantiate(Jail, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<JailScript>().space = space;
        instance.GetComponent<JailScript>().spaceName = spaceName;
        return instance;
    }

    GameObject CreateProperty(int space, string spaceName, string groupName, Color spaceColor, int spacePrice, int spaceHousePrice, int[] spaceRent)
    {
        GameObject instance = Instantiate(Property, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<PropertyScript>().space = space;
        instance.GetComponent<PropertyScript>().streetName = spaceName;
        instance.GetComponent<PropertyScript>().group = groupName;
        instance.GetComponent<PropertyScript>().color = spaceColor;
        instance.GetComponent<PropertyScript>().price = spacePrice;
        instance.GetComponent<PropertyScript>().housePrice = spaceHousePrice;
        instance.GetComponent<PropertyScript>().rent = spaceRent;
        return instance;
    }

    GameObject CreateStation(int space, string spaceName, Color spaceColor, int spacePrice, int[] spaceRent)
    {
        GameObject instance = Instantiate(Station, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<StationScript>().space = space;
        instance.GetComponent<StationScript>().stationName = spaceName;
        instance.GetComponent<StationScript>().color = spaceColor;
        instance.GetComponent<StationScript>().price = spacePrice;
        instance.GetComponent<StationScript>().rent = new int[4];
        instance.GetComponent<StationScript>().rent[0] = spaceRent[0];
        instance.GetComponent<StationScript>().rent[1] = spaceRent[1];
        instance.GetComponent<StationScript>().rent[2] = spaceRent[2];
        instance.GetComponent<StationScript>().rent[3] = spaceRent[3];
        return instance;
    }

    GameObject CreateUtility(int space, string spaceName, Color spaceColor, int spacePrice, int[] spaceRent)
    {
        GameObject instance = Instantiate(Utility, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<UtilityScript>().space = space;
        instance.GetComponent<UtilityScript>().spaceName = spaceName;
        instance.GetComponent<UtilityScript>().color = spaceColor;
        instance.GetComponent<UtilityScript>().price = spacePrice;
        instance.GetComponent<UtilityScript>().multiplier = new int[2];
        instance.GetComponent<UtilityScript>().multiplier[0] = spaceRent[0];
        instance.GetComponent<UtilityScript>().multiplier[1] = spaceRent[1];
        return instance;
    }

    public int GetTypeLocation(string t)
    {
        return Array.IndexOf(type, t);
    }

    public void AddToFPPot(int amount)
    {
        board[GetTypeLocation("Free Parking")].gameObject.GetComponent<FreeParkingScript>().pot += amount;
    }

    public int NumberOfOwnedProperties(int playerNo)
    {
        int total = 0;
        foreach (GameObject space in board)
        {
            if (space.gameObject.GetComponent<PropertyScript>() != null && space.gameObject.GetComponent<PropertyScript>().owned && space.gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
            {
                total++;
            } else if (space.gameObject.GetComponent<StationScript>() != null && space.gameObject.GetComponent<StationScript>().owned && space.gameObject.GetComponent<StationScript>().ownerNo == playerNo)
            {
                total++;
            } else if (space.gameObject.GetComponent<UtilityScript>() != null && space.gameObject.GetComponent<UtilityScript>().owned && space.gameObject.GetComponent<UtilityScript>().ownerNo == playerNo)
            {
                total++;
            }
        }
        return total;
    }

    public int NumberOfOwnedHouses(int playerNo)
    {
        int total = 0;
        foreach (GameObject space in board)
        {
            if (space.gameObject.GetComponent<PropertyScript>() != null && space.gameObject.GetComponent<PropertyScript>().owned && space.gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
            {
                if (space.gameObject.GetComponent<PropertyScript>().houses < 5)
                {
                    total += space.gameObject.GetComponent<PropertyScript>().houses;
                }
            }
        }
        return total;
    }

    public int NumberOfOwnedHotels(int playerNo)
    {
        int total = 0;
        foreach (GameObject space in board)
        {
            if (space.gameObject.GetComponent<PropertyScript>() != null && space.gameObject.GetComponent<PropertyScript>().owned && space.gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
            {
                if (space.gameObject.GetComponent<PropertyScript>().houses == 5)
                {
                    total++;
                }
            }
        }
        return total;
    }

    public static Vector3 SpacePosition(int space)
    {
        return SpacePosition(space, new Vector3(0,0,0));
    }

    public static Vector3 SpacePosition(int space, int playersNum, int i, string jailStatus)
    {
        switch (jailStatus)
        {
            case "In Jail":
                return SpacePosition(space, playersNum, i, new Vector3(-0.25f,0f,0.25f));
            case "Just Visiting":
                return SpacePosition(space, playersNum, i, new Vector3(0.25f,0f,-0.25f));
            default:
                return SpacePosition(space, playersNum, i, new Vector3(0f,0f,0f));
        }
    }

    public static Vector3 SpacePosition(int space, int playersNum, int i, Vector3 modifier)
    {
        if (playersNum > 0)
        {
            return SpacePosition(space, (Quaternion.Euler(0,(360 / playersNum) * i,0) * new Vector3(0.25f,0.25f,0f)) + (SpaceRotation(space) * modifier));
        } else
        {
            return SpacePosition(space, new Vector3(0f,0.25f,0f) + (SpaceRotation(space) * modifier));
        }
    }

    public static Vector3 SpacePosition(int space, Vector3 modifier)
    {
        float disp = (space % 10f) + (0.5f * Convert.ToInt32(space % 10 > 0));
        Vector3 corner;
        Vector3 rowDisplacement;
        switch (Mathf.Floor(space / 10)) {
            case 0f:
                corner = new Vector3(0,0,0);
                rowDisplacement = new Vector3(-disp,0,0);
                break;
            case 1f:
                corner = new Vector3(-11,0,0);
                rowDisplacement = new Vector3(0,0,disp);
                break;
            case 2f:
                corner = new Vector3(-11,0,11);
                rowDisplacement = new Vector3(disp,0,0);
                break;
            case 3f:
                corner = new Vector3(0,0,11);
                rowDisplacement = new Vector3(0,0,-disp);
                break;
            default:
                corner = new Vector3(0,0,0);
                rowDisplacement = new Vector3(-disp,0,0);
                break;
        }
        return GameObject.Find("Board Controller").transform.position + corner + rowDisplacement + modifier;
    }

    public static Quaternion SpaceRotation(int space)
    {
        return Quaternion.Euler(0,Mathf.Floor(space / 10) * 90,0);
    }
}
