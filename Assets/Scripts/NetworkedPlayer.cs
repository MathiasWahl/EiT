using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    public GameObject avatar;
    public Transform playerGlobal, playerLocal;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.isMine) {
        Debug.Log("Playerjoined");
        playerGlobal = GameObject.Find("[CameraRig]").transform;
        playerLocal = playerGlobal.Find("Camera");
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
