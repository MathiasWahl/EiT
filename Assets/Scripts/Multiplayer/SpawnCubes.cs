using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    public GameObject cubePrefab;

    // Update is called once per frame
    void Update()
    {

        
        var lDevice = SteamVR_Controller.Input((int)ViveManager.Instance.leftHand.GetComponent<SteamVR_TrackedObject>().index);
        var rDevice = SteamVR_Controller.Input((int)ViveManager.Instance.rightHand.GetComponent<SteamVR_TrackedObject>().index);
 
        if (lDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) 
        {
            PhotonNetwork.Instantiate(cubePrefab.name, ViveManager.Instance.leftHand.tranform.position, ViveManager.Instance.leftHand.tranform.rotation);
        }

        if (rDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) 
        {
            PhotonNetwork.Instantiate(cubePrefab.name, ViveManager.Instance.rightHand.tranform.position, ViveManager.Instance.rightHand.tranform.rotation);
        }


    }
}
