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
    private Canvas questCanvas;

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
    private Transform[] mammothTargetPointList;

    [SerializeField]
    private Transform[] catTargetPointList;  
  
    [SerializeField]
    private Collider dodoColliderOne;

    [SerializeField]
    private Transform[] dodoTargetPointOne;

    [SerializeField]
    private Collider dodoColliderTwo;

    [SerializeField]
    private Transform[] dodoTargetPointTwo;

    [SerializeField]
    private Collider dodoColliderThree;

    [SerializeField]
    private Transform[] dodoTargetPointThree;

    [SerializeField]
    private Collider dodoColliderFour;

    [SerializeField]
    private Transform[] dodoTargetPointFour;
  
    [SerializeField]
    private LayerMask layer;

    private int resWidth = 256;
    private int resHeight = 256;

    private Texture2D snapshot;
  

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
            snapshot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            Graphics.CopyTexture(snapCam.targetTexture, snapshot);

            if (!questTextCamera.isOn)
            {
                questTextCamera.isOn = true;
            }
            if(!questTextMammoth.isOn && IsVisible(snapCam, mammothCollider, mammothTargetPointList))
            {
                questTextMammoth.isOn = true;
            }
            else if (!questTextCat.isOn && IsVisible(snapCam, catCollider, catTargetPointList))
            {
                questTextCat.isOn = true;
            }
            else if (!questTextDodo.isOn && (IsVisible(snapCam, dodoColliderOne, dodoTargetPointOne) || IsVisible(snapCam, dodoColliderTwo, dodoTargetPointTwo) || IsVisible(snapCam, dodoColliderThree, dodoTargetPointThree) || IsVisible(snapCam, dodoColliderFour, dodoTargetPointFour)) )
            {
                questTextDodo.isOn = true;
            }

            ShowPhoto();
        }
    }

    public void ShowPhoto()
    {
        photoCanvas.enabled = true;
        if (questCanvas.enabled)
        {
            questCanvas.enabled = false;
        }
        Sprite photoSprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, resWidth, resHeight), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
    }


    public bool IsVisible(Camera c, Collider target, Transform[] rayTargetPointList)
    {
        Vector3 cameraPosition = c.transform.position;
        Vector3 targetCenterPosition = rayTargetPointList[0].transform.position;
        float distanceToObject = Vector3.Distance(targetCenterPosition, cameraPosition);

        // min / max photo distance for dodo
        if (target == dodoColliderOne || target == dodoColliderTwo || target == dodoColliderThree || target == dodoColliderFour)
        {
            if (distanceToObject > 30.0f || distanceToObject < 1.0f)
            {
                return false;
            }
        }
        
        // min / max photo distance for cat
        else if (target == catCollider)
        {
            if (distanceToObject > 40.0f || distanceToObject < 0.5f)
            {
                return false;
            }
        }
        //min / max photo distance for mammoth
        else
        {
            if(distanceToObject > 50.0f || distanceToObject < 8.0f)
            {
                return false;
            }
        }

        //check if camera is looking in direction of target gameobject
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(targetCenterPosition) < 0)
            {
                return false;
            }
        }

        //check if target points are really visible (not hidden behind other objects/terrain)
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
