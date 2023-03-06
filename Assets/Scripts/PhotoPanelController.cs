using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPanelController : MonoBehaviour
{
    [SerializeField]
    private Canvas photoCanvas;

    public void ClosePhotoPanel()
    {
        photoCanvas.enabled = false;
    }
}
