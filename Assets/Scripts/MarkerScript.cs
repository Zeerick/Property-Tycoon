using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour
{
    public GameObject playerController;

    public int space;
    public bool active;

    void Update()
    {
        if (!active)
        {
            space = playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerController.gameObject.GetComponent<PlayerControllerScript>().currentPlayer].currentSpace;
        }
        gameObject.transform.position = BoardControllerScript.SpacePosition(space, gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players.Length, playerNo, jailStatus);
    }

    void NextOwnedSpace()
    {
        GameObject[] board = gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().board;
    }

    void PrevOwnedSpace()
    {

    }
}
