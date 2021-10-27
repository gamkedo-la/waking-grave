using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleAnimationCurve : MonoBehaviour
{
    public AnimationCurve wobbleUpDown;
    public float upDownSize = 1f;
    public float upDownSpeed = 1f;
    public AnimationCurve wobbleLeftRight;
    public float leftRightSize = 1f;
    public float leftRightSpeed = 1f;

    private float timingOffsetUD;
    private float timingOffsetLR;

    private Vector3 startPos;
   
    void Start () {
        // so that each sprite starts at a different timestamp in the curve
        timingOffsetUD = Random.Range(0f,100f);
        timingOffsetLR = Random.Range(0f,100f);
        startPos = transform.localPosition;
    }
    
    void Update() {
        
        transform.localPosition = startPos + new Vector3(
            leftRightSize * wobbleLeftRight.Evaluate(((Time.time*leftRightSpeed+timingOffsetLR) % wobbleLeftRight.length/2)), 
            upDownSize * wobbleUpDown.Evaluate(((Time.time*upDownSpeed+timingOffsetUD) % wobbleUpDown.length/2)), 
            transform.position.z);

    }

}
