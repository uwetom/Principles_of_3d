using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera triggerCam;
    public Camera liveCam;
    public GameObject target;

    private Camera followCam;
    
    private void Awake()
    {
     //   liveCam = Camera.allCameras[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if(other == PlayerCollider)
        {
            triggerCam.enabled = true;
            liveCam.enabled = false;

    

            PlayerCharacter.GetComponent<AudioListener>().enabled = false;

            triggerCam.GetComponent<AudioListener>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            triggerCam.enabled = false;
            liveCam.enabled = true;

           
            PlayerCharacter.GetComponent<AudioListener>().enabled = true;

            triggerCam.GetComponent<AudioListener>().enabled = false;
        }
    }

}
