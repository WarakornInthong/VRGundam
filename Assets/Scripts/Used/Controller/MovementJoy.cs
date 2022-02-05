using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoy : Joy
{
    private CharacterController playerCharacter;
    private CharacterController mechCharacter;
    private JaganController jaganController;
    private MechController mechController;
    private Vector2 inputMovement;
    // public float speed = 2;

    void Start()
    {
        playerCharacter = player.GetComponent<CharacterController>();
        mechCharacter = target.GetComponent<CharacterController>();
        jaganController = target.GetComponent<JaganController>();
        // mechController = target.GetComponent<MechController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isUsed){
            playerCharacter.enabled = false;
            mechCharacter.enabled = true;
            Vector3 jdirect = Vector3.ProjectOnPlane(joyDirection.up, Vector3.up).normalized;
            Quaternion headYaw = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(-jdirect.z, 0, jdirect.x);
            mechCharacter.Move(direction * Time.fixedDeltaTime * jaganController.moveSpeed);
            // Debug.Log(jdirect);

        }
        else{
            playerCharacter.enabled = true;
            mechCharacter.enabled = false;
        }
    }
}
