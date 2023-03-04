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
    private Toggle questTextCamera;

    [SerializeField]
    private Toggle questTextMammoth;

    [SerializeField]
    private Toggle questTextCat;

    [SerializeField]
    private Toggle questTextDodo;

    [SerializeField]
    private Collider mammothCollider;

    [SerializeField]
    private Collider catCollider;

    [SerializeField]
    private Collider dodoCollider;

    [SerializeField]
    private Transform[] mammothTargetPointList;

    [SerializeField]
    private Transform[] catTargetPointList;

    [SerializeField]
    private Transform[] dodoTargetPointList;

    [SerializeField]
    private LayerMask layer;

    private int resWidth = 256;
    private int resHeight = 256;

    private Texture2D snapshot;
    //private int screenshotNumber = 0;
  

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
            //png-file wird gespeichert, ist aber komplett wei�
            byte[] byteArray = snapshot.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/camera" + screenshotNumber + ".png", byteArray);             
            screenshotNumber += 1;
            */
            if (!questTextCamera.isOn)
            {
                questTextCamera.isOn = true;
            }
            if(!questTextMammoth.isOn && IsVisible(snapCam, mammothCollider, mammothTargetPointList))
            {
                questTextMammoth.isOn = true;
            }
            if (!questTextCat.isOn && IsVisible(snapCam, catCollider, catTargetPointList))
            {
                questTextCat.isOn = true;
            }
            if (!questTextDodo.isOn && IsVisible(snapCam, dodoCollider, dodoTargetPointList))
            {
                questTextDodo.isOn = true;
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

    public bool IsVisible(Camera c, Collider target, Transform[] rayTargetPointList)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        Vector3 cameraPosition = c.transform.position;
        Vector3 targetCenterPosition = rayTargetPointList[0].position;
        //Vector3 targetDirection = targetCenterPosition - cameraPosition;
        //Vector3 fwd = c.transform.TransformDirection(Vector3.forward);
        //float distanceToObject = Vector3.Distance(point, cameraPosition);
        
        //check if camera is looking in direction of target gameobject
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(targetCenterPosition) < 0)
            {
                return false;
            }
        }
        
        //check if target points are really visible (not hidden behind other objects/terrain
        RaycastHit hit;

        foreach (Transform targetPoint in rayTargetPointList)
        {
            Vector3 targetDirection = targetPoint.position - cameraPosition;
            if (Physics.Raycast(cameraPosition, targetDirection, out hit, layer))
            {
                if (hit.collider != target)
                {
                    return false;
                }
                
            }
        }
        return true;

        

    }
}
