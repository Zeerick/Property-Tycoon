using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToJailScript : MonoBehaviour
{
    public int space;
    public string spaceName;
    public int targetSpace;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Name").gameObject.GetComponent<TextMesh>().text = spaceName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
