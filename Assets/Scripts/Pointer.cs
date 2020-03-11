using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class Pointer : MonoBehaviour
{
    private float m_DefaultLength = 10.0f;
    private float m_CurrentLength;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    public LayerMask teleportMask;
    public LayerMask dotMask;

    private LineRenderer m_LineRenderer = null;

    //teleport vars
    public Transform cameraRigTransform;
    public Transform headTransform;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Input_Sources handType;
    private BoxCollider laserColider;
    private float zLength;


    // Start is called before the first frame update
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();

        m_CurrentLength = m_DefaultLength;

        laserColider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {


        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_CurrentLength : data.pointerCurrentRaycast.distance;
       
        RaycastHit hit = CreateRaycast(m_CurrentLength);


        Vector3 endPosition = transform.position + (transform.forward * targetLength);


        //while (hit.collider == null && ControllerGrabObject.grab == true) { } ;

        if (hit.collider != null && Math.Pow(2, hit.collider.gameObject.layer) != dotMask.value)
        {
            endPosition = hit.point;

            //if (ControllerGrabObject.grab == true)
            //{
            //    m_CurrentLength = (float)Math.Sqrt(Math.Pow(endPosition.x, 2) + Math.Pow(endPosition.y, 2) + Math.Pow(endPosition.z, 2));
            //}
            //else if (ControllerGrabObject.grab != true)
            //{
            //    m_CurrentLength = m_DefaultLength;
            //}

            //Debug.Log(m_CurrentLength);

            if (Math.Pow(2, hit.collider.gameObject.layer) == teleportMask.value && teleportAction.GetStateUp(handType))
            {
                Teleport(hit.point);
            }
        
        }


        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);


        zLength = Vector3.Distance(transform.position, endPosition);

        //laserColider.size = new Vector3(0.01f, 0.01f, zLength);
        laserColider.center = new Vector3(0.01f, 0.01f, zLength + 0.05f);
    }

    private RaycastHit CreateRaycast(float length)
    {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);

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
