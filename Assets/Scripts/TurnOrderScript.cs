using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderScript : MonoBehaviour
{
    public GameObject player;
    public GameObject controller;

    void Start()
    {
        gameObject.transform.Find("Change Name").gameObject.SetActive(false);
    }

    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerScript>().playerName + " £" + player.GetComponent<PlayerScript>().money.ToString();
        gameObject.GetComponent<UnityEngine.UI.Text>().color = player.GetComponent<PlayerScript>().color;
        gameObject.transform.localPosition = new Vector3(0,80 - (((player.GetComponent<PlayerScript>().playerNo + controller.GetComponent<PlayerControllerScript>().players.Length - controller.GetComponent<PlayerControllerScript>().currentPlayer) % controller.GetComponent<PlayerControllerScript>().players.Length) * 30), 0);
    }

    public void ChangeNameSubmit()
    {
        player.GetComponent<PlayerScript>().playerName = gameObject.transform.Find("Change Name").gameObject.transform.Find("InputField").gameObject.GetComponent<InputField>().text;
    }
}
