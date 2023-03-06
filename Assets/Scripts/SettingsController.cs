using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private LocomotionTeleport teleportSystem;

    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Canvas settingsCanvas;

    [SerializeField]
    private Canvas questList;

    [SerializeField]
    private Toggle showQuestListToggle;

    public void EnableTeleportSystem()
    {
        if (controller.enabled)
        {
            controller.enabled = false;
        }
        if (!teleportSystem.enabled)
        {
            teleportSystem.enabled = true;
        }

    }

    public void EnableContinuousMovement()
    {
        if (!controller.enabled)
        {
            controller.enabled = true;
        }
        if (teleportSystem.enabled)
        {
            teleportSystem.enabled = false;
        }
    }

    public void BackToMain()
    {
        settingsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void ExitMenu()
    {
        settingsCanvas.enabled = false;
        if (showQuestListToggle.isOn)
        {
            questList.enabled = true;
        }
    }
}
