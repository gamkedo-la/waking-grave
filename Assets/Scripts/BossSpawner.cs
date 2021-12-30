using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject abomination;
    public GameObject stageWall;
    public GameObject bossHealthBar;
    public GameObject cam;
    public float camSpeed;
    private bool moveCam;
    private Vector3 camTarget = new Vector3 (250.5f, -9, -10);

    private void FixedUpdate() {
        if(moveCam) {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camTarget , camSpeed * Time.deltaTime);
            if(Vector3.Distance(cam.transform.position, camTarget) == 0) {
                EnableBoss();
                moveCam = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            stageWall.SetActive(true);
            cam.GetComponent<CameraFollow>().enabled = false;
            moveCam = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void EnableBoss () {
        bossHealthBar.SetActive(true);
        abomination.SetActive(true);

    }
}
