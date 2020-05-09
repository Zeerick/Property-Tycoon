using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeParkingScript : MonoBehaviour
{
    public int space;
    public string spaceName;
    public int pot;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = spaceName;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Find("Pot").gameObject.GetComponent<TextMesh>().text = "£" + pot.ToString();
    }

    public int TakePot()
    {
        int amount = pot;
        pot = 0;
        return amount;
    }
}
