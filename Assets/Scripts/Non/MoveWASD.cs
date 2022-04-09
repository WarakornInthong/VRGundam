using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody RB3D ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying){
            return;
        } else {    
            if (Input.GetKey("w")){
                RB3D.AddForce(0,0,10);
            }

            if (Input.GetKey("a")){
                RB3D.AddForce(-10,0,0);
            }

            if (Input.GetKey("s")){
                RB3D.AddForce(0,0,-10);
            }

            if (Input.GetKey("d")){
                RB3D.AddForce(10,0,0);
                
            } 
        }
    }
}
