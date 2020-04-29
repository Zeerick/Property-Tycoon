using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour
{
    public int space;
    public string stationName;
    public Color color;
    public bool owned;
    public int ownerNo;
    public int price;
    public int[] rent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = stationName;
        gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
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
            description = description + "\n\nOwned by:\nPlayer " + ownerNo.ToString();
        } else {
            description = description + "\n\nAvailable";
        }

        gameObject.transform.Find("Information").gameObject.GetComponent<TextMesh>().text = description;
    }
}
