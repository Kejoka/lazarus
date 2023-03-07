using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [SerializeField] private XRRayInteractor xrRayInteractor;
    [SerializeField] private ActionBasedController actionBasedController;
    [SerializeField] private XRDirectInteractor xrDirectInteractor;
    [SerializeField] private InputActionReference teleportActionRef;

    private void OnEnable()
    {
        teleportActionRef.action.performed += TeleportModeActivate;
        teleportActionRef.action.canceled += TeleportModeCancel;
    }

    private void TeleportModeActivate(InputAction.CallbackContext obj)
    {
        xrDirectInteractor.enabled = false;

        xrRayInteractor.enabled = true;
        actionBasedController.enableInputActions = true;
    }

    private void TeleportModeCancel(InputAction.CallbackContext obj) => Invoke(methodName: "DisableTeleport", time: 0.05f);

    private void DisableTeleport()
    {
        xrDirectInteractor.enabled = true;

        xrRayInteractor.enabled = false;
        actionBasedController.enableInputActions = false;
    }

    private void OnDisable()
    {
        teleportActionRef.action.performed -= TeleportModeActivate;
        teleportActionRef.action.canceled -= TeleportModeCancel;
    }
}
