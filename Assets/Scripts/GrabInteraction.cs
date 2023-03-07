using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabInteraction : MonoBehaviour
{

    [SerializeField]
    private Toggle questText;

    public void toggleQuestText() {
        questText.isOn = true;
    }


}
