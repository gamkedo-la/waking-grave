using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{

    [Header("AI Settings")]
    [SerializeField] private Vector2[] positions;
    [SerializeField] private Transform playerPosition;     // Will only use if corrupted
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isCorrupted;
    private int posIndex;

    // Start is called before the first frame update
    void Start()
    {
        if(isCorrupted) {
            positions = new Vector2[6];
        } else {
            positions = new Vector2[2];
        }
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(0).GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[posIndex], movementSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, positions[posIndex]) < 0.3f) {
            posIndex++;
            if(posIndex >= positions.Length) {
                posIndex = 0;
            }
        }
    }
}
