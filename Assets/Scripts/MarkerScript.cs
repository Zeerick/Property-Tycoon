using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour
{
    public GameObject playerController;
    GameObject[] board;

    public int space;
    public int playerNo;
    public bool active = false;
    public bool trade = false;
    public bool gameActive = false;

    void Start()
    {
        transform.Find("Manage Properties UI").gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameActive)
        {
            if (!active)
            {
                playerNo = playerController.gameObject.GetComponent<PlayerControllerScript>().currentPlayer;
                space = playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().currentSpace;
            }
            gameObject.transform.position = BoardControllerScript.SpacePosition(space);
            gameObject.transform.rotation = BoardControllerScript.SpaceRotation(space);
            if (board[space].gameObject.GetComponent<PropertyScript>() != null)
            {
                if (!trade)
                {
                    PropertyManage();
                }
            } else if (board[space].gameObject.GetComponent<StationScript>() != null)
            {
                if (!trade)
                {
                    StationManage();
                }
            } else if (board[space].gameObject.GetComponent<UtilityScript>() != null)
            {
                if (!trade)
                {
                    UtilityManage();
                }
            }
        }
        transform.Find("Outline").gameObject.SetActive(active);
    }

    void PropertyManage()
    {
        if (!board[space].gameObject.GetComponent<PropertyScript>().mortgaged)
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Mortgage for £" + ((int)(board[space].gameObject.GetComponent<PropertyScript>().price / 2)).ToString();
            transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Buy House for £" + board[space].gameObject.GetComponent<PropertyScript>().housePrice.ToString();
            transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Sell House for £" + ((int)(board[space].gameObject.GetComponent<PropertyScript>().housePrice / 2)).ToString();
            if (board[space].gameObject.GetComponent<PropertyScript>().IsSetComplete())
            {
                if (board[space].gameObject.GetComponent<PropertyScript>().houses < 5 &&
                board[space].gameObject.GetComponent<PropertyScript>().houses <= board[space].gameObject.GetComponent<PropertyScript>().LeastHousesInSet() &&
                board[space].gameObject.GetComponent<PropertyScript>().housePrice <= playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
                {
                    transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
                } else
                {
                    transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
                }
                if (board[space].gameObject.GetComponent<PropertyScript>().houses > 0 &&
                board[space].gameObject.GetComponent<PropertyScript>().houses >= board[space].gameObject.GetComponent<PropertyScript>().MostHousesInSet())
                {
                    transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0);
                } else
                {
                    transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
                }
            } else
            {
                transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
                transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            }
        } else
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Unmortgage for £" + ((int)((board[space].gameObject.GetComponent<PropertyScript>().price / 2) * 1.1)).ToString();
            if ((int)((board[space].gameObject.GetComponent<PropertyScript>().price / 2) * 1.1) >= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money || board[space].gameObject.GetComponent<PropertyScript>().houses > 0)
            {
                transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            }
            transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Buy House";
            transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Sell House";
            transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        }
    }

    void StationManage()
    {
        if (!board[space].gameObject.GetComponent<StationScript>().mortgaged)
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Mortgage for £" + ((int)(board[space].gameObject.GetComponent<StationScript>().price / 2)).ToString();
        } else
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Unmortgage for £" + ((int)((board[space].gameObject.GetComponent<StationScript>().price / 2) * 1.1)).ToString();
            if ((int)((board[space].gameObject.GetComponent<StationScript>().price / 2) * 1.1) >= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
            {
                transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            }
        }
        transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Buy House";
        transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Sell House";
        transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
    }

    void UtilityManage()
    {
        if (!board[space].gameObject.GetComponent<UtilityScript>().mortgaged)
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Mortgage for £" + ((int)(board[space].gameObject.GetComponent<UtilityScript>().price / 2)).ToString();
        } else
        {
            transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Unmortgage for £" + ((int)((board[space].gameObject.GetComponent<UtilityScript>().price / 2) * 1.1)).ToString();
            if ((int)((board[space].gameObject.GetComponent<UtilityScript>().price / 2) * 1.1) >= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
            {
                transform.Find("Manage Properties UI").Find("Mortgage").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
            }
        }
        transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Buy House";
        transform.Find("Manage Properties UI").Find("Buy House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
        transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Sell House";
        transform.Find("Manage Properties UI").Find("Sell House").Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().color = new Color(1,0,0);
    }

    public void SetActive(bool a)
    {
        active = a;
    }

    public void StartGame()
    {
        gameActive = true;
        board = gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().board;
    }

    public void NextOwnedSpace()
    {
        board = gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().board;
        bool check = false;
        do {
            space = (space + 1) % board.Length;
            if (board[space].gameObject.GetComponent<PropertyScript>() != null && board[space].gameObject.GetComponent<PropertyScript>().owned && board[space].gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
            {
                check = true;
            } else if (board[space].gameObject.GetComponent<StationScript>() != null && board[space].gameObject.GetComponent<StationScript>().owned && board[space].gameObject.GetComponent<StationScript>().ownerNo == playerNo)
            {
                check = true;
            } else if (board[space].gameObject.GetComponent<UtilityScript>() != null && board[space].gameObject.GetComponent<UtilityScript>().owned && board[space].gameObject.GetComponent<UtilityScript>().ownerNo == playerNo)
            {
                check = true;
            }
        } while (!check);
    }

    public void PrevOwnedSpace()
    {
        board = gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().boardController.gameObject.GetComponent<BoardControllerScript>().board;
        bool check = false;
        do {
            space = (space - 1 + board.Length) % board.Length;
            if (board[space].gameObject.GetComponent<PropertyScript>() != null && board[space].gameObject.GetComponent<PropertyScript>().owned && board[space].gameObject.GetComponent<PropertyScript>().ownerNo == playerNo)
            {
                check = true;
            } else if (board[space].gameObject.GetComponent<StationScript>() != null && board[space].gameObject.GetComponent<StationScript>().owned && board[space].gameObject.GetComponent<StationScript>().ownerNo == playerNo)
            {
                check = true;
            } else if (board[space].gameObject.GetComponent<UtilityScript>() != null && board[space].gameObject.GetComponent<UtilityScript>().owned && board[space].gameObject.GetComponent<UtilityScript>().ownerNo == playerNo)
            {
                check = true;
            }
        } while (!check);
    }

    public void Mortgage()
    {
        if (board[space].gameObject.GetComponent<PropertyScript>() != null)
        {
            if (!board[space].gameObject.GetComponent<PropertyScript>().mortgaged)
            {
                board[space].gameObject.GetComponent<PropertyScript>().mortgaged = true;
                playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Income(board[space].gameObject.GetComponent<PropertyScript>().price / 2);
            } else
            {
                if ((int)((board[space].gameObject.GetComponent<PropertyScript>().price / 2) * 1.1) <= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money && board[space].gameObject.GetComponent<PropertyScript>().houses == 0)
                {
                    board[space].gameObject.GetComponent<PropertyScript>().mortgaged = false;
                    playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Pay((int)((board[space].gameObject.GetComponent<PropertyScript>().price / 2) * 1.1));
                }
            }
        } else if (board[space].gameObject.GetComponent<StationScript>() != null)
        {
            if (!board[space].gameObject.GetComponent<StationScript>().mortgaged)
            {
                board[space].gameObject.GetComponent<StationScript>().mortgaged = true;
                playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Income(board[space].gameObject.GetComponent<StationScript>().price / 2);
            } else
            {
                if ((int)((board[space].gameObject.GetComponent<StationScript>().price / 2) * 1.1) <= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
                {
                    board[space].gameObject.GetComponent<StationScript>().mortgaged = false;
                    playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Pay((int)((board[space].gameObject.GetComponent<StationScript>().price / 2) * 1.1));
                }
            }
        } else if (board[space].gameObject.GetComponent<UtilityScript>() != null)
        {
            if (!board[space].gameObject.GetComponent<UtilityScript>().mortgaged)
            {
                board[space].gameObject.GetComponent<UtilityScript>().mortgaged = true;
                playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Income(board[space].gameObject.GetComponent<UtilityScript>().price / 2);
            } else
            {
                if ((int)((board[space].gameObject.GetComponent<UtilityScript>().price / 2) * 1.1) <= gameObject.transform.parent.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
                {
                    board[space].gameObject.GetComponent<UtilityScript>().mortgaged = false;
                    playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Pay((int)((board[space].gameObject.GetComponent<UtilityScript>().price / 2) * 1.1));
                }
            }
        }
    }

    public void BuyHouse()
    {
        if (board[space].gameObject.GetComponent<PropertyScript>() != null &&
            !board[space].gameObject.GetComponent<PropertyScript>().mortgaged &&
            board[space].gameObject.GetComponent<PropertyScript>().IsSetComplete() &&
            board[space].gameObject.GetComponent<PropertyScript>().houses < 5 &&
            board[space].gameObject.GetComponent<PropertyScript>().houses <= board[space].gameObject.GetComponent<PropertyScript>().LeastHousesInSet() &&
            board[space].gameObject.GetComponent<PropertyScript>().housePrice <= playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().money)
        {
            playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Pay(board[space].gameObject.GetComponent<PropertyScript>().housePrice);
            board[space].gameObject.GetComponent<PropertyScript>().houses += 1;
        }
    }

    public void SellHouse()
    {
        if (board[space].gameObject.GetComponent<PropertyScript>() != null &&
            !board[space].gameObject.GetComponent<PropertyScript>().mortgaged &&
            board[space].gameObject.GetComponent<PropertyScript>().IsSetComplete() &&
            board[space].gameObject.GetComponent<PropertyScript>().houses > 0 &&
            board[space].gameObject.GetComponent<PropertyScript>().houses >= board[space].gameObject.GetComponent<PropertyScript>().MostHousesInSet())
        {
            playerController.gameObject.GetComponent<PlayerControllerScript>().players[playerNo].gameObject.GetComponent<PlayerScript>().Income(board[space].gameObject.GetComponent<PropertyScript>().housePrice / 2);
            board[space].gameObject.GetComponent<PropertyScript>().houses -= 1;
        }
    }
}
