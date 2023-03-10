 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
   public AudioClip shoutingClip;
   public float speedDampTime = 0.01f;
   public float sensitivityX = 1.0f;
   public float walkSpeed = 1.5f;
   public float pitchValue = 1.5f;

   private Animator anim;
   private HashIDs hash;
   private float elapsedTime = 0;
   private bool noBackMov = true;


   private void Awake(){
        anim = GetComponent <Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
   }

   void FixedUpdate(){
    float v = Input.GetAxis("Vertical");
    bool sneak = Input.GetButton("Sneak");
    float turn = Input.GetAxis("Turn");

    Rotating(turn);
    MovementManagement(v, sneak);

        elapsedTime += Time.deltaTime;
      //  Debug.Log("elapsed time is : " + elapsedTime);
   }

   void Update(){
      //Cache the attention attracting input.
       bool shout = Input.GetButtonDown("Attract");
       //Set the animator shouting parameter.
       anim.SetBool(hash.shoutingBool, shout);
      AudioManagement(shout);

   }

   void MovementManagement (float vertical, bool sneaking){

        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0){
            anim.SetFloat(hash.speedFloat, walkSpeed, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
            noBackMov = true;
        }
        
        if(vertical < 0)
        {
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();

            float movement = Mathf.Lerp(0f, -0.025f, 0.7f);
            Vector3 moveBack = new Vector3(0.0f, 0.0f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;

            if(noBackMov == true)
            {
                elapsedTime = 0;
                noBackMov = false;
            }
        }

        if(vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0.01f);
            anim.SetBool(hash.backwardsBool, false);
            noBackMov = true;
        }
        
      



   }

   void Rotating(float mouseXInput){
      //access the avatar's rigidbody
      Rigidbody ourBody = this.GetComponent<Rigidbody>();

      //first check to see if we have rotaion data that needs to be applied
      if(mouseXInput != 0){
         //if so we use mouseX value to create a Euler angle which provides rotation in the Y axis
         //which is then turned to a Quaternion
         Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);

         //and then applied to turn the avatar via the rigidbody
         ourBody.MoveRotation(ourBody.rotation * deltaRotation);
      }
   }

   void AudioManagement (bool shout){
      if(anim.GetCurrentAnimatorStateInfo (0). IsName("Walk")){
         // and if the footsteps are not already playing
         if(!GetComponent<AudioSource>().isPlaying){
            //play footsteps.
            GetComponent<AudioSource>().pitch = 0.27f;
            GetComponent<AudioSource>().Play();
         }
      }
      else{
         GetComponent<AudioSource>().Stop();
      }

      if(shout){
         //3D sound so play the shouting clip where we are.
         AudioSource.PlayClipAtPoint(shoutingClip, transform.position);

            GameObject thisAudio = GameObject.Find("One shot audio");

            if(thisAudio.name == "z2 - V2 - Angry - Free - 1")
            {
                thisAudio.GetComponent<AudioSource>().pitch = pitchValue;
            }
      }
   }
}
