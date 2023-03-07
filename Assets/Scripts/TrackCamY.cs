using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamY : MonoBehaviour
{
    [SerializeField] private Transform mainCamTransform;
    [SerializeField] private Transform UITransform;

    void Update()
    {
        float euler_y = mainCamTransform.eulerAngles.y;
        UITransform.eulerAngles = new Vector3(UITransform.eulerAngles.x, euler_y, UITransform.eulerAngles.z);
    }
}
