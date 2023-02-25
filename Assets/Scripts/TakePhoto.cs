using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Rendering;

public class TakePhoto : MonoBehaviour
{
    public InputActionReference triggerPressedReference = null;

    [SerializeField]
    private Camera snapCam;

    [SerializeField]
    private Image photoDisplayArea;

    [SerializeField]
    private Canvas photoCanvas;

    [SerializeField]
    private OVRGrabbable cameraGrab;

    [SerializeField]
    private Toggle questText;

    private int resWidth = 256;
    private int resHeight = 256;

    private Texture2D snapshot;
    private int screenshotNumber = 0;
  

    private void Awake()
    {
        triggerPressedReference.action.performed += TakeSnapshot;
    }

    private void OnDestroy()
    {
        triggerPressedReference.action.performed -= TakeSnapshot;
    }

    private void TakeSnapshot(InputAction.CallbackContext context)
    {
        if (cameraGrab.isGrabbed && !photoCanvas.enabled)
        {
            /*
            RenderTexture.active = snapCam.targetTexture;            //render from snapCam, not Main Camera
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            Rect regionToRead = new Rect(0, 0, resWidth, resHeight);
            snapshot.ReadPixels(regionToRead, 0, 0, false);
            snapshot.Apply();
            */
            snapshot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            Graphics.CopyTexture(snapCam.targetTexture, snapshot);

            /*
            //png-file wird gespeichert, ist aber komplett weiﬂ
            byte[] byteArray = snapshot.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/camera" + screenshotNumber + ".png", byteArray);             
            screenshotNumber += 1;
            */
            if (!questText.isOn)
            {
                questText.isOn = true;
            }
            ShowPhoto();
        }
    }

    public void ShowPhoto()
    {
        photoCanvas.enabled = true;
        Sprite photoSprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, resWidth, resHeight), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
    }
}
