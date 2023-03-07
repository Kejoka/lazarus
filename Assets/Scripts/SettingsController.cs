using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour continuuousMoveProvider;

    [SerializeField]
    private GameObject teleportationProvider;

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
        if (continuuousMoveProvider.enabled)
        {
            continuuousMoveProvider.enabled = false;
        }
        if (!teleportationProvider.activeSelf)
        {
            teleportationProvider.SetActive(true);
        }

    }

    public void EnableContinuousMovement()
    {
        if (!continuuousMoveProvider.enabled)
        {
            continuuousMoveProvider.enabled = true;
        }
        if (teleportationProvider.activeSelf)
        {
            teleportationProvider.SetActive(false);
        }
    }

    public void EnableDualMovement() {
        continuuousMoveProvider.enabled = true;
        teleportationProvider.SetActive(true);
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
