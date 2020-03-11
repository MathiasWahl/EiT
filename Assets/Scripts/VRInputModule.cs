using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{

    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;
    public SteamVR_Action_Boolean m_MenuAction;

    private GameObject m_currentObject = null;
    private PointerEventData m_Data = null;

    private bool menuActive = true;
    public GameObject menuObject;

    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth/2, m_Camera.pixelHeight/2);

        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_currentObject = m_Data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(m_Data, m_currentObject);
        
        if (m_ClickAction.GetStateDown(m_TargetSource))
            ProcessPress(m_Data);

        if (m_ClickAction.GetStateUp(m_TargetSource))
            ProcessRelease(m_Data);


    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {


        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_currentObject, data, ExecuteEvents.pointerDownHandler);

        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_currentObject);

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_currentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHanndler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_currentObject);

        if (data.pointerPress == pointerUpHanndler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

    private void Update()
    {
        //turn menu on/of
        if (m_MenuAction.GetStateDown(m_TargetSource))
        {
            ToggleActive();
        }
    }

    public void ToggleActive()
    {
        if (menuActive)
        {
            menuObject.SetActive(false);
            menuActive = false;
        }
        else
        {
            menuObject.SetActive(true);
            menuActive = true;
        }

    }
}
