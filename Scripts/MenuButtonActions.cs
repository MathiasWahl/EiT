using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : Photon.MonoBehaviour
{

    // public GameObject spawnObject;
    public Transform cameraRigTransform;
    public Vector3 model_position = new Vector3(590, 2.96f, -18);
    public Vector3 office_position = new Vector3(0, 0, 0);
    private Vector3 office_model_position = new Vector3(-0.164f, 1.044f, 5.529f);
    private Vector3 homemade_model_position = new Vector3(-1004, 4.2f, 11);
    public static GameObject visibleModel, hiddenModel;
    public GameObject set_visible, set_hidden;
    private bool house_model_showing = true;
    private ControllerGrabObject left, right;
    private GameObject toy_model;


    void Awake()
    {
        // get components
        GameObject leftPointer = GameObject.Find("PR_pointer (left)");
        GameObject rightPointer = GameObject.Find("PR_pointer (right)");
        left = leftPointer.GetComponent<ControllerGrabObject>();
        right = rightPointer.GetComponent<ControllerGrabObject>();
        visibleModel = set_visible;
        hiddenModel = set_hidden;

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
            teleport_position = homemade_model_position;
        }
        Debug.Log("Scenes work");
        // get camera rig, change position
        cameraRigTransform.position = teleport_position;
    }

    public void SpawnToyModelButton()
    {
        if (toy_model != null)
        {
            PhotonNetwork.Destroy(toy_model);
        }
        toy_model = PhotonNetwork.Instantiate(visibleModel.name, new Vector3(-7.79f, 1.4f, 3.09f), Quaternion.identity, 0);
        toy_model.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        toy_model.SetActive(true);
        Rigidbody rigid_body = toy_model.GetComponent<Rigidbody>();
        rigid_body.useGravity = true;
        rigid_body.constraints = RigidbodyConstraints.None;

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

        // transfer ownership:
        pre_visible.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        pre_hidden.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);


        // hide visible object
        pre_visible.SetActive(false);


        pre_hidden.transform.position = office_model_position;
        pre_hidden.transform.rotation = Quaternion.identity;
        pre_hidden.SetActive(true); // make hidden model visible;

        house_model_showing = !house_model_showing;

        hiddenModel = pre_visible;
        visibleModel = pre_hidden;


    }

}
