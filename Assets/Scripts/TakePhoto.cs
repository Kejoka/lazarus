using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Rendering;

public class TakePhoto : MonoBehaviour
{

    [SerializeField]
    private AudioSource cameraClick;

    [SerializeField]
    private AudioSource questComplete;

    [SerializeField]
    private AudioSource mammutInfo;

    [SerializeField]
    private AudioSource dodoInfo;

    [SerializeField]
    private AudioSource smilodonInfo;

    [SerializeField]
    private Camera snapCam;

    [SerializeField]
    private RawImage photoDisplayArea;

    [SerializeField]
    private Canvas photoCanvas;

      [SerializeField]
    private Canvas endCanvas;

    [SerializeField]
    private Canvas questCanvas;

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

    [SerializeField]
    private GameObject tutCanvas;

    private Texture2D snapshot;
    // Variables that are needed for method 1 + 2
    // private int resWidth = 256;
    // private int resHeight = 256;
    // private int screenshotNumber = 0;

    // DEBUG
    //  IEnumerator ExecuteAfterTime()
    // {
    //     yield return new WaitForSeconds(2);
    //     TakeSnapshot();
    // }

    // void Start() {
    //     StartCoroutine(ExecuteAfterTime());
    // }

    public void TakeSnapshot()
    {
        if (!photoCanvas.enabled && (tutCanvas == null || !tutCanvas.activeSelf) && (endCanvas == null || !endCanvas.enabled))
        {
            // Method 1: Use ReadPixels() and work with byteArray -> results in white image on disk and in unity on windows
            // On OSX the image that is saved to the Disk is fine but Unity still only shows a white image
            // RenderTexture.active = snapCam.targetTexture;            //render from snapCam, not Main Camera
            // Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            // Rect regionToRead = new Rect(0, 0, resWidth, resHeight);
            // snapshot.ReadPixels(regionToRead, 0, 0, false);
            // snapshot.Apply();
            // png-file wird gespeichert, ist aber komplett wei�
            // byte[] byteArray = snapshot.EncodeToPNG();
            // File.WriteAllBytes(Application.persistentDataPath + "/camera" + screenshotNumber + ".png", byteArray);             
            // screenshotNumber += 1;

            // Method 2: Use CopyTexture() -> results in transparent vegetation in shown image if said vegetation has nothign but the skybox in the background
            // snapshot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            // Graphics.CopyTexture(snapCam.targetTexture, snapshot);

            // Method 3: Works but is just a workaround instead of the functionality that is technically needed for an image gallery
            cameraClick.Play();
            snapCam.enabled = false;


            if (!questTextCamera.isOn)
            {
                questTextCamera.isOn = true;
            }
            if(!questTextMammoth.isOn && IsVisible(snapCam, mammothCollider, mammothTargetPointList))
            {
                mammutInfo.Stop();
                smilodonInfo.Stop();
                dodoInfo.Stop();
                questComplete.Play();
                questTextMammoth.isOn = true;
                mammutInfo.Play();
            }
            else if (!questTextCat.isOn && IsVisible(snapCam, catCollider, catTargetPointList))
            {
                mammutInfo.Stop();
                smilodonInfo.Stop();
                dodoInfo.Stop();
                questComplete.Play();
                questTextCat.isOn = true;
                smilodonInfo.Play();
            }
            else if (!questTextDodo.isOn && (IsVisible(snapCam, dodoColliderOne, dodoTargetPointOne) || IsVisible(snapCam, dodoColliderTwo, dodoTargetPointTwo) || IsVisible(snapCam, dodoColliderThree, dodoTargetPointThree) || IsVisible(snapCam, dodoColliderFour, dodoTargetPointFour)) )
            {
                mammutInfo.Stop();
                smilodonInfo.Stop();
                dodoInfo.Stop();
                questComplete.Play();
                questTextDodo.isOn = true;
                dodoInfo.Play();
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
        // Part of method 1 and 2. photoDisplayArea must be an Image instead of a RawImage for this to workx
        // Sprite photoSprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, resWidth, resHeight), new Vector2(0.5f, 0.5f));
        // photoDisplayArea.texture = snapshot;
    }

    public bool IsVisible(Camera c, Collider target, Transform[] rayTargetPointList)
    {
        Vector3 cameraPosition = c.transform.position;
        Vector3 targetCenterPosition = rayTargetPointList[0].transform.position;
        float distanceToObject = Vector3.Distance(targetCenterPosition, cameraPosition);

        // min / max photo distance for dodo
        if (target == dodoColliderOne || target == dodoColliderTwo || target == dodoColliderThree || target == dodoColliderFour)
        {
            if(distanceToObject > 30.0f || distanceToObject < 1.0f)
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
