using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestList : MonoBehaviour
{
    [SerializeField]
    private OVRGrabbable cameraObject;

    [SerializeField]
    private Collider camObject;

    [SerializeField]
    private Toggle questText;

    [SerializeField]
    private Canvas canvas;
    
    

    /*
    public void Update()
    {
        if (cameraObject.isGrabbed)
        {
            Console.Write("Camera is grabbed!");
            questText.isOn = true;
            canvas.enabled = false;
        }
    }
    */
}
