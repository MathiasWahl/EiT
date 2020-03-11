using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ScalingGrip : MonoBehaviour
{
    public ControllerGrabObject leftHand, rightHand;
    private GameObject scalingObject; // 2
    private float initDistance, currentDistance, p_diff;
    private bool set_distance = true;
    public float scalingVariable = 0.1f, max_scale = 1, min_scale=0.1f;
    public GameObject dot_left, dot_right;
    private Vector3 one = new Vector3(1, 1, 1);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftHand.objectInHand != null && (leftHand.objectInHand == rightHand.objectInHand))
        {
        
            if (set_distance)
            {
                set_distance = false;
                initDistance = Vector3.Distance(dot_left.transform.localPosition, dot_right.transform.localPosition);

            }
            currentDistance = Vector3.Distance(dot_left.transform.localPosition, dot_right.transform.localPosition);
            p_diff = currentDistance - initDistance;
            scalingObject = leftHand.objectInHand;
            if ((scalingObject.transform.localScale.x > min_scale && p_diff < 0) || (scalingObject.transform.localScale.x < max_scale && p_diff > 0)) {
                scalingObject.transform.localScale = p_diff * scalingVariable * one + scalingObject.transform.localScale;

                /*
                Vector3 A = scalingObject.transform.localPosition;
                Vector3 B = leftHand.GetComponent<FixedJoint>().transform.localPosition;
                Vector3 C = A - B; // diff from object pivot to desired pivot/origin
                Vector3 newScale = scalingObject.transform.localScale;

                float RS = newScale.x / scalingObject.transform.localScale.x; // relataive scale factor

                // calc final position post-scale
                Vector3 FP = B + C * RS;

                // finally, actually perform the scale/translation
                scalingObject.transform.localScale = newScale;
                scalingObject.transform.localPosition = FP;

                */

            }

        } else
        {
            set_distance = true;
        }
        
    }
}
