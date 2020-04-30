using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditionSelectScript : MonoBehaviour
{
    Object[] csvFiles;
    string[] editionNames;
    string[] csvStrings;
    int currentEditionNo;

    // Start is called before the first frame update
    void Start()
    {
        csvFiles = Resources.LoadAll("Data/Board", typeof(TextAsset));
        editionNames = new string[csvFiles.Length];
        csvStrings = new string[csvFiles.Length];
        currentEditionNo = 0;
        foreach (TextAsset csv in csvFiles) {
            StreamReader reader = new StreamReader("Assets/Resources/Data/Board/" + csv.name + ".csv");
            string txt = reader.ReadToEnd();
            reader.Close();
            csvStrings[currentEditionNo] = txt;
            string[] lines = txt.Split('\n');
            string[] cells = lines[0].Split(',');
            editionNames[currentEditionNo] = cells[0];
            currentEditionNo += 1;
        }
        currentEditionNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = editionNames[currentEditionNo];
    }

    public void NextEdition()
    {
        if (currentEditionNo < csvFiles.Length - 1) {
            currentEditionNo += 1;
        }
    }

    public void PrevEdition()
    {
        if (currentEditionNo > 0) {
            currentEditionNo -= 1;
        }
    }
}
