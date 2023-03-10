using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Completecheck : MonoBehaviour
{
    [SerializeField]
    private Toggle[] Quests;

    [SerializeField]
    private Canvas EndCanvas;
    // Update is called once per frame
    void Update()
    {
        foreach (Toggle quest in Quests) {
            if (!quest.isOn) {
                return;
            }
        } 
        EndCanvas.enabled = true;
    }
}
