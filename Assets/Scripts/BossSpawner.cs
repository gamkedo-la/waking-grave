using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject abomination;
    public GameObject stageWall;
    public GameObject bossHealthBar;
    public GameObject cam;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            abomination.SetActive(true);
            stageWall.SetActive(true);
            bossHealthBar.SetActive(true);
            gameObject.SetActive(false);
            cam.GetComponent<CameraFollow>().enabled = false;
        }
    }
}
