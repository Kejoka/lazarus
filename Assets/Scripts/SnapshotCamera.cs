using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;

public class SnapshotCamera : MonoBehaviour
{
    [SerializeField]
    private Camera snapCam;

    [SerializeField]
    private Image photoDisplayArea;

    [SerializeField]
    private Canvas photoCanvas;

    [SerializeField]
    private OVRGrabbable cameraGrab;

    private int resWidth = 256;
    private int resHeight = 256;

    private UnityEngine.XR.InputDevice device;

    private Texture2D snapshot;

    void Start()
    {
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        device = rightHandDevices[0];

        //screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.One) && cameraGrab.isGrabbed && !photoCanvas.enabled) 
        {
            StartCoroutine(TakeSnapshot());
        }
       


        /*
        device.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonIsPressed);
        if (buttonIsPressed && cameraGrab.isGrabbed && !photoCanvas.enabled)
        {
            //StartCoroutine(TakeSnapshot());
            TakeSnapshot();
        }

        /*
        float triggerValue;
        if (cameraGrab.isGrabbed && (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerValue) ))
        {
            if(triggerValue == 1.0f)
            {
                TakeSnapshot();
            }
            //StartCoroutine(TakeSnapshot());
            
        }
        */
        /*
        device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerIsPressed);
        if(triggerIsPressed && cameraGrab.isGrabbed && !photoCanvas.enabled)
        {
            //StartCoroutine(TakeSnapshot());
            TakeSnapshot();
        }
        */

    }

    public IEnumerator TakeSnapshot()
    {
        yield return new WaitForEndOfFrame();
        /*
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        //ScreenCapture.CaptureScreenshot("screenshot.png");
        ShowPhoto();
        */
        
         //RenderTexture screenTexture = new RenderTexture(resWidth, resHeight, 16);
         //snapCam.targetTexture = screenTexture;
         RenderTexture.active = snapCam.targetTexture;            //render from snapCam, not Main Camera
         //snapCam.Render();

         snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false); 
         Rect regionToRead = new Rect(0, 0, resWidth, resHeight); 

         snapshot.ReadPixels(regionToRead, 0, 0, false);
         snapshot.Apply();
         byte[] byteArray = snapshot.EncodeToPNG();
         File.WriteAllBytes(Application.dataPath + "camera.png", byteArray);
         ShowPhoto();
        
    }


    public void ShowPhoto()
    {
        photoCanvas.enabled = true;
        Sprite photoSprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, snapshot.width, snapshot.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        
        /*
        if (!photoCanvas.enabled)
        {
            photoCanvas.enabled = true;
        }
        */
    }
}
