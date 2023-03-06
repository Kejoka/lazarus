using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabInteraction : MonoBehaviour
{
    [SerializeField]
    private Collider camObject;

    [SerializeField]
    private Toggle questText;

    [SerializeField]
    private Canvas questList;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider == camObject)
        {
            questText.isOn = true;
        }

    }


}
