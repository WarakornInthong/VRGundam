using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaganController : MonoBehaviour
{
    // Aiming
    public Transform aimPoint;
    public Transform upperChest;

    [SerializeField][Range (0,1)]
    private float aimSpeed;

    // Controller
    [Range (4, 10)]
    public float moveSpeed;

    // Animation
    private Animator animator;

    void Start()
    {
        // Set Animation Fight Mode
        animator = GetComponent<Animator>();
        animator.SetBool("Aim",true);
    }

    void Update()
    {
        // Check Speed
        if(moveSpeed >= 10){
            moveSpeed = 10;
        }
        else if(moveSpeed < 4f){
            moveSpeed = 4f;
        }
    }

    void LateUpdate()
    {
        Aiming();
    }

    private void Aiming(){
        // Find Aim Vector
        Vector3 aimVector = aimPoint.position - upperChest.position;
        Quaternion aimRotation = Quaternion.LookRotation(aimVector);
        
        // Change Degree Setting
        Vector3 aimEulerAngles = aimRotation.eulerAngles;
        ChangeDegreeBound(ref aimEulerAngles);
        
        upperChest.rotation = Quaternion.Lerp(upperChest.rotation, aimRotation, aimSpeed);

        // Limit Rotation
        if(Mathf.Abs(aimEulerAngles.x) >= 30){
            if(aimEulerAngles.x > 0)
                upperChest.localEulerAngles = new Vector3(30, aimEulerAngles.y, aimEulerAngles.z);
            else
                upperChest.localEulerAngles = new Vector3(-30, aimEulerAngles.y, aimEulerAngles.z);
        }
    }

    private void ChangeDegreeBound(ref Vector3 eulerAngles){
        if(eulerAngles.x > 180){
            eulerAngles.x -= 360;
        }
        if(eulerAngles.y > 180){
            eulerAngles.y -= 360;
        }
        if(eulerAngles.z > 180){
            eulerAngles.z -= 360;
        }
    }
}
