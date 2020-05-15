using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour
{
    public int space;
    public string stationName;
    public Color color;
    public bool owned;
    public bool mortgaged = false;
    public int ownerNo;
    public int price;
    public int[] rent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = stationName;
        gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        string description;

        description = "1 Station: £" + rent[0].ToString() +
            "\n2 Stations: £" + rent[1].ToString() +
            "\n3 Stations: £" + rent[2].ToString() +
            "\n4 Stations: £" + rent[3].ToString() +
            "\n\nPrice: £" + price.ToString();
        if(owned) {
            description = description + "\n\nOwned by:\n" + gameObject.transform.parent.GetComponent<BoardControllerScript>().PlayerController.gameObject.GetComponent<PlayerControllerScript>().players[ownerNo].GetComponent<PlayerScript>().playerName;
        } else {
            description = description + "\n\nAvailable";
        }
        if (mortgaged)
        {
            description = description + "\n\nMORTGAGED";
        }

        gameObject.transform.Find("Information").gameObject.GetComponent<TextMesh>().text = description;
        if (owned)
        {
            gameObject.transform.Find("Owner").gameObject.SetActive(true);
            gameObject.transform.Find("Owner").gameObject.GetComponent<Renderer>().material.color = gameObject.transform.parent.GetComponent<BoardControllerScript>().PlayerController.gameObject.GetComponent<PlayerControllerScript>().players[ownerNo].GetComponent<PlayerScript>().color;
        } else
        {
            gameObject.transform.Find("Owner").gameObject.SetActive(false);
        }
    }

    public int StationsOwned()
    {
        int stations = 0;
        foreach (GameObject space in gameObject.transform.parent.gameObject.GetComponent<BoardControllerScript>().board)
        {
            if (space.gameObject.GetComponent<StationScript>() != null)
            {
                if (space.gameObject.GetComponent<StationScript>().ownerNo == ownerNo && space.gameObject.GetComponent<StationScript>().owned)
                {
                    stations += 1;
                }
            }
        }
        return stations;
    }
}
