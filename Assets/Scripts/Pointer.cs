using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    public LayerMask teleportMask;

    private LineRenderer m_LineRenderer = null;

    //teleport vars
    public Transform cameraRigTransform;
    public Transform headTransform;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Input_Sources handType;


    // Start is called before the first frame update
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {

        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;
        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
            if (Math.Pow(2, hit.collider.gameObject.layer) == teleportMask.value && teleportAction.GetStateUp(handType))
            {
                Teleport(hit.point);
            }
        
        }


        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }

    private void Teleport(Vector3 hitPoint)
    {
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5
        cameraRigTransform.position = hitPoint + difference;
    }
}
