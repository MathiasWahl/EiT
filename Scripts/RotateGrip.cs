using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateGrip : Photon.MonoBehaviour
{
    public ControllerGrabObject leftHand, rightHand;
    public SteamVR_Input_Sources leftHandType;
    public SteamVR_Input_Sources rightHandType;
    private GameObject rotateObject;
    public SteamVR_Action_Boolean rotateActionLeft, rotateActionRight;
    private Vector3 rotateChange = new Vector3(0, 3, 0);

    // Update is called once per frame
    void Update()
    {

        if (leftHand.objectInHand != null)
        {
            rotateObject = leftHand.objectInHand;

            if (rotateActionLeft.GetState(leftHandType))
            {
                leftHand.ReleaseObject();
                rotateObject.transform.Rotate(-rotateChange);
                leftHand.GrabObject();
            }
            else if (rotateActionRight.GetState(leftHandType))
            {
                leftHand.ReleaseObject();
                rotateObject.transform.Rotate(rotateChange);
                leftHand.GrabObject();
            }

        }
        
        if (rightHand.objectInHand != null)
        {
            rotateObject = rightHand.objectInHand;

            if (rotateActionLeft.GetState(rightHandType))
            {
                rightHand.ReleaseObject();
                rotateObject.transform.Rotate(-rotateChange);
                rightHand.GrabObject();
            }
            else if (rotateActionRight.GetState(rightHandType))
            {
                rightHand.ReleaseObject();
                rotateObject.transform.Rotate(rotateChange);
                rightHand.GrabObject();
            }

        }
    }
}
