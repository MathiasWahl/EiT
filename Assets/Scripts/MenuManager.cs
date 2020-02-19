using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Panel currentPanel = null;
    private bool active = false;

    private List<Panel> panelHistory = new List<Panel>();

    // Start is called before the first frame update
    void Start()
    {
        SetupPanels();
    }

    private void SetupPanels()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
            panel.Setup(this);

        currentPanel.Show();
    }

    public void Update()
    {
        //poll meny_knapp
    }

}
