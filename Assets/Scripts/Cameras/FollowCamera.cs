using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;
    private Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        transform.position = Vector3.Lerp(transform.position, target.transform.position + (rotation * Offset), 0.05f);

        //transform.position = target.transform.position + (rotation * Offset);
        
        transform.LookAt(target.transform);
    }
   
}
