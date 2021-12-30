using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 followOffset;
    public Vector2 threshold;
    public float speed;
    private Rigidbody2D targetRb2d;
    public float xBoundaryLeft;

    private void Awake() {
        if(CheckpointManager.instance != null && CheckpointManager.instance.lastCheckpointPos != Vector2.zero ) {
            transform.position = new Vector3( CheckpointManager.instance.lastCheckpointPos.x , CheckpointManager.instance.lastCheckpointPos.y, -10f);
        }
    }

    private void Start()
    {
        threshold = CalculateThreshold();
        targetRb2d = target.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float xDifference = Vector2.Distance(Vector2.right *  transform.position.x, Vector2.right * target.position.x);
        float yDifference = Vector2.Distance(Vector2.right *  transform.position.y, Vector2.right * target.position.y);

        Vector3 newPos = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x && target.position.x > xBoundaryLeft) { // dont follow player to the left edge
            newPos.x = target.position.x;
        }

        if(Mathf.Abs(yDifference) >= threshold.y) {
            newPos.y = target.position.y;
        }

        float moveSpeed = targetRb2d.velocity.magnitude > speed ? targetRb2d.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime) ;

    }

    private Vector3 CalculateThreshold() {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3( border.x *2, border.y * 2, 1) );
    }
}
