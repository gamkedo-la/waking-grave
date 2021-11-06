using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudioScript : MonoBehaviour
{
    [SerializeField] AudioClip zombieAlertSFX1;
    [SerializeField] AudioClip zombieAlertSFX2;
    [SerializeField] AudioClip zombieAlertSFX3;
    private List<AudioClip> listOfZombieAlertSFX = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        listOfZombieAlertSFX.Add(zombieAlertSFX1);
        listOfZombieAlertSFX.Add(zombieAlertSFX2);
        listOfZombieAlertSFX.Add(zombieAlertSFX3);
    }

    public void PlayRandomZombieAlertSFX()
    {
        int randomIndex = Random.Range(0, listOfZombieAlertSFX.Count - 1);
        GetComponent<AudioSource>().clip = listOfZombieAlertSFX[randomIndex];
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
    }
}
