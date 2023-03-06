using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenuController : MonoBehaviour
{
    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Canvas controlsCanvas;

    [SerializeField]
    private Canvas questList;

    [SerializeField]
    private Toggle showQuestListToggle;

    public void BackToMain()
    {
        controlsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void ExitMenu()
    {
        controlsCanvas.enabled = false;
        if (showQuestListToggle.isOn)
        {
            questList.enabled = true;
        }
    }
}
