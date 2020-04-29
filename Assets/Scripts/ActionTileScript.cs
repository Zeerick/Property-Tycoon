using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTileScript : MonoBehaviour
{
    public int space;
    public string spaceName;
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = spaceName;
        gameObject.transform.Find("Information").gameObject.GetComponent<TextMesh>().text = description;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
