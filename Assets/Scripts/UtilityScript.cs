using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityScript : MonoBehaviour
{
    public int space;
    public string spaceName;
    public Color color;
    public bool owned;
    public int ownerNo;
    public int price;
    public int[] multiplier;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = spaceName;
        gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.color = color;
        //gameObject.transform.Find("Colour").gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        string description;

        description = "1 Utility: " + multiplier[0].ToString() + "x dice" +
            "\n2 Utilities: " + multiplier[1].ToString() + "x dice" +
            "\n\nPrice: £" + price.ToString();
        if(owned) {
            description = description + "\n\nOwned by:\nPlayer " + ownerNo.ToString();
        } else {
            description = description + "\n\nAvailable";
        }

        gameObject.transform.Find("Information").gameObject.GetComponent<TextMesh>().text = description;
    }
}
