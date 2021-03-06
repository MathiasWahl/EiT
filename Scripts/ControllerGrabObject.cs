﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : Photon.MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    private GameObject collidingObject; // 1
    public GameObject objectInHand; // 2

    public SteamVR_Action_Boolean rotateActionLeft, rotateActionRight, scaleActionNorth, scaleActionSouth;
    // public GameObject visibleModel;
    private Vector3 rotateChange = new Vector3(0, 2, 0);
    public float scaleChange = 1.05f;

    public static bool grab = false;

    private void SetCollidingObject(Collider col)
    {
        //objectInHand.transform.localScale *= new Vector3(x, y, z);
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    public void GrabObject()
    {
        if (collidingObject != MenuButtonActions.visibleModel)
        {
            // 1
            objectInHand = collidingObject;
            objectInHand.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
            objectInHand.GetComponent<Rigidbody>().useGravity = false;
            collidingObject = null;
            // 2
            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        }
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().useGravity = true;
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        // 4
        objectInHand = null;
    }


    // Update is called once per frame
    void Update()
    {

        if (collidingObject == MenuButtonActions.visibleModel)
        {
            if (rotateActionLeft.GetState(handType))
            {
                MenuButtonActions.visibleModel.transform.Rotate(-rotateChange);
            }
            else if (rotateActionRight.GetState(handType))
            {
                MenuButtonActions.visibleModel.transform.Rotate(rotateChange);
            }
            else if (scaleActionNorth.GetState(handType) && MenuButtonActions.visibleModel.transform.localScale[0] < 1)
            {
                MenuButtonActions.visibleModel.transform.localScale = MenuButtonActions.visibleModel.transform.localScale * scaleChange;
            }
            else if (scaleActionSouth.GetState(handType) && MenuButtonActions.visibleModel.transform.localScale[0] > 0.1f)
            {
                MenuButtonActions.visibleModel.transform.localScale = MenuButtonActions.visibleModel.transform.localScale / scaleChange;
            }
        }

        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                grab = true;
                GrabObject();
                //objectInHand.transform.localScale = objectInHand.transform.localScale * 2;
            }
        }

        // 2
        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                
                //objectInHand.transform.localScale = objectInHand.transform.localScale / 2;
                ReleaseObject();
                grab = false;
            }
        }

    }
}
