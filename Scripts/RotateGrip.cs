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
    public SteamVR_Action_Boolean pushAction, pullAction;
    private Vector3 rotateChange = new Vector3(0, 3, 0);

    // Update is called once per frame
    void Update()
    {

        if (leftHand.objectInHand != null)
        {
            //rotate
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

            //push
            if (pushAction.GetState(leftHandType))
            {
                PushObject(rotateObject);
            }
            //pull
            else if (pullAction.GetState(leftHandType))
            {
                PullObject(rotateObject);
            }


        }
        
        if (rightHand.objectInHand != null)
        {
            //rotate
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


            //push
            if (pushAction.GetState(rightHandType))
            {
                PushObject(rotateObject);
            }
            //pull
            else if (pullAction.GetState(rightHandType))
            {
                PullObject(rotateObject);
            }
        }

    }

    void PushObject(GameObject go)
    {
        go.GetComponent<FixedJoint>().connectedBody.transform.localScale += new Vector3(0.0f, 0.0f, 1.0f); // ?? should work
    }


    void PullObject(GameObject go)
    {
        go.GetComponent<FixedJoint>().connectedBody.transform.localScale += new Vector3(0.0f, 0.0f, -1.0f);
    }
}
