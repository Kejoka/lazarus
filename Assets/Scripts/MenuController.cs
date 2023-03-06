using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public InputActionReference menuButtonPressedReference = null;

    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Canvas controlsCanvas;

    [SerializeField]
    private Canvas settingsCanvas;

    [SerializeField]
    private Canvas questList;

    [SerializeField]
    private Toggle showQuestListToggle;

    private void Awake()
    {
        menuButtonPressedReference.action.started += OpenMenu;
    }

    private void OnDestroy()
    {
        menuButtonPressedReference.action.started -= OpenMenu;
    }
    
    
    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!menuCanvas.enabled)
        {
            questList.enabled = false;
            menuCanvas.enabled = true;
        }
        
    }

    public void ShowControls()
    {
        menuCanvas.enabled = false;
        controlsCanvas.enabled = true;
    }

    public void ShowMovementSettings()
    {
        menuCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }

    public void ExitMenu()
    {
        menuCanvas.enabled = false;
        if (showQuestListToggle.isOn)
        {
            questList.enabled = true;
        }  
    }
}
