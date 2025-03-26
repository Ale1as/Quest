using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("Door opened");
        Destroy(gameObject, 1f);
    }
}
