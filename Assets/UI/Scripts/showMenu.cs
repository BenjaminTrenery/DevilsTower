using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class showMenu : MonoBehaviour
{
    public GameObject myUIElement;
    public void ToggleVisibility()
    {

        if(myUIElement.activeSelf == true)
        {
            myUIElement.SetActive(false);
        }

        else
        {
            myUIElement.SetActive(true);
        }
    }
}
    
