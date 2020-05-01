using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerNo;
    public string name;
    public Color color;
    public int currentSpace;
    int targetSpace;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        money = 1500;
    }

    void FixedUpdate()
    {
        if (targetSpace != currentSpace)
        {
            transform.position = Vector3.MoveTowards(transform.position, BoardControllerScript.SpacePosition(currentSpace + 1, new Vector3(0,0.25f,0)), 0.2f);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, BoardControllerScript.SpacePosition(targetSpace, new Vector3(0,0.25f,0)), 0.2f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Player: " + col.name);
        if(col.transform.parent.name == "Board Controller")
        {
            if(col.gameObject.GetComponent<ActionTileScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<ActionTileScript>().space;
            } else if(col.gameObject.GetComponent<FreeParkingScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<FreeParkingScript>().space;
            } else if(col.gameObject.GetComponent<GoScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<GoScript>().space;
                money += col.gameObject.GetComponent<GoScript>().salary;
            } else if(col.gameObject.GetComponent<GoToJailScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<GoToJailScript>().space;
            } else if(col.gameObject.GetComponent<JailScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<JailScript>().space;
            } else if(col.gameObject.GetComponent<PropertyScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<PropertyScript>().space;
            } else if(col.gameObject.GetComponent<StationScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<StationScript>().space;
            } else if(col.gameObject.GetComponent<UtilityScript>() != null)
            {
                currentSpace = col.gameObject.GetComponent<UtilityScript>().space;
            }
        }
    }

    public void Move(int spaces)
    {
        targetSpace = (currentSpace + spaces) % 40;
    }
}
