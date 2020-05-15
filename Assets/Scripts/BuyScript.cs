using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    GameObject property;
    int price;

    void Update()
    {
        if (price > gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        } else
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
        }
    }

    public void Setup(GameObject p)
    {
        property = p;
        if(property.gameObject.GetComponent<PropertyScript>() != null)
        {
            price = property.gameObject.GetComponent<PropertyScript>().price;
        } else if(property.gameObject.GetComponent<StationScript>() != null)
        {
            price = property.gameObject.GetComponent<StationScript>().price;
        } else if(property.gameObject.GetComponent<UtilityScript>() != null)
        {
            price = property.gameObject.GetComponent<UtilityScript>().price;
        }
    }

    public void Buy()
    {
        if (price <= gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerScript>().money)
        {
            gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerScript>().Pay(price);
            gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerScript>().AcquireProperty(property);
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.parent.parent.gameObject.GetComponent<PlayerScript>().MoveDone();
        }
    }
}
