using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public bool active = false;
    public GameObject playerController;
    GameObject topCam;
    GameObject sideCam;
    int boardEdge;
    Vector3 sidePos;
    Quaternion sideRot;

    // Start is called before the first frame update
    void Start()
    {
        topCam = gameObject.transform.Find("Top Camera").gameObject;
        sideCam = gameObject.transform.Find("Side Camera").gameObject;
        topCam.SetActive(true);
        sideCam.SetActive(false);
        sidePos = sideCam.transform.position;
        sideRot = sideCam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                topCam.SetActive(!topCam.activeSelf);
                sideCam.SetActive(!sideCam.activeSelf);
            }
            boardEdge = (int)Mathf.Floor(playerController.GetComponent<PlayerControllerScript>().players[playerController.GetComponent<PlayerControllerScript>().currentPlayer].gameObject.GetComponent<PlayerScript>().currentSpace / 10);
            sideCam.transform.position = Vector3.MoveTowards(sideCam.transform.position, Quaternion.Euler(0, 90 * boardEdge, 0) * sidePos, Mathf.Max(Vector3.Distance(sideCam.transform.position, Quaternion.Euler(0, 90 * boardEdge, 0) * sidePos) / 15, 0.1f));
            sideCam.transform.rotation = Quaternion.RotateTowards(sideCam.transform.rotation, Quaternion.Euler(sideRot.eulerAngles.x, 90 * boardEdge, sideRot.eulerAngles.z), Mathf.Max(Mathf.Abs(((90 * boardEdge) - sideCam.transform.rotation.eulerAngles.y) / 15), 0.5f));
        }
    }

    public void Activate()
    {
        active = true;
    }
}
