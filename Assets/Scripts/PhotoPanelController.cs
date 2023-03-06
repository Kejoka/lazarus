using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPanelController : MonoBehaviour
{
    [SerializeField]
    private Canvas photoCanvas;

    [SerializeField]
    private Canvas questCanvas;

    [SerializeField]
    private Toggle showQuestListToggle;

    public void ClosePhotoPanel()
    {
        photoCanvas.enabled = false;
        if (showQuestListToggle.isOn)
        {
            questCanvas.enabled = true;
        }
    }
}
