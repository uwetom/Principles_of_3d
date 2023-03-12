using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public Transform target;
    public Camera zoomCamera;

    private float initHeightAtDistance;
    private bool dollyZoomEnabled;


    void Start()
    {
        StartDollyZoomEffect();
    }

    void Update()
    {
        if (dollyZoomEnabled)
        {
            var currDistance = Vector3.Distance(transform.position, target.position);
            zoomCamera.fieldOfView = FOVForHeightAndDistance(initHeightAtDistance, currDistance);
        }

        if((Input.GetKey("[")) || (Input.GetKey("]")))
        {
            transform.Translate(Input.GetAxis("AltVertical") * Vector3.forward * Time.deltaTime);
        }
    }
    
    float FrustrumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(zoomCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(initHeightAtDistance * 0.5f / distance) * Mathf.Rad2Deg;
    }

    void StartDollyZoomEffect()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDistance = FrustrumHeightAtDistance(distance);
        transform.LookAt(target.transform);
        dollyZoomEnabled = true;
    }

    void StopDollyZoomEffect()
    {
        dollyZoomEnabled = false;
    }

}
