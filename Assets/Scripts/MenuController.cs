using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private LocomotionTeleport teleportSystem;

    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Canvas questList;

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

    public void ExitMenu()
    {
        menuCanvas.enabled = false;
        questList.enabled = true;
    }
}
