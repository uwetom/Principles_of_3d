
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset;


    private float mouseX;
    private float mouseY;
    private float mouseZ;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }


    private void LateUpdate()
    {
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        float desiredAngleY = target.transform.eulerAngles.y;
        //float desiredAngleX = target.transform.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(0, desiredAngleY, 0);

        transform.position = target.transform.position + (rotation * offset);

        transform.LookAt(target.transform);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        //mouseZ = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            Debug.Log(mouseX);

            offset = Quaternion.Euler(0, mouseX, 0) * offset;
            Debug.Log(offset);
        }
        if (Input.GetMouseButton(0))
        {
            if (((angleBetween > 90) && (mouseY < 0)) || ((angleBetween < 140) && (mouseY > 0)))
            {
                Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
                offset = Quaternion.AngleAxis(mouseY, LocalRight) * offset;
            }

        }
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;
    public Vector3 Offset;

    private float mouseX;
    private float mouseY;
    private float mouseZ;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        transform.position = Vector3.Lerp(transform.position, target.transform.position + (rotation * Offset), 0.01f);

        //transform.position = target.transform.position + (rotation * Offset);
        
        transform.LookAt(target.transform);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            Offset = Quaternion.Euler(0, mouseX, 0) * Offset;
        }


        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
   

        if (Input.GetMouseButton(0))
        {
            if (((angleBetween > 100.0f) && (mouseY < 0)) || ((angleBetween < 140.0f) && (mouseY > 0)))
             {

                Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
                Offset = Quaternion.AngleAxis(mouseY, LocalRight) * Offset;

            }
         


        }


        float dist = Vector3.Distance(target.transform.position, transform.position);
      //  Debug.Log("dist: " + dist);



        if((mouseZ > 0) && (dist < 10.0f))
        {
            Offset = Vector3.Scale(Offset, new Vector3(1.05f, 1.05f, 1.05f));
        }

        if((mouseZ < 0) && (dist > 1.6f))
        {
            Offset = Vector3.Scale(Offset, new Vector3(0.95f, 0.95f, 0.95f));
        }

    }
   
}

*/