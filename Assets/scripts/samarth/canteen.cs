using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canteen : MonoBehaviour, IInteractable
{
    public GameObject Qrscan;
    public void Interact()
    {
        Qrscan.SetActive(true);
        print("canteen opened");
    }
}
