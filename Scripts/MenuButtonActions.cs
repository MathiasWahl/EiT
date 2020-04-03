using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : Photon.MonoBehaviour
{

    // public GameObject spawnObject;
    public Transform cameraRigTransform;
    public Vector3 model_position = new Vector3(590, 2.96f, -18);
    public Vector3 office_position = new Vector3(0, 0, 0);
    public GameObject visibleModel, hiddenModel;
    private bool house_model_showing = true;
    private ControllerGrabObject left, right;

    void Awake()
    {
        // get components
        GameObject leftPointer = GameObject.Find("PR_pointer (left)");
        GameObject rightPointer = GameObject.Find("PR_pointer (right)");
        left = leftPointer.GetComponent<ControllerGrabObject>();
        right = rightPointer.GetComponent<ControllerGrabObject>();

        left.setVisibleModel(visibleModel);
        right.setVisibleModel(visibleModel);
        // set visibleModel
    }

    public void TeleportToModelButton()
    {
        Vector3 teleport_position;
        if (house_model_showing)
        {
            teleport_position = model_position;
        }
        else
        {
            teleport_position = new Vector3(100, 100, 100);
        }
        Debug.Log("Scenes work");
        // get camera rig, change position
        cameraRigTransform.position = teleport_position;
    }

    public void SpawnCubeButton()
    {
        Debug.Log("Spawn cube!");
        PhotonNetwork.Instantiate("Online Cube", new Vector3(0.7f, 1.8f, 2.6f), Quaternion.identity, 0);
        // spawnObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
    }

    public void TeleportToOfficeButton()
    {
        cameraRigTransform.position = office_position;
    }

    public void ChangeModelButton()
    {
        GameObject pre_visible = visibleModel;
        GameObject pre_hidden = hiddenModel;

        // set visibility:
        pre_visible.SetActive(false); // hide visible object

        
        pre_hidden.transform.position = new Vector3(0, 1, 2.4f);
        pre_hidden.transform.rotation = Quaternion.identity;
        pre_hidden.SetActive(true); // make hidden model visible;

        house_model_showing = !house_model_showing;


        hiddenModel = pre_visible;
        visibleModel = pre_hidden;

        left.setVisibleModel(visibleModel);
        right.setVisibleModel(visibleModel);


    }

}
