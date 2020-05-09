using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyScript : MonoBehaviour
{
    public int space;
    public string streetName;
    public Color color;
    public bool owned;
    public int ownerNo;
    public int price;
    public int housePrice;
    public int[] rent;
    public int houses;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = streetName;
        gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        string description;

        description = "Rent: £" + rent[0].ToString() +
            "\nWith 1 House: £" + rent[1].ToString() +
            "\nWith 2 House: £" + rent[2].ToString() +
            "\nWith 3 House: £" + rent[3].ToString() +
            "\nWith 5 House: £" + rent[4].ToString() +
            "\nWith HOTEL: £" + rent[5].ToString() +
            "\n\nPrice: £" + price.ToString() +
            "\n\nHouse & Hotels: £" + housePrice.ToString();
        if(owned) {
            description = description + "\n\nOwned by:\n" + gameObject.transform.parent.GetComponent<BoardControllerScript>().PlayerController.gameObject.GetComponent<PlayerControllerScript>().players[ownerNo].GetComponent<PlayerScript>().playerName;
        } else {
            description = description + "\n\nAvailable";
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

        if(houses == 5) {
            gameObject.transform.Find("hotel").gameObject.SetActive(true);
            gameObject.transform.Find("house1").gameObject.SetActive(false);
            gameObject.transform.Find("house2").gameObject.SetActive(false);
            gameObject.transform.Find("house3").gameObject.SetActive(false);
            gameObject.transform.Find("house4").gameObject.SetActive(false);
        } else if(houses == 4) {
            gameObject.transform.Find("hotel").gameObject.SetActive(false);
            gameObject.transform.Find("house1").gameObject.SetActive(true);
            gameObject.transform.Find("house2").gameObject.SetActive(true);
            gameObject.transform.Find("house3").gameObject.SetActive(true);
            gameObject.transform.Find("house4").gameObject.SetActive(true);
        } else if(houses == 3) {
            gameObject.transform.Find("hotel").gameObject.SetActive(false);
            gameObject.transform.Find("house1").gameObject.SetActive(true);
            gameObject.transform.Find("house2").gameObject.SetActive(true);
            gameObject.transform.Find("house3").gameObject.SetActive(true);
            gameObject.transform.Find("house4").gameObject.SetActive(false);
        } else if(houses == 2) {
            gameObject.transform.Find("hotel").gameObject.SetActive(false);
            gameObject.transform.Find("house1").gameObject.SetActive(true);
            gameObject.transform.Find("house2").gameObject.SetActive(true);
            gameObject.transform.Find("house3").gameObject.SetActive(false);
            gameObject.transform.Find("house4").gameObject.SetActive(false);
        } else if(houses == 1) {
            gameObject.transform.Find("hotel").gameObject.SetActive(false);
            gameObject.transform.Find("house1").gameObject.SetActive(true);
            gameObject.transform.Find("house2").gameObject.SetActive(false);
            gameObject.transform.Find("house3").gameObject.SetActive(false);
            gameObject.transform.Find("house4").gameObject.SetActive(false);
        } else {
            gameObject.transform.Find("hotel").gameObject.SetActive(false);
            gameObject.transform.Find("house1").gameObject.SetActive(false);
            gameObject.transform.Find("house2").gameObject.SetActive(false);
            gameObject.transform.Find("house3").gameObject.SetActive(false);
            gameObject.transform.Find("house4").gameObject.SetActive(false);
        }
    }
}
