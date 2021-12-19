using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    public void MoveToBossGraveyard() {
        cam.position = new Vector3 (250.5f, -9, -10);
        cam.GetComponent<CameraFollow>().enabled = false;
        player.position = new Vector3(240, -14.23f, 0);
    }

    public void MoveToStartGraveyard() {
        cam.position = new Vector3 (0.6f, 1.5f, -10);
        cam.GetComponent<CameraFollow>().enabled = true;
        player.position = new Vector3(0.6f, -2.2f, 0);
    }
}
