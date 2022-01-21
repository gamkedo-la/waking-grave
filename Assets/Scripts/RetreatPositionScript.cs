using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatPositionScript : MonoBehaviour
{
    [SerializeField] GameObject leftBoundGameObject;
    [SerializeField] GameObject rightBoundGameObject;
    [SerializeField] GameObject lowerBoundGameObject;
    [SerializeField] GameObject upperBoundGameObject;

    private float leftBoundX;
    private float rightBoundX;
    private float upperBoundY;
    private float lowerBoundY;

    // Start is called before the first frame update
    void Start()
    {
        leftBoundX = leftBoundGameObject.transform.position.x;
        rightBoundX = rightBoundGameObject.transform.position.x;
        upperBoundY = upperBoundGameObject.transform.position.y;
        lowerBoundY = lowerBoundGameObject.transform.position.y;
        ResetPosition();
    }

    public void ResetPosition()
    {
        float newX = Random.Range(leftBoundX, rightBoundX);
        float newY = Random.Range(lowerBoundY, upperBoundY);

        Vector2 newPosition = new Vector2(newX, newY);
        gameObject.transform.position = newPosition;
    }
}
