using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailScript : MonoBehaviour
{
    public int space;
    public string spaceName;

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
