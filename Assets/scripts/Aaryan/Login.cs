using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text text;
    public GameObject LoginScreenUI;
    public GameObject DesktopScreenUI;
    
    private string username_ = "Jai Pausch" ;
    private string password_ = "NTL" ;

    public void Submit()
    {
        if (username.text == username_ && password.text == password_)
        {
            text.text = "You've got access";
            Invoke("ClosePC",1.5f);
        }
        else
        {
            text.text = "Wrong username or password";
        }
    }

    public void ClosePC()
    {
        LoginScreenUI.SetActive(false);
        DesktopScreenUI.SetActive(true);
    }
}
