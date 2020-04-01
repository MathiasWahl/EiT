/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateGrip : MonoBehaviour
{
    public ControllerGrabObject leftHand, rightHand;
    public SteamVR_Input_Sources leftHandType;
    public SteamVR_Input_Sources rightHandType;
    private GameObject rotateObject;
    public SteamVR_Action_Boolean rotateAction;
    private Vector3 rotateChange = new Vector3(0.1f, 0.1f, 0.1f);

    // Update is called once per frame
    void Update()
    {
        if (leftHand.objectInHand != null && (leftHand.objectInHand == rightHand.objectInHand))
        {
            rotateObject = leftHand.objectInHand;

            if (rotateAction.GetLastStateDown(leftHandType))
            {
                rotateObject.transform.Rotate += rotateChange;
            }
            else if (rotateAction.GetLastStateDown(leftHandType))
            {
                rotateObject.transform.Rotate -= rotateChange;
            }

        }
    }
}*/
