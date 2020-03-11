using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : MonoBehaviour
{

    public GameObject spawnObject;
 
    public void ScenesButton()
    {
        Debug.Log("Scenes work");
    }

    public void ObjectsButton()
    {
        Instantiate(spawnObject);
    }
}
