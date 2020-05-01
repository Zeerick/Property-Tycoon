using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISelectorScript : MonoBehaviour
{
    public int maxAIs;
    public int minAIs;
    int noOfAIs;

    // Start is called before the first frame update
    void Start()
    {
        noOfAIs = minAIs;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = noOfAIs.ToString();
    }

    public void Increase()
    {
        if (noOfAIs < maxAIs) {
            noOfAIs += 1;
        }
    }

    public void Decrease()
    {
        if (noOfAIs > minAIs) {
            noOfAIs -= 1;
        }
    }
}
