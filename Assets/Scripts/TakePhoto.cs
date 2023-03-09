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
    private Image photoDisplayArea;

    [SerializeField]
    private Canvas photoCanvas;

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
    private Collider dodoCollider;

    [SerializeField]
    private Transform[] mammothTargetPointList;

    [SerializeField]
    private Transform[] catTargetPointList;

    [SerializeField]
    private Transform[] dodoTargetPointList;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private GameObject tutCanvas;

    private int resWidth = 256;
    private int resHeight = 256;

    private Texture2D snapshot;
    //private int screenshotNumber = 0;

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
        if (!photoCanvas.enabled && (tutCanvas == null || !tutCanvas.activeSelf))
        {
            /*
            RenderTexture.active = snapCam.targetTexture;            //render from snapCam, not Main Camera
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            Rect regionToRead = new Rect(0, 0, resWidth, resHeight);
            snapshot.ReadPixels(regionToRead, 0, 0, false);
            snapshot.Apply();
            */

            cameraClick.Play();
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
                questComplete.Play();
                questTextMammoth.isOn = true;
                mammutInfo.Play();
            }
            else if (!questTextCat.isOn && IsVisible(snapCam, catCollider, catTargetPointList))
            {
                questComplete.Play();
                questTextCat.isOn = true;
                smilodonInfo.Play();
            }
            else if (!questTextDodo.isOn && IsVisible(snapCam, dodoCollider, dodoTargetPointList))
            {
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
        Sprite photoSprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, resWidth, resHeight), new Vector2(0.5f, 0.5f));
        photoDisplayArea.sprite = photoSprite;
    }

    public bool IsVisible(Camera c, Collider target, Transform[] rayTargetPointList)
    {
        Vector3 cameraPosition = c.transform.position;
        Vector3 targetCenterPosition = rayTargetPointList[0].transform.position;
        float distanceToObject = Vector3.Distance(targetCenterPosition, cameraPosition);

        // min / max photo distance for dodo
        if (target == dodoCollider)
        {
            if(distanceToObject > 20.0f || distanceToObject < 1.0f)
            {
                return false;
            }
        }
        // min / max photo distance for cat
        else if (target == catCollider)
        {
            if (distanceToObject > 40.0f || distanceToObject < 1.0f)
            {
                return false;
            }
        }
        //min / max photo distance for mammoth
        else
        {
            if(distanceToObject > 60.0f || distanceToObject < 8.0f)
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
