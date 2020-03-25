using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : Photon.MonoBehaviour
{

    // public GameObject spawnObject;
    public Transform cameraRigTransform;
    public Vector3 model_position = new Vector3(590, 2.96f, -18);
    public Vector3 office_position = new Vector3(0, 0, 0);


    public void TeleportToModelButton()
    {
        Debug.Log("Scenes work");
        // get camera rig, change position
        cameraRigTransform.position = model_position;
    }

    public void SpawnCubeButton()
    {
        Debug.Log("Spawn cube!");
        // PhotonNetwork.Instantiate("Online Cube", new Vector3(0, 0, 0), Quaternion.identity, 0);
        // spawnObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
    }

    public void TeleportToOfficeButton()
    {
        cameraRigTransform.position = office_position;
    }
}
