using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPC : MonoBehaviour
{
    public GameObject PCScreenUI;
    public GameObject DesktopScreenUI;
    public GameObject LoginScreenUI;
    private bool isUsing=false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!isUsing)
            {
                EnterPC();
            }
            else
            {
                ExitPC();
            }
        }
    }

    public void EnterPC()
    {
        PCScreenUI.SetActive(true);
        DesktopScreenUI.SetActive(true);
        LoginScreenUI.SetActive(false);
        isUsing = true;
        Debug.Log("Entered PC: Desktop Visible, Login Hidden");

    }

    public void ExitPC()
    {
        PCScreenUI.SetActive(false);
        isUsing = false;
        Debug.Log("All UI hidden");

    }

    public void OpenApp()
    {
        DesktopScreenUI.SetActive(false);
        LoginScreenUI.SetActive(true);
        Debug.Log("LoginScreenUI Opened");
    }
    
    
}
