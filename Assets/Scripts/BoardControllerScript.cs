using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControllerScript : MonoBehaviour
{
    public GameObject Action;
    public GameObject FreeParking;
    public GameObject Go;
    public GameObject GoToJail;
    public GameObject Jail;
    public GameObject Property;
    public GameObject Station;
    public GameObject Utility;

    string[] name = new string[40];
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
            name[row] = line[1];
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
                    CreateFreeParking(space, name[space]);
                    break;
                case "Go":
                    CreateGo(space, name[space], amount[space]);
                    break;
                case "Go To Jail":
                    CreateGoToJail(space, name[space]);
                    break;
                case "Jail":
                    CreateJail(space, name[space]);
                    break;
                case "Property":
                    CreateProperty(space, name[space], color[space], price[space], housePrice[space], rent[space]);
                    break;
                case "Station":
                    break;
                case "Take Card":
                    break;
                case "Tax":
                    break;
                case "Utility":
                    break;
            }
        }
    }

    void CreateAction()
    {

    }

    void CreateFreeParking(int space, string spaceName)
    {
        GameObject instance = Instantiate(FreeParking, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<FreeParkingScript>().space = space;
        instance.GetComponent<FreeParkingScript>().spaceName = spaceName;
    }

    void CreateGo(int space, string spaceName, int salary)
    {
        GameObject instance = Instantiate(Go, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<GoScript>().space = space;
        instance.GetComponent<GoScript>().spaceName = spaceName;
        instance.GetComponent<GoScript>().salary = salary;
    }

    void CreateGoToJail(int space, string spaceName)
    {
        GameObject instance = Instantiate(GoToJail, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<GoToJailScript>().space = space;
        instance.GetComponent<GoToJailScript>().spaceName = spaceName;
        instance.GetComponent<GoToJailScript>().targetSpace = Array.IndexOf(type, "Jail");
    }

    void CreateJail(int space, string spaceName)
    {
        GameObject instance = Instantiate(Jail, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<JailScript>().space = space;
        instance.GetComponent<JailScript>().spaceName = spaceName;
    }

    void CreateProperty(int space, string spaceName, Color spaceColor, int spacePrice, int spaceHousePrice, int[] spaceRent)
    {
        GameObject instance = Instantiate(Property, SpacePosition(space), SpaceRotation(space), gameObject.transform);
        instance.GetComponent<PropertyScript>().space = space;
        instance.GetComponent<PropertyScript>().streetName = spaceName;
        instance.GetComponent<PropertyScript>().color = spaceColor;
        instance.GetComponent<PropertyScript>().price = spacePrice;
        instance.GetComponent<PropertyScript>().housePrice = spaceHousePrice;
        instance.GetComponent<PropertyScript>().rent = spaceRent;
    }

    void CreateStation()
    {

    }

    void CreateUtility()
    {

    }

    Vector3 SpacePosition(int space)
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
        return gameObject.transform.position + corner + rowDisplacement;
    }

    Quaternion SpaceRotation(int space)
    {
        return Quaternion.Euler(0,Mathf.Floor(space / 10) * 90,0);
    }
}
