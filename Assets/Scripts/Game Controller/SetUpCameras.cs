using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{

    public Camera FollowCam;
    public Camera StaticCam;
    public Camera PipCam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        FollowCam.enabled = true;
        StaticCam.enabled = false;
        PipCam.enabled = false;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        StaticCam.GetComponent<AudioListener>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyUp("p")){
            PipCam.enabled = !PipCam.enabled;
        }
    }



  
}
