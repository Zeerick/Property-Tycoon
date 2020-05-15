using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class EditionSelectScript : MonoBehaviour
{
    Object[] csvBoardFiles;
    Object[] csvCardFiles;
    string[] editionNames;
    string[] csvBoardStrings;
    string[] csvCardStrings;
    int currentEditionNo;

    [System.Serializable]
    public class SubmitEvent : UnityEvent<string> {}

    public SubmitEvent StartGameBoard;
    public SubmitEvent StartGameCards;

    // Start is called before the first frame update
    void Start()
    {
        csvBoardFiles = Resources.LoadAll("Data/Board", typeof(TextAsset));
        csvCardFiles = Resources.LoadAll("Data/Cards", typeof(TextAsset));
        editionNames = new string[csvBoardFiles.Length];
        csvBoardStrings = new string[csvBoardFiles.Length];
        currentEditionNo = 0;
        foreach (TextAsset csvBoard in csvBoardFiles) {
            StreamReader reader = new StreamReader("Assets/Resources/Data/Board/" + csvBoard.name + ".csv");
            string txt = reader.ReadToEnd();
            reader.Close();
            csvBoardStrings[currentEditionNo] = txt;
            string[] lines = txt.Split('\n');
            string[] cells = lines[0].Split(',');
            editionNames[currentEditionNo] = cells[0];
            currentEditionNo += 1;
        }
        csvCardStrings = new string[csvCardFiles.Length];
        currentEditionNo = 0;
        foreach (TextAsset csvCards in csvCardFiles) {
            StreamReader reader = new StreamReader("Assets/Resources/Data/Cards/" + csvCards.name + ".csv");
            csvCardStrings[currentEditionNo] = reader.ReadToEnd();
            reader.Close();
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
        if (currentEditionNo < csvBoardFiles.Length - 1) {
            currentEditionNo += 1;
        }
    }

    public void PrevEdition()
    {
        if (currentEditionNo > 0) {
            currentEditionNo -= 1;
        }
    }

    public void Submit()
    {
        StartGameBoard.Invoke(csvBoardStrings[currentEditionNo]);
        StartGameCards.Invoke(csvCardStrings[currentEditionNo]);
    }
}
