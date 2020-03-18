using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOwners : Photon.MonoBehaviour
{

    public GameObject handled_object;

    // Start is called before the first frame update
    void Start()
    {
        // handled_object.InteractableObjectGrabbed += Toss_InteractableObjectGrabbed();
        StartCoroutine(Pop());
    }

    /*private void Toss_InteractableObjectGrabbed(object sender)
    {
        handled_object.GetCompomnent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
    }*/

    IEnumerator Pop()
    {
        yield return new WaitForSeconds(10f);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
